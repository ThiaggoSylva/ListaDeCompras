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
    private TelaProduto telaProduto;
    private TelaListaCompra telaListaCompra;

    public TelaPrincipal()
    {
        Categoria categoria = new Categoria("Compras do Mês", CorCategoria.Vermelha);
        repositorioCategoria.Cadastrar(categoria);

        telaProduto = new TelaProduto(repositorioProduto, repositorioCategoria);

        telaListaCompra = new TelaListaCompra(repositorioListaCompra, telaProduto);

        Produto produto = new Produto("Arroz", categoria, "kg", 25.90m);
        repositorioProduto.Cadastrar(produto);

        Produto feijao = new Produto("Feijão", categoria, "kg", 8.50m);
        Produto leite = new Produto("Leite", categoria, "litro", 4.75m);

        repositorioProduto.Cadastrar(feijao);
        repositorioProduto.Cadastrar(leite);
   
        ListaCompra lista = new ListaCompra("Lista de Teste", StatusListaCompra.Aberta);

        repositorioListaCompra.Cadastrar(lista);
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
            return new TelaListaCompra(repositorioListaCompra, telaProduto);

        if (opcaoMenuPrincipal == "4")
        {
            telaListaCompra.GerenciarItens();
            return ApresentarMenuOpcoesPrincipal();
        }

        if (opcaoMenuPrincipal == "S")
            return null;

        return ApresentarMenuOpcoesPrincipal();
    }
}