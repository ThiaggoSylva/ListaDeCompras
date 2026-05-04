using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;
using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.ModuloProduto;

public class TelaProduto : TelaBase<Produto>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioCategoria repositorioCategoria;

    public TelaProduto(
        RepositorioProduto repositorioProduto,
        RepositorioCategoria repositorioCategoria
    ) : base("Produto", repositorioProduto)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Produtos");

        List<Produto> produtos = repositorio.SelecionarTodos();

        if (produtos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum item registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -15}",
            "Id", "Nome", "Medida", "Preço Aproximado", "Categoria"
        );

        foreach (Produto p in produtos)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -15}",
                p.Id, p.Nome, p.UnidadeMedida, p.PrecoAproximado.ToString("C2"), p.Categoria.Nome
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Produto ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a unidade de medida do produto (ex: 2 lt, 5 kg): ");
        string unidadeMedida = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o preço aproximado do produto em R$: ");
        decimal precoAproximado = Convert.ToDecimal(Console.ReadLine());

        Categoria? categoriaSelecionada;

        do
        {
            Console.WriteLine("---------------------------------");
            VisualizarCategorias();
            Console.WriteLine("---------------------------------");

            Console.Write("Digite o Id da categoria do produto: ");
            string idSelecionado = Console.ReadLine() ?? string.Empty;

            categoriaSelecionada = repositorioCategoria.SelecionarPorId(idSelecionado);

        } while (categoriaSelecionada == null);

        return new Produto(nome, unidadeMedida, precoAproximado, categoriaSelecionada);
    }

    private void VisualizarCategorias()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        if (categorias.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhuma categoria registrada.");
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
    }
}
