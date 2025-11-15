// Em Services/UsuarioService.cs
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using FindPet.Settings;
// Referencie o seu modelo de Usuario (se estiver em outro namespace, ajuste)
// using FindPet.Models; 

public class UsuarioService
{
    private readonly IMongoCollection<Usuario> _usuariosCollection;

    public UsuarioService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _usuariosCollection = mongoDatabase.GetCollection<Usuario>(mongoDbSettings.Value.CollectionName);
    }

    // MÉTODO DE CRIAR (POST) - Este você já tinha
    public async Task CreateAsync(Usuario novoUsuario) =>
        await _usuariosCollection.InsertOneAsync(novoUsuario);

    // ==================================================
    // == ADICIONE ESTES DOIS NOVOS MÉTODOS AQUI ==
    // ==================================================

    // MÉTODO PARA BUSCAR UM (GET por ID)
    public async Task<Usuario?> GetAsync(string id) =>
        await _usuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // MÉTODO PARA ATUALIZAR (PUT)
    public async Task UpdateAsync(string id, Usuario usuarioAtualizado) =>
        await _usuariosCollection.ReplaceOneAsync(x => x.Id == id, usuarioAtualizado);

}