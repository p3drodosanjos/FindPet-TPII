// Em Models/Usuario.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Usuario
{
    [BsonId] // Atributo para marcar esta propriedade como a chave primária do documento
    [BsonRepresentation(BsonType.ObjectId)] // Para tratar como um ObjectId do Mongo
    public string? Id { get; set; }

    [BsonElement("Nome")] // O nome do campo no documento MongoDB
    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;
    
    public string Cpf { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string DatadeNascimento { get; set; } = null!;
}