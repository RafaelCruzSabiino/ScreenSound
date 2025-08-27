using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Controllers
{
    public static class ArtistaExtension
    {
        public static void AddEnpointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] Dal<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);

                dal.Adicionar(artista);
                
                return Results.Ok();
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] Dal<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(a => a.Id == id);

                if (artista is null)
                {
                    return Results.NotFound();
                }

                dal.Deletar(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequest) =>
            {
                var artistaUpdate = dal.RecuperarPor(a => a.Id == artistaRequest.Id);
                if (artistaUpdate is null)
                {
                    return Results.NotFound();
                }

                artistaUpdate.Nome = artistaRequest.Nome;
                artistaUpdate.Bio = artistaRequest.Bio;

                dal.Atualizar(artistaUpdate);

                return Results.Ok();
            });
        }
    }
}
