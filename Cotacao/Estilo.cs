using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Cotacao;

internal static class Tema
{
    public static readonly Color Fundo = Color.FromArgb(244, 246, 251);
    public static readonly Color FundoSuave = Color.FromArgb(248, 250, 252);
    public static readonly Color Borda = Color.FromArgb(226, 232, 240);
    public static readonly Color BordaInput = Color.FromArgb(203, 213, 225);
    public static readonly Color Texto = Color.FromArgb(30, 41, 59);
    public static readonly Color TextoSuave = Color.FromArgb(100, 116, 139);

    // Para mudar a cor da app, altera só estas quatro
    public static readonly Color Primaria = Color.FromArgb(79, 70, 229);
    public static readonly Color PrimariaHover = Color.FromArgb(67, 56, 202);
    public static readonly Color PrimariaEscura = Color.FromArgb(55, 48, 163);
    public static readonly Color PrimariaSuave = Color.FromArgb(224, 231, 255);

    public static readonly Font Normal = new("Segoe UI", 10f);
    public static readonly Font Rotulo = new("Segoe UI", 9f);
    public static readonly Font Cabecalho = new("Segoe UI Semibold", 9.5f);
    public static readonly Font Negrito = new("Segoe UI Semibold", 10f);
    public static readonly Font Titulo = new("Segoe UI Semibold", 12.5f);
    public static readonly Font Valor = new("Segoe UI Semibold", 13f);
    public static readonly Font Grande = new("Segoe UI Semibold", 18f);
    public static readonly Color Perigo = Color.FromArgb(220, 38, 38);
}

internal static class Desenho
{
    public static GraphicsPath Arredondado(RectangleF r, float raio)
    {
        float d = raio * 2;
        var p = new GraphicsPath();
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }
}

/// <summary>Cartão branco com cantos arredondados.</summary>
internal class Cartao : Panel
{
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color CorFundo { get; set; } = Color.White;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color CorBorda { get; set; } = Tema.Borda;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Raio { get; set; } = 14;

    public Cartao()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Tema.Fundo;               // cor que aparece nos cantos
        Padding = new Padding(20, 16, 20, 10);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
        using var caminho = Desenho.Arredondado(r, Raio);
        using var pincel = new SolidBrush(CorFundo);
        using var caneta = new Pen(CorBorda);
        e.Graphics.FillPath(pincel, caminho);
        e.Graphics.DrawPath(caneta, caminho);
    }
}

/// <summary>Envolve uma TextBox com borda arredondada que muda de cor ao focar.</summary>
internal class CampoTexto : Panel
{
    private readonly TextBox _caixa;
    private bool _foco;

    public CampoTexto(TextBox caixa)
    {
        _caixa = caixa;
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
        Padding = new Padding(12, 6, 12, 6);

        caixa.BorderStyle = BorderStyle.None;
        caixa.BackColor = Color.White;
        caixa.ForeColor = Tema.Texto;
        caixa.Font = Tema.Normal;
        caixa.Enter += (_, _) => { _foco = true; Invalidate(); };
        caixa.Leave += (_, _) => { _foco = false; Invalidate(); };
        Controls.Add(caixa);

        Click += (_, _) => caixa.Focus();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
        if (_caixa.Multiline)
            _caixa.SetBounds(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);
        else
            _caixa.SetBounds(Padding.Left, (Height - _caixa.PreferredHeight) / 2,
                             Width - Padding.Horizontal, _caixa.PreferredHeight);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var r = new RectangleF(1f, 1f, Width - 3f, Height - 3f);
        using var caminho = Desenho.Arredondado(r, 10);
        using var caneta = new Pen(_foco ? Tema.Primaria : Tema.BordaInput, _foco ? 2f : 1f);
        e.Graphics.DrawPath(caneta, caminho);
    }
}

/// <summary>Botão plano e arredondado, com efeito ao passar o rato.</summary>
internal class BotaoModerno : Button
{
    private bool _hover, _pressionado;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Primario { get; set; } = true;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Perigo { get; set; }
    public BotaoModerno()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Cursor = Cursors.Hand;
        Font = Tema.Negrito;
        Size = new Size(150, 42);
        DoubleBuffered = true;
    }

    protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _hover = false; _pressionado = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { _pressionado = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { _pressionado = false; Invalidate(); base.OnMouseUp(e); }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? Tema.Fundo);

        Color fundo, texto, borda;
        if (Perigo)
        {
            fundo = _pressionado ? Color.FromArgb(254, 226, 226)
                  : _hover ? Color.FromArgb(254, 242, 242) : Color.White;
            texto = Tema.Perigo;
            borda = Tema.Perigo;
        }
        else if (Primario)
        {
            fundo = _pressionado ? Tema.PrimariaEscura : _hover ? Tema.PrimariaHover : Tema.Primaria;
            texto = Color.White;
            borda = fundo;
        }
        else
        {
            fundo = _pressionado ? Color.FromArgb(226, 232, 240)
                  : _hover ? Color.FromArgb(241, 245, 249) : Color.White;
            texto = Tema.Texto;
            borda = Tema.BordaInput;
        }

        var r = new RectangleF(0.5f, 0.5f, Width - 2f, Height - 2f);
        using var caminho = Desenho.Arredondado(r, 10);
        using var pincel = new SolidBrush(fundo);
        using var caneta = new Pen(borda);
        g.FillPath(pincel, caminho);
        g.DrawPath(caneta, caminho);

        TextRenderer.DrawText(g, Text, Font, ClientRectangle, texto,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
            TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
    }
}