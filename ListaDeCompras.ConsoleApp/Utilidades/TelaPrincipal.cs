using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;
using ListaDeCompras.ConsoleApp.ModuloProduto;
using ListaDeCompras.ConsoleApp.ModuloListaCompra;

namespace ListaDeCompras.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
    private RepositorioProduto repositorioProduto = new RepositorioProduto();
    private RepositorioListaCompra repositorioListaCompra = new RepositorioListaCompra();
    public TelaPrincipal()
    {
        Categoria categoria = new Categoria("Compras do Mês", CorCategoria.Vermelha);
        repositorioCategoria.Cadastrar(categoria);

        Produto produto = new Produto("Arroz", categoria, "kg", 25.90m);
        repositorioProduto.Cadastrar(produto);
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
        Console.WriteLine("4 - Gerenciar itens de listas de compras");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCategoria(repositorioCategoria);

        if (opcaoMenuPrincipal == "2")
            return new TelaProduto(repositorioProduto, repositorioCategoria);

        if (opcaoMenuPrincipal == "3")
            return new TelaListaCompra(repositorioListaCompra);

        return null;
    }
}