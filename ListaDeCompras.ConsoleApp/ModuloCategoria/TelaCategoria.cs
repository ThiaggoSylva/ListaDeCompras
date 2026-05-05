using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloProduto;
using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.ModuloCategoria;

public class TelaCategoria : TelaBase<Categoria>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public TelaCategoria(
        RepositorioCategoriaEmArquivo repositorioCategoria,
        RepositorioProdutoEmArquivo repositorioProduto
    ) : base("Categoria", repositorioCategoria)
    {
        this.repositorioProduto = repositorioProduto;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Categorias");

        List<Categoria> categorias = repositorio.SelecionarTodos();

        if (categorias.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhuma item registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10}",
            "Id", "Nome", "Cor"
        );

        foreach (Categoria c in categorias)
        {
            CorCategoria corSelecionada = c.Cor;

            if (corSelecionada == CorCategoria.Vermelha)
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == CorCategoria.Verde)
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == CorCategoria.Azul)
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10}",
                c.Id, c.Nome, c.Cor
            );
        }

        Console.ResetColor();

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Categoria ObterDadosCadastrais()
    {
        Console.Write("Digite o nome da categoria: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Selecione uma cor válida para a categoria");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Branca (Padrão)");
        Console.WriteLine("2 - Vermelha");
        Console.WriteLine("3 - Verde");
        Console.WriteLine("4 - Azul");
        Console.WriteLine("---------------------------------");
        Console.Write("Digite a cor da categoria: ");
        string cor = Console.ReadLine() ?? string.Empty;

        CorCategoria corSelecionada = CorCategoria.Branca;

        if (cor == "2")
            corSelecionada = CorCategoria.Vermelha;
        else if (cor == "3")
            corSelecionada = CorCategoria.Verde;
        else if (cor == "4")
            corSelecionada = CorCategoria.Azul;

        return new Categoria(nome, corSelecionada);
    }

    protected override List<string> ValidarRegistroDuplicado(Categoria novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = new List<string>();

        List<Categoria> categorias = repositorio.SelecionarTodos();

        foreach (Categoria c in categorias)
        {
            if (c.Id != idIgnorado && c.Nome == novaEntidade.Nome)
            {
                erros.Add($"Já existe uma categoria com o nome \"{novaEntidade.Nome}\"");
                break;
            }
        }

        return erros;
    }

    protected override List<string> ValidarExclusaoRegistro(Categoria registro)
    {
        List<string> erros = new List<string>();

        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        foreach (Produto p in produtos)
        {
            if (p.Categoria == registro)
            {
                erros.Add("Não é possível excluir uma categoria com produtos cadastrados.");
                break;
            }
        }

        return erros;
    }
}
