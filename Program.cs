using Microsoft.OpenApi.Models;
using ReceitasCRUD.Data;
using ReceitasCRUD.Routes;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer(); // Necessário para explorar endpoints da API
builder.Services.AddScoped<Context>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API - WeatherForecast",
        Version = "v1"
    });
});

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Rotas da aplicação
app.ReceitasRoutes();
app.IngredienteRoutes();

app.UseHttpsRedirection();



app.Run();
