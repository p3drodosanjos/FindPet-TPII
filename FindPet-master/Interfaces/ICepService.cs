// Em Interfaces/ICepService.cs
using FindPet.Models; // Adicione o using para o EnderecoModel

namespace FindPet.Interfaces
{
    public interface ICepService
    {
        // Define o "contrato" que nosso app espera
        Task<EnderecoModel> BuscarPorCep(string cep);
    }
}