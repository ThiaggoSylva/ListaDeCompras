using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class RepositorioListaCompra : RepositorioBase<ListaCompra>
{
    public override bool Excluir(string idSelecionado)
    {
        ListaCompra? listaSelecionada = SelecionarPorId(idSelecionado);

        if (listaSelecionada == null)
            return false;

        if (listaSelecionada.PossuiItensVinculados())
            return false;

        registros.Remove(listaSelecionada);

        return true;
    }
}
