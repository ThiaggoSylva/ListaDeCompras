using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompras;

public class TelaListaCompras : TelaBase<ListaCompras>, ITelaOpcoes, ITelaCrud
{
    public TelaListaCompras(
        RepositorioListaCompras repositorioListaCompras
    ) : base("Lista de Compras", repositorioListaCompras)
    {
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Lista de Compras");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar lista de compras");
        Console.WriteLine($"2 - Editar  lista de compras");
        Console.WriteLine($"3 - Excluir lista de compras");
        Console.WriteLine($"4 - Visualizar listas de compras");
        Console.WriteLine($"5 - Adicionar item à lista de compras");
        Console.WriteLine($"6 - Remover item da lista de compras");
        Console.WriteLine($"7 - Visualizar itens de listas de compras");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void AdicionarItem()
    {

    }

    public void RemoverItem()
    {

    }

    public void VisualizarItens()
    {

    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Listas de Compras");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
            "Id", "Nome", "Criação", "Qtd. Itens", "Total Gasto (R$)"
        );

        List<ListaCompras> listas = repositorio.SelecionarTodos();

        foreach (ListaCompras l in listas)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
                l.Id, l.Nome, l.DataCriacao.ToShortDateString(), 0, 0.0m.ToString("C2")
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override ListaCompras ObterDadosCadastrais()
    {
        Console.Write("Digite o nome da lista: ");
        string nome = Console.ReadLine() ?? string.Empty;

        return new ListaCompras(nome);
    }
}
