// Adicionando os namespaces (tipos) que seu app usa.
// Se seu projeto não usa namespaces, pode remover essas linhas,
// mas é uma boa prática tê-las.

using FindPet.Settings; 
using FindPet.Interfaces; // <<< ADICIONADO: Para a ICepService
using FindPet.Services;   // <<< ADICIONADO: Para o ViaCepAdapter e UsuarioService
using FindPet.Builders; // <<< ADICIONE O NAMESPACE DO BUILDER

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

// =======================================================
// == 4. ADIÇÕES PARA O ADAPTER DE CEP ==
// =======================================================

// Habilita o uso do IHttpClientFactory (necessário para o Adapter)
builder.Services.AddHttpClient(); // <<< ADICIONADO

// Registra o Adapter:
// "Quando alguém pedir um ICepService, entregue um ViaCepAdapter"
builder.Services.AddScoped<ICepService, ViaCepAdapter>(); // <<< ADICIONADO

// =======================================================
// == 5. ADIÇÃO DO PADRÃO BUILDER ==
// =======================================================
// Registra o builder com um ciclo de vida "Scoped", 
// o que significa que um novo builder é criado a cada requisição HTTP.
builder.Services.AddScoped<IUsuarioBuilder, UsuarioBuilder>();
// =======================================================

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