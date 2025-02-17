using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class ArtistaDal
    {
        private readonly ScreenSoundContext _context;

        public ArtistaDal(ScreenSoundContext context) 
            => _context = context;

        public IEnumerable<Artista> Listar()
            => _context.Artistas.ToList();

        public void Adicionar(Artista artista)
        {
            _context.Artistas.Add(artista);
            _context.SaveChanges();            
        }

        public void Atualizar(Artista artista)
        {
            _context.Artistas.Update(artista);
            _context.SaveChanges();            
        }

        public void Deletar(Artista artista)
        {
            _context.Artistas.Remove(artista);
            _context.SaveChanges();
        }

        public Artista? RecuperarPeloNome(string nome)
            => _context.Artistas.FirstOrDefault(a => a.Nome.Equals(nome));
    }
}
