using Cotacao.Models;
using PuppeteerSharp;
using System.Text;

namespace Cotacao;

public static class PdfExporter
{
    public static async Task ExportarParaPdfAsync(Cotacao.Models.Cotacao cotacao, List<Cotacao.Models.CotacaoArtigo> artigos, string outputPath)
    {
        string templatePath = Path.Combine(AppContext.BaseDirectory, "TemplateCotacao.html");
        if (!File.Exists(templatePath))
            throw new FileNotFoundException("TemplateCotacao.html não encontrado no diretório de saída.", templatePath);

        string html = await File.ReadAllTextAsync(templatePath);

        // Substituições fixas
        html = html.Replace("{{NUMERO_COTACAO}}", cotacao.NumeroCotacao);
        html = html.Replace("{{EMPRESA_NOME}}", cotacao.EmpresaNome);
        html = html.Replace("{{EMPRESA_NUIT}}", cotacao.EmpresaNuit);
        html = html.Replace("{{EMPRESA_ENDERECO}}", cotacao.EmpresaEndereco);
        html = html.Replace("{{EMPRESA_CONTACTOS}}", cotacao.EmpresaContactos);
        html = html.Replace("{{EMPRESA_EMAIL}}", cotacao.EmpresaEmail);
        html = html.Replace("{{EMPRESA_BANCO}}", cotacao.EmpresaBanco);
        html = html.Replace("{{CLIENTE_NOME}}", cotacao.ClienteNome);
        html = html.Replace("{{CLIENTE_LOCALIZACAO}}", cotacao.ClienteLocalizacao);
        html = html.Replace("{{CLIENTE_NUIT}}", cotacao.ClienteNuit);
        html = html.Replace("{{DATA_EMISSAO}}", cotacao.DataEmissao.ToShortDateString());
        html = html.Replace("{{SUBTOTAL}}", cotacao.Subtotal.ToString("N2"));
        html = html.Replace("{{TAXA}}", cotacao.TaxaAplicavel.ToString("0") + "%");
        html = html.Replace("{{VALOR_TAXA}}", cotacao.ValorTaxa.ToString("N2"));
        html = html.Replace("{{TOTAL_GERAL}}", cotacao.TotalGeral.ToString("N2"));

        // Logótipo
        string logotipoHtml = string.IsNullOrWhiteSpace(cotacao.LogotipoPath)
            ? "<div style=\"font-weight:bold;font-size:14pt;\">LOGOTIPO</div>"
            : $"<img src=\"{cotacao.LogotipoPath}\" style=\"width:150px;height:60px;object-fit:contain;\" />";
        html = html.Replace("{{LOGOTIPO_HTML}}", logotipoHtml);

        // Linhas da tabela
        var sb = new StringBuilder();
        foreach (var artigo in artigos)
        {
            sb.AppendLine("            <tr>");
            sb.AppendLine($"                <td class=\"col-item\">{artigo.NumeroItem}</td>");
            sb.AppendLine($"                <td class=\"col-designacao\">{System.Net.WebUtility.HtmlEncode(artigo.Designacao)}</td>");
            sb.AppendLine($"                <td class=\"col-qtd\">{artigo.Quantidade}</td>");
            sb.AppendLine($"                <td class=\"col-preco\">{artigo.PrecoUnitario:N2}</td>");
            sb.AppendLine($"                <td class=\"col-total preco-total\">{artigo.PrecoTotal:N2}</td>");
            sb.AppendLine("            </tr>");
        }
        html = html.Replace("{{LINHAS_TABELA}}", sb.ToString());

        // PuppeteerSharp - usar navegador local (Edge/Chrome) sem download
        string[] caminhosPossiveis = new[]
        {
            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
            @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
            @"C:\Program Files\Google\Chrome\Application\chrome.exe",
            @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
        };

        string? caminhoEncontrado = null;
        foreach (var caminho in caminhosPossiveis)
        {
            if (File.Exists(caminho))
            {
                caminhoEncontrado = caminho;
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(caminhoEncontrado))
        {
            throw new FileNotFoundException("Não foi encontrado nenhum navegador compatível (Edge ou Chrome) instalado no sistema.");
        }

        var launchOptions = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = caminhoEncontrado
        };

        await using var browser = await Puppeteer.LaunchAsync(launchOptions);
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);
        await page.PdfAsync(outputPath, new PdfOptions
        {
            Format = PuppeteerSharp.Media.PaperFormat.A4,
            PrintBackground = true
        });
    }
}