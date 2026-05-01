using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.ModuloProduto;

public class TelaProduto : TelaBase<Produto>
{
    private readonly RepositorioProduto repositorioProduto;
    private readonly RepositorioCategoria repositorioCategoria;

    public TelaProduto(
        RepositorioProduto repositorioProduto,
        RepositorioCategoria repositorioCategoria
    ) : base("Produto", repositorioProduto)
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    protected override Produto ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine() ?? "";

        Categoria categoria = SelecionarCategoria();

        Console.Write("Digite a unidade de medida: ");
        string unidadeMedida = Console.ReadLine() ?? "";

        Console.Write("Digite o preço aproximado: ");
        decimal.TryParse(Console.ReadLine(), out decimal precoAproximado);

        return new Produto(nome, categoria, unidadeMedida, precoAproximado);
    }

    public override void VisualizarTodos(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho("Visualização de Produtos");

        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("{0, -8} | {1, -25} | {2, -20} | {3, -15} | {4, -10}",
            "ID", "Nome", "Categoria", "Unidade", "Preço");

        foreach (Produto produto in produtos)
        {
            Console.WriteLine("{0, -8} | {1, -25} | {2, -20} | {3, -15} | R$ {4:F2}",
                produto.Id,
                produto.Nome,
                produto.Categoria.Nome,
                produto.UnidadeMedida,
                produto.PrecoAproximado);
        }

        Console.ReadLine();
    }

    protected override string[] ValidarCadastro(Produto produto)
    {
        List<string> erros = produto.Validar().ToList();

        if (produto.Categoria != null &&
            repositorioProduto.ExisteProdutoComMesmoNomeNaCategoria(produto.Nome, produto.Categoria))
        {
            erros.Add("Já existe um produto com este nome nesta categoria.");
        }

        return erros.ToArray();
    }

    protected override string[] ValidarEdicao(string idSelecionado, Produto produto)
    {
        List<string> erros = produto.Validar().ToList();

        if (produto.Categoria != null &&
            repositorioProduto.ExisteProdutoComMesmoNomeNaCategoria(produto.Nome, produto.Categoria, idSelecionado))
        {
            erros.Add("Já existe um produto com este nome nesta categoria.");
        }

        return erros.ToArray();
    }

    private Categoria SelecionarCategoria()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Categorias cadastradas:");
        Console.WriteLine("---------------------------------");

        foreach (Categoria categoria in categorias)
            Console.WriteLine($"{categoria.Id} - {categoria.Nome}");

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o ID da categoria: ");
        string idCategoria = Console.ReadLine() ?? "";

        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(idCategoria);

        return categoriaSelecionada!;
    }
}