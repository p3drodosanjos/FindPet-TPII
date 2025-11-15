// Em Models/DTOs/ViaCepResponse.cs
using System.Text.Json.Serialization; // Importante para o mapeamento do JSON

namespace FindPet.Models.DTOs
{
    // Esta classe espelha a resposta da API externa
    public class ViaCepResponse
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } // "Rua" para eles

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string Localidade { get; set; } // "Cidade" para eles

        [JsonPropertyName("uf")]
        public string Uf { get; set; } // "Estado" para eles
        
        [JsonPropertyName("erro")]
        public bool Erro { get; set; } // ViaCEP retorna isso se o CEP não existe
    }
}