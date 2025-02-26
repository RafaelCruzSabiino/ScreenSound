using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<Dal<Artista>>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] Dal<Artista> dal, string nome) =>
{
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

    if(artista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] Artista artista) => 
{
    dal.Adicionar(artista);
    return Results.Ok();
});

app.Run();
