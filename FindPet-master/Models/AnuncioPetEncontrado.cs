namespace findPet.Models // Deve estar no MESMO namespace do Anuncio.cs
{
    public class AnuncioPetEncontrado : AnuncioModel
    {
        public override string ObterStatusPublicacao()
        {
            return $"PET ENCONTRADO: {this.NomeDoPet} encontrado perto de {this.Localizacao}";
        }
    }
}