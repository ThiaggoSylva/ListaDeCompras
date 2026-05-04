using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompras;

public class TelaListaCompras : TelaBase<ListaCompras>, ITelaOpcoes, ITelaCrud
{
    public TelaListaCompras(
        RepositorioListaCompras repositorioListaCompras
    ) : base("Lista de Compras", repositorioListaCompras)
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
