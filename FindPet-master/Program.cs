// Adicionando os namespaces (tipos) que seu app usa.
// Se seu projeto não usa namespaces, pode remover essas linhas,
// mas é uma boa prática tê-las.

using FindPet.Settings; // Adicione o namespace onde MongoDbSettings está definido, se necessário

var builder = WebApplication.CreateBuilder(args);

// --- Adicionar serviços ao contêiner ---
builder.Services.AddControllersWithViews();

// Configuração do MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// *** CORREÇÃO: Registre o serviço apenas UMA VEZ ***
builder.Services.AddSingleton<UsuarioService>();

// --- Serviços do Swagger (para testar a API) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// --- Configurar o pipeline de requisições HTTP ---

// Habilitar o Swagger SOMENTE em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração para ambiente de produção
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/TelaLogin/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers(); // Essencial para suas API Controllers (como UsuariosController)

// Rota padrão aponta para TelaLogin
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TelaLogin}/{action=Index}/{id?}");

app.Run();
