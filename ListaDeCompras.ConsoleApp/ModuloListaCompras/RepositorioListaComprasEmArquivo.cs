using ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompras;

public class RepositorioListaComprasEmArquivo : RepositorioBaseEmArquivo<ListaCompras>
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaCompras> CarregarRegistros()
    {
        return contexto.ListaCompras;
    }
}
