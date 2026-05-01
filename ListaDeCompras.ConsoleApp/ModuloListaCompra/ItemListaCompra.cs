using ListaDeCompras.ConsoleApp.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class ItemListaCompra
{
    public Produto Produto { get; }
    public int Quantidade { get; }

    public ItemListaCompra(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }

    public decimal CalcularTotal()
    {
        return Produto.PrecoAproximado * Quantidade;
    }
}