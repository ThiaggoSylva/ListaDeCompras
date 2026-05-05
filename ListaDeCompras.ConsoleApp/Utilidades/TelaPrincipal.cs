using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;
using ListaDeCompras.ConsoleApp.ModuloListaCompras;
using ListaDeCompras.ConsoleApp.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly RepositorioCategoriaEmArquivo repositorioCategoria;
    private readonly RepositorioProdutoEmArquivo repositorioProduto;
    private readonly RepositorioListaComprasEmArquivo repositorioListaCompras;

    public TelaPrincipal(
        RepositorioCategoriaEmArquivo repositorioCategoria,
        RepositorioProdutoEmArquivo repositorioProduto,
        RepositorioListaComprasEmArquivo repositorioListaCompras
    )
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioProduto = repositorioProduto;
        this.repositorioListaCompras = repositorioListaCompras;
    }

    public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Lista de Compras");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar categorias");
        Console.WriteLine("2 - Gerenciar produtos");
        Console.WriteLine("3 - Gerenciar listas de compras");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCategoria(repositorioCategoria, repositorioProduto);

        if (opcaoMenuPrincipal == "2")
            return new TelaProduto(repositorioProduto, repositorioCategoria, repositorioListaCompras);

        if (opcaoMenuPrincipal == "3")
            return new TelaListaCompras(repositorioListaCompras, repositorioProduto);

        return null;
    }
}