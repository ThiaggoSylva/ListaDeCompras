using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase
{
    public string nomeEntidade = string.Empty;
    protected RepositorioBase<T> repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public virtual string? ObterOpcaoMenu()
    {
        string nomeMinusculo = nomeEntidade.ToLower();

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar {nomeMinusculo}");
        Console.WriteLine($"2 - Editar {nomeMinusculo}");
        Console.WriteLine($"3 - Excluir {nomeMinusculo}");
        Console.WriteLine($"4 - Visualizar {nomeMinusculo}s");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");

        T novaEntidade = ObterDadosCadastrais();

        List<string> erros = novaEntidade.Validar();

        if (erros.Count > 0)
        {
            Notificador.ExibirMensagensErro(erros);

            Cadastrar();
            return;
        }

        repositorio.Cadastrar(novaEntidade);

        Notificador.ExibirMensagem($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso!");
    }

    public void Editar()
    {
        ExibirCabecalho($"Edição de {nomeEntidade}");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar (ou S para sair): ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado == "S")
                return;

            if (idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        T novaEntidade = ObterDadosCadastrais();

        List<string> erros = novaEntidade.Validar();

        if (erros.Count > 0)
        {
            Notificador.ExibirMensagensErro(erros);

            Editar();
            return;
        }

        bool conseguiuEditar = repositorio.Editar(idSelecionado, novaEntidade);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        Notificador.ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }

    public void Excluir()
    {
        ExibirCabecalho("Exclusão de Caixa");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir (ou S para sair): ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.ToUpper() == "S")
                return;

            if (idSelecionado.Length == 7)
                break;
        } while (true);

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        Notificador.ExibirMensagem($"O registro \"{idSelecionado}\" foi excluído com sucesso.");
    }

    public abstract void VisualizarTodos(bool deveExibirCabecalho);

    protected abstract T ObterDadosCadastrais();

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }
}
