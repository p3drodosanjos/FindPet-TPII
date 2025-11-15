// Em Models/EnderecoModel.cs
namespace FindPet.Models
{
    public class EnderecoModel
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; } // "UF"
        public string Cep { get; set; }
    }
}