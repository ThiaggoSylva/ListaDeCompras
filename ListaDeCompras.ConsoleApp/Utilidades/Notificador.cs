namespace ListaDeCompras.ConsoleApp.Utilidades;

public static class Notificador
{
    public static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public static void ExibirMensagensErro(List<string> erros)
    {
        Console.WriteLine("---------------------------------");

        Console.ForegroundColor = ConsoleColor.Red;

        foreach (string erro in erros)
            Console.WriteLine(erro);

        Console.ResetColor();
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}