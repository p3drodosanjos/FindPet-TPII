namespace findPet.Models // Deve estar no MESMO namespace do Anuncio.cs
{
    public class AnuncioPetPerdido : AnuncioModel
    {
        public override string ObterStatusPublicacao()
        {
            return $"PET PERDIDO: {this.NomeDoPet} perdido perto de {this.Localizacao}";
        }
    }
}