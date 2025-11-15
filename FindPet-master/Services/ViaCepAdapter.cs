// Em Services/ViaCepAdapter.cs
using FindPet.Interfaces;
using FindPet.Models;
using FindPet.Models.DTOs;
using System.Net.Http.Json; // Para usar GetFromJsonAsync

namespace FindPet.Services
{
    public class ViaCepAdapter : ICepService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Pedimos o IHttpClientFactory via Injeção de Dependência
        public ViaCepAdapter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<EnderecoModel> BuscarPorCep(string cep)
        {
            // 1. Cria um cliente HTTP usando a Factory (boa prática)
            var httpClient = _httpClientFactory.CreateClient();
            
            // Define a URL da API externa
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                // 2. Chama a API externa e já deserializa a resposta para nosso DTO
                var viaCepResponse = await httpClient.GetFromJsonAsync<ViaCepResponse>(url);

                if (viaCepResponse == null || viaCepResponse.Erro)
                {
                    return null; // Ou lançar uma exceção, dependendo da regra de negócio
                }

                // 3. A MÁGICA DO ADAPTER: A "Tradução"
                // Converte do modelo externo (ViaCepResponse) para o nosso (EnderecoModel)
                var enderecoModel = new EnderecoModel
                {
                    Rua = viaCepResponse.Logradouro,
                    Bairro = viaCepResponse.Bairro,
                    Cidade = viaCepResponse.Localidade,
                    Estado = viaCepResponse.Uf,
                    Cep = viaCepResponse.Cep
                };

                return enderecoModel;
            }
            catch (Exception ex)
            {
                // Lidar com erros de rede, JSON inválido, etc.
                // Logar o erro (ex.Logger.LogError(ex, "Erro ao buscar CEP"))
                return null;
            }
        }
    }
}