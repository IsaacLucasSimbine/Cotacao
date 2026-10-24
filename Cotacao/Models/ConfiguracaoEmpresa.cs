namespace Cotacao.Models;

public class ConfiguracaoEmpresa
{
    public int Id { get; set; }
    public string NomeEmpresa { get; set; } = string.Empty;
    public string Nuit { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Contactos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string InformacaoBancaria { get; set; } = string.Empty;
    public string LogotipoPath { get; set; } = string.Empty;
}
