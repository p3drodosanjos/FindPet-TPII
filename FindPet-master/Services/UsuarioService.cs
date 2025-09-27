// Em Services/UsuarioService.cs
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class UsuarioService
{
    private readonly IMongoCollection<Usuario> _usuariosCollection;

    // O construtor recebe a configuração do appsettings.json
    public UsuarioService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        // Cria um novo cliente MongoDB com a string de conexão
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

        // Obtém uma referência ao banco de dados
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

        // Obtém uma referência à coleção "Usuarios" (se não existir, será criada)
        _usuariosCollection = mongoDatabase.GetCollection<Usuario>("Usuarios");
    }

    // Método para criar (cadastrar) um novo usuário
    public async Task CreateAsync(Usuario novoUsuario) =>
        await _usuariosCollection.InsertOneAsync(novoUsuario);

    // Você pode adicionar outros métodos aqui (Get, Update, Delete)
    // Ex: public async Task<Usuario?> GetAsync(string id) =>
    //        await _usuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}