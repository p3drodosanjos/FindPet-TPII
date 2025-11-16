using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace findPet.Models
{
    // Classe base/abstrata (o "Produto" do Factory Method)
    public abstract class AnuncioModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        // Propriedades comuns a todos os anúncios (perdido ou encontrado)
        public string TipoAnimal { get; set; } // Ex: Cachorro, Gato
        public string NomeDoPet { get; set; }
        public string Descricao { get; set; }
        public string Localizacao { get; set; } // Onde foi perdido/encontrado
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Método comum (exemplo)
        public abstract string ObterStatusPublicacao();
    }
}