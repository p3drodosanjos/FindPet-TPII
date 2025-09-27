// Seu Program.cs completo e corrigido

var builder = WebApplication.CreateBuilder(args);

// 2. Adicionar todos os serviços necessários
builder.Services.AddControllersWithViews();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<UsuarioService>();

// --- Serviços do Swagger ---
builder.Services.AddEndpointsApiExplorer(); // <-- ADICIONE AQUI 1
builder.Services.AddSwaggerGen();           // <-- ADICIONE AQUI 2


var app = builder.Build();

// 4. Configurar o pipeline de requisições HTTP

// --- Habilitar o Swagger SOMENTE em ambiente de desenvolvimento ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();           // <-- ADICIONE AQUI 3
    app.UseSwaggerUI();         // <-- ADICIONE AQUI 4 (UseSwaggerUI já vem dentro do 'if')
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
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TelaLogin}/{action=Index}/{id?}");

app.Run();