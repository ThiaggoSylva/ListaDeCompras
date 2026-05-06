using System.Text.Json;
using System.Text.Json.Serialization;
using ListaDeCompras.ConsoleApp.ModuloCategoria;
using ListaDeCompras.ConsoleApp.ModuloListaCompras;
using ListaDeCompras.ConsoleApp.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;

public class ContextoJson
{
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public List<Produto> Produtos { get; set; } = new List<Produto>();
    public List<ListaCompras> ListaCompras { get; set; } = new List<ListaCompras>();

    public void Salvar()
    {
        string caminhoDiretorio = "C:\\Users\\User\\Downloads";

        string caminhoArquivo = caminhoDiretorio + "\\dados.json";

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        string caminhoDiretorio = "C:\\Users\\User\\Downloads";

        string caminhoArquivo = caminhoDiretorio + "\\dados.json";

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo = JsonSerializer.Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        this.Categorias = contextoSalvo.Categorias;
        this.Produtos = contextoSalvo.Produtos;
        this.ListaCompras = contextoSalvo.ListaCompras;
    }
}
