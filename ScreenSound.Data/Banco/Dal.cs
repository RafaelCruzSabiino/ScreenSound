using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    public class Dal<T> where T : class
    {
        protected readonly ScreenSoundContext context;

        public Dal(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> Listar()
            => context.Set<T>().ToList();

        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao)
            => context.Set<T>().FirstOrDefault(condicao);

        public IEnumerable<T> ListarPor(Func<T, bool> condicao)
            => context.Set<T>().Where(condicao);
    }
}
