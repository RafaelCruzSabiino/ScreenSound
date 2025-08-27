using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.EndPoints
{
    public static class MusicasExtension
    {
        public static void AddEnspointsMusicas(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] Dal<Musica> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] Dal<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(musica);

            });

            app.MapPost("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.Nome) 
                {
                    AnoLancamento = musicaRequest.AnoLancamento
                };

                dal.Adicionar(musica);

                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] Dal<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();

            });

            app.MapPut("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] MusicaRequestEdit musicaRequest) => {
                var musicaAAtualizar = dal.RecuperarPor(a => a.Id == musicaRequest.Id);
                if (musicaAAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAAtualizar.Nome = musicaRequest.Nome;
                musicaAAtualizar.AnoLancamento = musicaRequest.AnoLancamento;

                dal.Atualizar(musicaAAtualizar);
                return Results.Ok();
            });
        }
    }
}
