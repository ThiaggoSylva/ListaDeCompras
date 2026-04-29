using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Utilidades;

TelaPrincipal telaPrincipal = new TelaPrincipal();

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
            break;

        if (telaSelecionada is TelaBase<EntidadeBase>)
            continue;

        dynamic telaBase = telaSelecionada;

        if (opcaoSubMenu == "1")
            telaBase.Cadastrar();
        else if (opcaoSubMenu == "2")
            telaBase.Editar();
        else if (opcaoSubMenu == "3")
            telaBase.Excluir();
        else if (opcaoSubMenu == "4")
            telaBase.VisualizarTodos(true);
    }
}