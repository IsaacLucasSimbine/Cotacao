namespace Cotacao.Models;

using System.Collections.Generic;

public class Cotacao
{
    public int Id { get; set; }
    public string NumeroCotacao { get; set; } = string.Empty;
    public DateTime DataEmissao { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string ClienteLocalizacao { get; set; } = string.Empty;
    public string? ClienteNuit { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TaxaAplicavel { get; set; }
    public decimal ValorTaxa { get; set; }
    public decimal TotalGeral { get; set; }
    public string ResponsavelNome { get; set; } = string.Empty;

    public List<CotacaoArtigo> Artigos { get; set; } = new();

    // Dados da empresa (para impressão/PDF)
    public string EmpresaNome { get; set; } = string.Empty;
    public string EmpresaNuit { get; set; } = string.Empty;
    public string EmpresaEndereco { get; set; } = string.Empty;
    public string EmpresaContactos { get; set; } = string.Empty;
    public string EmpresaEmail { get; set; } = string.Empty;
    public string EmpresaBanco { get; set; } = string.Empty;
    public string LogotipoPath { get; set; } = string.Empty;
}
