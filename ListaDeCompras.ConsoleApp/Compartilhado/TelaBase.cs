namespace ListaDeCompras.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> : ITelaOpcoes where T : EntidadeBase
{
    protected readonly string nomeEntidade;
    protected readonly RepositorioBase<T> repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar");
        Console.WriteLine("2 - Editar");
        Console.WriteLine("3 - Excluir");
        Console.WriteLine("4 - Visualizar");
        Console.WriteLine("S - Voltar");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        return Console.ReadLine()?.ToUpper();
    }

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");

        T novoRegistro = ObterDados();

        string[] erros = ValidarCadastro(novoRegistro);

        if (erros.Length > 0)
        {
            ExibirErros(erros);
            return;
        }

        repositorio.Cadastrar(novoRegistro);

        ExibirMensagem($"{nomeEntidade} cadastrado com sucesso!");
    }

    public void Editar()
    {
        ExibirCabecalho($"Edição de {nomeEntidade}");

        VisualizarTodos(false);

        Console.Write("Digite o ID do registro que deseja editar: ");
        string idSelecionado = Console.ReadLine() ?? string.Empty;

        T registroAtualizado = ObterDados();

        string[] erros = ValidarEdicao(idSelecionado, registroAtualizado);

        if (erros.Length > 0)
        {
            ExibirErros(erros);
            return;
        }

        bool conseguiuEditar = repositorio.Editar(idSelecionado, registroAtualizado);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Registro não encontrado.");
            return;
        }

        ExibirMensagem($"{nomeEntidade} editado com sucesso!");
    }

    public void Excluir()
    {
        ExibirCabecalho($"Exclusão de {nomeEntidade}");

        VisualizarTodos(false);

        Console.Write("Digite o ID do registro que deseja excluir: ");
        string idSelecionado = Console.ReadLine() ?? string.Empty;

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Registro não encontrado.");
            return;
        }

        ExibirMensagem($"{nomeEntidade} excluído com sucesso!");
    }

    protected virtual string[] ValidarCadastro(T registro)
    {
        return registro.Validar();
    }

    protected virtual string[] ValidarEdicao(string idSelecionado, T registro)
    {
        return registro.Validar();
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }

    protected void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    protected void ExibirErros(string[] erros)
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

    public abstract void VisualizarTodos(bool exibirCabecalho);

    protected abstract T ObterDados();
}