using FindPet.Interfaces;
// Importe o namespace do seu modelo Usuario (ex: FindPet.Models ou global)
// using FindPet.Models; 

namespace FindPet.Builders
{
    public class UsuarioBuilder : IUsuarioBuilder
    {
        private Usuario _usuario;

        public UsuarioBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._usuario = new Usuario();
        }

        // Define as propriedades da etapa 1
        public IUsuarioBuilder SetInfoPessoais(string nome, string email, string senha, string cpf, string telefone, string dataNascimento)
        {
            _usuario.Nome = nome;
            _usuario.Email = email;
            _usuario.Senha = senha; // IMPORTANTE: Em produção, a senha deve ser hasheada aqui ou no serviço.
            _usuario.Cpf = cpf;
            _usuario.Telefone = telefone;
            _usuario.DatadeNascimento = dataNascimento;
            return this;
        }

        // Define as propriedades da etapa 2
        public IUsuarioBuilder SetEndereco(string cep, string rua, string bairro, string cidade, string numero, string complemento, string estado, string pais)
        {
            _usuario.CEP = cep;
            _usuario.Rua = rua;
            _usuario.Bairro = bairro;
            _usuario.Cidade = cidade;
            _usuario.Numero = numero;
            _usuario.Complemento = complemento;
            _usuario.Estado = estado;
            _usuario.Pais = pais ?? "Brasil"; // Define um padrão
            return this;
        }

        // Retorna o usuário completo e reseta o builder
        public Usuario Build()
        {
            if (string.IsNullOrEmpty(_usuario.Email) || string.IsNullOrEmpty(_usuario.Nome))
            {
                throw new InvalidOperationException("Informações pessoais básicas são obrigatórias para construir o usuário.");
            }

            Usuario resultado = this._usuario;
            this.Reset(); // Prepara para a próxima construção
            return resultado;
        }
    }
}