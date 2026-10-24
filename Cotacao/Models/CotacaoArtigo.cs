namespace Cotacao.Models;

public class CotacaoArtigo
{
    public int Id { get; set; }
    public int CotacaoId { get; set; }
    public int NumeroItem { get; set; }
    public string Designacao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal PrecoTotal { get; set; }
}
