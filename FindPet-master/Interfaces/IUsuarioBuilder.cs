using FindPet.Models; // Você pode precisar alterar para o namespace do seu modelo Usuario, se for diferente

namespace FindPet.Interfaces
{
    public interface IUsuarioBuilder
    {
        // Etapa 1: Define as informações pessoais
        IUsuarioBuilder SetInfoPessoais(string nome, string email, string senha, string cpf, string telefone, string dataNascimento);
        
        // Etapa 2: Define o endereço
        IUsuarioBuilder SetEndereco(string cep, string rua, string bairro, string cidade, string numero, string complemento, string estado, string pais);
        
        // Retorna o objeto complexo final
        Usuario Build();
    }
}