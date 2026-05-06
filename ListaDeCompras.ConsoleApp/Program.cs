using System.Text.Json;
using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;
using ListaDeCompras.ConsoleApp.ModuloCategoria;
using ListaDeCompras.ConsoleApp.ModuloListaCompras;
using ListaDeCompras.ConsoleApp.ModuloProduto;
using ListaDeCompras.ConsoleApp.Utilidades;

ContextoJson contexto = new ContextoJson();

try
{
    contexto.Carregar();
}
catch (JsonException)
{
    Notificador.ExibirMensagem("O arquivo de armazenamento está corrompido! Contate o suporte.");
    return;
}

IRepositorio<Categoria> repositorioCategoria = new RepositorioCategoriaEmArquivo(contexto);
IRepositorio<Produto> repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
IRepositorio<ListaCompras> repositorioListaCompras = new RepositorioListaComprasEmArquivo(contexto);

TelaPrincipal telaPrincipal = new TelaPrincipal(
    repositorioCategoria,
    repositorioProduto,
    repositorioListaCompras
);

while (true)
{
    ITelaOpcoes? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    while (true)
    {
        string? opcaoSubMenu = telaSelecionada.ObterOpcaoMenu();

        if (opcaoSubMenu == "S")
        {
            Console.Clear();
            break;
        }

        if (telaSelecionada is ITelaCrud telaCrud)
        {
            if (opcaoSubMenu == "1")
                telaCrud.Cadastrar();

            else if (opcaoSubMenu == "2")
                telaCrud.Editar();

            else if (opcaoSubMenu == "3")
                telaCrud.Excluir();

            else if (opcaoSubMenu == "4")
                telaCrud.VisualizarTodos(deveExibirCabecalho: true);

            else if (telaCrud is TelaListaCompras telaListaCompras)
            {
                if (opcaoSubMenu == "5")
                    telaListaCompras.AdicionarItem();

                else if (opcaoSubMenu == "6")
                    telaListaCompras.RemoverItem();

                else if (opcaoSubMenu == "7")
                    telaListaCompras.VisualizarItens();
            }
        }
    }
}