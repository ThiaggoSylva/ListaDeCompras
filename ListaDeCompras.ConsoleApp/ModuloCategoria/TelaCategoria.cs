using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.ModuloCategoria;

public class TelaCategoria : TelaBase<Categoria>, ITelaOpcoes, ITelaCrud
{
    public TelaCategoria(RepositorioCategoria repositorio) : base("Categoria", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Categorias");

        List<Categoria> categorias = repositorio.SelecionarTodos();

        if (categorias.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Não existe nenhum registro.");
            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
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
}