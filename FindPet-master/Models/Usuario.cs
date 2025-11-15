
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string DatadeNascimento { get; set; } = null!;

    // ==================================================
    // == ADICIONE ESTES CAMPOS DE ENDEREÇO AQUI ==
    // ==================================================
    [BsonElement("CEP")]
    public string? CEP { get; set; }

    [BsonElement("Rua")]
    public string? Rua { get; set; }

    [BsonElement("Bairro")]
    public string? Bairro { get; set; }

    [BsonElement("Cidade")]
    public string? Cidade { get; set; }

    [BsonElement("Numero")]
    public string? Numero { get; set; }

    [BsonElement("Complemento")]
    public string? Complemento { get; set; }

    [BsonElement("Estado")]
    public string? Estado { get; set; }

    [BsonElement("Pais")]
    public string? Pais { get; set; }
    // ==================================================
}