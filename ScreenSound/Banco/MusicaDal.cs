using ScreenSound.Banco;
using ScreenSound.Modelos;

internal class MusicaDal
{
    private readonly ScreenSoundContext context;

    public MusicaDal(ScreenSoundContext context)
    {
        this.context = context;
    }

    public IEnumerable<Musica> Listar()
    {

        return context.Musicas.ToList();

    }

    public void Adicionar(Musica musica)
    {
        context.Musicas.Add(musica);
        context.SaveChanges();

    }

    public void Atualizar(Musica musica)
    {
        context.Musicas.Update(musica);
        context.SaveChanges();

    }

    public void Deletar(Musica musica)
    {
        context.Musicas.Remove(musica);
        context.SaveChanges();

    }

    public Musica? RecuperarPeloNome(string nome)
    {
        return context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));

    }
}