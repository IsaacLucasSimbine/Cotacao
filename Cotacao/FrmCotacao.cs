namespace Cotacao;

public partial class FrmCotacao : Form
{
    // Empresa
    private TextBox txtEmpresaNome = null!;
    private TextBox txtEmpresaEndereco = null!;
    private TextBox txtEmpresaNuit = null!;
    private TextBox txtEmpresaContactos = null!;
    private TextBox txtEmpresaEmail = null!;
    private TextBox txtEmpresaBanco = null!;

    // Documento
    private TextBox txtNumeroCotacao = null!;
    private DateTimePicker dtpDataEmissao = null!;
    private TextBox txtResponsavel = null!;

    // Cliente
    private TextBox txtClienteNome = null!;
    private TextBox txtClienteLocalizacao = null!;
    private TextBox txtClienteNuit = null!;

    // Totais
    private TextBox txtSubtotal = null!;
    private ComboBox cmbTaxa = null!;
    private TextBox txtValorTaxa = null!;
    private TextBox txtTotalGeral = null!;

    // Artigos
    private Panel pnlArtigos = null!;
    private DataGridView dgvArtigos = null!;

    // Botões
    private Button btnGuardar = null!;
    private Button btnCancelar = null!;
    private Button btnVerHistorico = null!;
    private Button btnImprimir = null!;

    private System.Drawing.Printing.PrintDocument documentoImpressao = new System.Drawing.Printing.PrintDocument();
    private System.Windows.Forms.PrintPreviewDialog previewDialog = new System.Windows.Forms.PrintPreviewDialog();

    public FrmCotacao()
    {
        InitializeComponent();
        documentoImpressao.PrintPage += documentoImpressao_PrintPage;
        InicializarBaseDados();
        BuildUI();
        ConfigurarGrelhaArtigos();
        dgvArtigos.EditingControlShowing += dgvArtigos_EditingControlShowing;
        GerarProximoNumeroCotacao();
        AtualizarTotais();
    }


        private void BuildUI()
    {
        SuspendLayout();

        Text = "Cotações";
        Font = Tema.Normal;
        BackColor = Tema.Fundo;
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1150, 700);
        var area = Screen.FromControl(this).WorkingArea;
        ClientSize = new Size(Math.Min(1280, area.Width - 40), Math.Min(860, area.Height - 80));
        Controls.Clear();

        // ── Campos (mesmos nomes de sempre) ──
        txtEmpresaNome = new TextBox { Name = "txtEmpresaNome", PlaceholderText = "Ex.: Maputo Serviços, Lda" };
        txtEmpresaEndereco = new TextBox { Name = "txtEmpresaEndereco", PlaceholderText = "Rua, bairro, cidade" };
        txtEmpresaNuit = new TextBox { Name = "txtEmpresaNuit", PlaceholderText = "9 dígitos" };
        txtEmpresaContactos = new TextBox { Name = "txtEmpresaContactos", PlaceholderText = "+258 84 000 0000" };
        txtEmpresaEmail = new TextBox { Name = "txtEmpresaEmail", PlaceholderText = "geral@empresa.co.mz" };
        txtEmpresaBanco = new TextBox { Name = "txtEmpresaBanco", Multiline = true, ScrollBars = ScrollBars.Vertical };

        txtNumeroCotacao = new TextBox { Name = "txtNumeroCotacao" };
        dtpDataEmissao = new DateTimePicker { Name = "dtpDataEmissao", Format = DateTimePickerFormat.Short };
        txtResponsavel = new TextBox { Name = "txtResponsavel", PlaceholderText = "Quem emite a cotação" };

        txtClienteNome = new TextBox { Name = "txtClienteNome", PlaceholderText = "Nome do cliente ou empresa" };
        txtClienteLocalizacao = new TextBox { Name = "txtClienteLocalizacao", PlaceholderText = "Cidade / província" };
        txtClienteNuit = new TextBox { Name = "txtClienteNuit", PlaceholderText = "NUIT do cliente" };

        txtSubtotal = new TextBox { Name = "txtSubtotal" };
        txtValorTaxa = new TextBox { Name = "txtValorTaxa" };
        txtTotalGeral = new TextBox { Name = "txtTotalGeral" };
        cmbTaxa = new ComboBox
        {
            Name = "cmbTaxa",
            DropDownStyle = ComboBoxStyle.DropDownList,
            FlatStyle = FlatStyle.Flat,
            Font = Tema.Valor,
            BackColor = Color.White
        };
        cmbTaxa.Items.AddRange(new object[] { "0%", "3%", "16%" });
        cmbTaxa.SelectedIndex = 0;
        cmbTaxa.SelectedIndexChanged += (s, e) => AtualizarTotais();

        // ── Cabeçalho ──
        var cabecalho = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0) };
        cabecalho.Controls.Add(new Label
        {
            Text = "Nova cotação",
            Font = Tema.Grande,
            ForeColor = Tema.Texto,
            BackColor = Color.White,
            AutoSize = true,
            Location = new Point(28, 14)
        });
        cabecalho.Controls.Add(new Label
        {
            Text = "Preenche os dados, adiciona os artigos e guarda ou exporta em PDF.",
            Font = Tema.Normal,
            ForeColor = Tema.TextoSuave,
            BackColor = Color.White,
            AutoSize = true,
            Location = new Point(30, 50)
        });
        cabecalho.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 4, BackColor = Tema.Primaria });
        cabecalho.Paint += (s, e) =>
        {
            using var caneta = new Pen(Tema.Borda);
            e.Graphics.DrawLine(caneta, 0, cabecalho.Height - 1, cabecalho.Width, cabecalho.Height - 1);
        };

        // ── Cartão Empresa ──
        var tabEmpresa = Tabela(1, 6);
        for (int i = 0; i < 5; i++)
            tabEmpresa.RowStyles[i] = new RowStyle(SizeType.Absolute, 70);
        tabEmpresa.Controls.Add(Campo("Nome da empresa", txtEmpresaNome), 0, 0);
        tabEmpresa.Controls.Add(Campo("Endereço", txtEmpresaEndereco), 0, 1);
        tabEmpresa.Controls.Add(Campo("NUIT", txtEmpresaNuit), 0, 2);
        tabEmpresa.Controls.Add(Campo("Contactos", txtEmpresaContactos), 0, 3);
        tabEmpresa.Controls.Add(Campo("Email", txtEmpresaEmail), 0, 4);
        tabEmpresa.Controls.Add(Campo("Dados bancários (banco, conta, NIB)", txtEmpresaBanco), 0, 5);
        var cartaoEmpresa = CriarCartao("Empresa", tabEmpresa);
        cartaoEmpresa.Margin = new Padding(0, 0, 16, 0);

        // ── Cartão Documento ──
        var tabDoc = Tabela(2, 2);
        var campoNumero = Campo("Número de cotação", txtNumeroCotacao);
        tabDoc.Controls.Add(campoNumero, 0, 0);
        tabDoc.SetColumnSpan(campoNumero, 2);
        tabDoc.Controls.Add(Campo("Data de emissão", dtpDataEmissao, direita: 8), 0, 1);
        tabDoc.Controls.Add(Campo("Responsável", txtResponsavel, esquerda: 8), 1, 1);
        var cartaoDoc = CriarCartao("Documento", tabDoc);
        cartaoDoc.Margin = new Padding(0, 0, 8, 16);

        // ── Cartão Cliente ──
        var tabCliente = Tabela(2, 2);
        var campoNomeCliente = Campo("Nome do cliente", txtClienteNome);
        tabCliente.Controls.Add(campoNomeCliente, 0, 0);
        tabCliente.SetColumnSpan(campoNomeCliente, 2);
        tabCliente.Controls.Add(Campo("Localização", txtClienteLocalizacao, direita: 8), 0, 1);
        tabCliente.Controls.Add(Campo("NUIT", txtClienteNuit, esquerda: 8), 1, 1);
        var cartaoCliente = CriarCartao("Cliente", tabCliente);
        cartaoCliente.Margin = new Padding(8, 0, 0, 16);

        var cartoesTopo = Tabela(2, 1, Tema.Fundo);
        cartoesTopo.Controls.Add(cartaoDoc, 0, 0);
        cartoesTopo.Controls.Add(cartaoCliente, 1, 0);

        // ── Cartão Artigos ──
        pnlArtigos = new Panel { Name = "pnlArtigos", Dock = DockStyle.Fill, BackColor = Color.White };

        var btnAdicionarLinha = CriarBotao("+  Adicionar artigo", false, 160, 34);
        var btnRemoverLinha = CriarBotao("Remover linha", false, 130, 34);
        btnAdicionarLinha.Click += (s, e) =>
        {
            dgvArtigos.Focus();
            int nova = dgvArtigos.NewRowIndex;
            if (nova >= 0)
            {
                dgvArtigos.CurrentCell = dgvArtigos.Rows[nova].Cells["designacao"];
                dgvArtigos.BeginEdit(true);
            }
        };
        btnRemoverLinha.Click += (s, e) =>
        {
            var linha = dgvArtigos.CurrentRow;
            if (linha != null && !linha.IsNewRow)
                dgvArtigos.Rows.Remove(linha);
        };

        var barraArtigos = Tabela(3, 1);
        barraArtigos.Dock = DockStyle.Top;
        barraArtigos.Height = 44;
        barraArtigos.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
        barraArtigos.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
        barraArtigos.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
        barraArtigos.Controls.Add(new Label
        {
            Text = "Artigos",
            Dock = DockStyle.Fill,
            Font = Tema.Titulo,
            ForeColor = Tema.Texto,
            BackColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        barraArtigos.Controls.Add(btnRemoverLinha, 1, 0);
        barraArtigos.Controls.Add(btnAdicionarLinha, 2, 0);

        var cartaoArtigos = new Cartao { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 16) };
        cartaoArtigos.Controls.Add(pnlArtigos);   // Fill primeiro
        cartaoArtigos.Controls.Add(barraArtigos); // depois o Top

        // ── Cartão Resumo (totais) ──
        var cartaoTotal = new Cartao
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            CorFundo = Tema.Primaria,
            CorBorda = Tema.Primaria,
            Raio = 12,
            Padding = new Padding(16, 8, 16, 8),
            Margin = new Padding(0)
        };
        txtTotalGeral.ReadOnly = true;
        txtTotalGeral.TabStop = false;
        txtTotalGeral.BorderStyle = BorderStyle.None;
        txtTotalGeral.BackColor = Tema.Primaria;
        txtTotalGeral.ForeColor = Color.White;
        txtTotalGeral.Font = Tema.Grande;
        txtTotalGeral.Dock = DockStyle.Top;
        var lblTotal = new Label
        {
            Text = "TOTAL GERAL",
            Dock = DockStyle.Top,
            Height = 22,
            Font = Tema.Rotulo,
            ForeColor = Color.FromArgb(199, 210, 254),
            BackColor = Tema.Primaria
        };
        cartaoTotal.Controls.Add(txtTotalGeral);
        cartaoTotal.Controls.Add(lblTotal);

        var tabResumo = Tabela(5, 1);
        tabResumo.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
        tabResumo.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 170);
        tabResumo.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 130);
        tabResumo.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 170);
        tabResumo.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 240);
        tabResumo.Controls.Add(new Label
        {
            Text = "Os totais são calculados automaticamente.",
            Dock = DockStyle.Fill,
            Font = Tema.Rotulo,
            ForeColor = Tema.TextoSuave,
            BackColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        tabResumo.Controls.Add(Valor("Subtotal", txtSubtotal), 1, 0);
        tabResumo.Controls.Add(Valor("Taxa (IVA)", cmbTaxa), 2, 0);
        tabResumo.Controls.Add(Valor("Valor da taxa", txtValorTaxa), 3, 0);
        tabResumo.Controls.Add(cartaoTotal, 4, 0);

        var cartaoResumo = new Cartao
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 14, 20, 14),
            Margin = new Padding(0)
        };
        cartaoResumo.Controls.Add(tabResumo);

        // ── Coluna da direita ──
        var colunaDireita = Tabela(1, 3, Tema.Fundo);
        colunaDireita.RowStyles[0] = new RowStyle(SizeType.Absolute, 218);
        colunaDireita.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
        colunaDireita.RowStyles[2] = new RowStyle(SizeType.Absolute, 104);
        colunaDireita.Controls.Add(cartoesTopo, 0, 0);
        colunaDireita.Controls.Add(cartaoArtigos, 0, 1);
        colunaDireita.Controls.Add(cartaoResumo, 0, 2);

        var conteudo = Tabela(2, 1, Tema.Fundo);
        conteudo.Padding = new Padding(24, 20, 24, 20);
        conteudo.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 390);
        conteudo.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
        conteudo.Controls.Add(cartaoEmpresa, 0, 0);
        conteudo.Controls.Add(colunaDireita, 1, 0);

        // ── Rodapé com os botões ──
        btnVerHistorico = CriarBotao("Ver histórico", false, 140);
        btnVerHistorico.Margin = new Padding(0);
        btnCancelar = CriarBotao("Cancelar", false, 120);
        btnImprimir = CriarBotao("Imprimir / Exportar PDF", false, 200);
        btnGuardar = CriarBotao("Guardar cotação", true, 170);

        btnVerHistorico.Click += (s, e) =>
        {
            using var historico = new FrmHistorico(this);
            historico.ShowDialog(this);
        };
        btnCancelar.Click += btnCancelar_Click;
        btnImprimir.Click += btnImprimir_Click;
        btnGuardar.Click += btnGuardar_Click;

        var rodape = Tabela(5, 1);
        rodape.Padding = new Padding(24, 0, 24, 0);
        rodape.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
        rodape.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
        rodape.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
        rodape.ColumnStyles[3] = new ColumnStyle(SizeType.AutoSize);
        rodape.ColumnStyles[4] = new ColumnStyle(SizeType.AutoSize);
        rodape.Controls.Add(btnVerHistorico, 0, 0);
        rodape.Controls.Add(btnCancelar, 2, 0);
        rodape.Controls.Add(btnImprimir, 3, 0);
        rodape.Controls.Add(btnGuardar, 4, 0);
        rodape.Paint += (s, e) =>
        {
            using var caneta = new Pen(Tema.Borda);
            e.Graphics.DrawLine(caneta, 0, 0, rodape.Width, 0);
        };

        // ── Montagem final ──
        var raiz = Tabela(1, 3, Tema.Fundo);
        raiz.RowStyles[0] = new RowStyle(SizeType.Absolute, 84);
        raiz.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
        raiz.RowStyles[2] = new RowStyle(SizeType.Absolute, 80);
        raiz.Controls.Add(cabecalho, 0, 0);
        raiz.Controls.Add(conteudo, 0, 1);
        raiz.Controls.Add(rodape, 0, 2);

        Controls.Add(raiz);
        ResumeLayout(true);
    }

    // ───────── Auxiliares de interface ─────────

    private static TableLayoutPanel Tabela(int colunas, int linhas, Color? fundo = null)
    {
        var t = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = colunas,
            RowCount = linhas,
            BackColor = fundo ?? Color.White,
            Margin = new Padding(0)
        };
        for (int c = 0; c < colunas; c++)
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / colunas));
        for (int l = 0; l < linhas; l++)
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / linhas));
        return t;
    }

    private static Cartao CriarCartao(string titulo, Control conteudo)
    {
        var cartao = new Cartao { Dock = DockStyle.Fill };
        conteudo.Dock = DockStyle.Fill;
        var lbl = new Label
        {
            Text = titulo,
            Dock = DockStyle.Top,
            Height = 34,
            Font = Tema.Titulo,
            ForeColor = Tema.Texto,
            BackColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft
        };
        cartao.Controls.Add(conteudo);
        cartao.Controls.Add(lbl);
        return cartao;
    }

    /// <summary>Rótulo pequeno em cima + campo por baixo.</summary>
    private static Control Campo(string rotulo, Control entrada, int esquerda = 0, int direita = 0)
    {
        Control conteudo;
        if (entrada is TextBox caixa)
        {
            conteudo = new CampoTexto(caixa);
        }
        else
        {
            conteudo = new Panel { BackColor = Color.White, Padding = new Padding(0, 8, 0, 0) };
            entrada.Dock = DockStyle.Top;
            conteudo.Controls.Add(entrada);
        }
        conteudo.Dock = DockStyle.Fill;

        var painel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Margin = new Padding(esquerda, 0, direita, 10)
        };
        var lbl = new Label
        {
            Text = rotulo,
            Dock = DockStyle.Top,
            Height = 20,
            Font = Tema.Rotulo,
            ForeColor = Tema.TextoSuave,
            BackColor = Color.White
        };
        painel.Controls.Add(conteudo);
        painel.Controls.Add(lbl);
        return painel;
    }

    /// <summary>Valor de resumo (Subtotal, Taxa, ...): rótulo + valor grande.</summary>
    private static Control Valor(string rotulo, Control controle)
    {
        if (controle is TextBox tb)
        {
            tb.ReadOnly = true;
            tb.TabStop = false;
            tb.BorderStyle = BorderStyle.None;
            tb.BackColor = Color.White;
            tb.ForeColor = Tema.Texto;
            tb.Font = Tema.Valor;
        }
        controle.Dock = DockStyle.Top;

        var painel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 0, 12, 0) };
        var lbl = new Label
        {
            Text = rotulo,
            Dock = DockStyle.Top,
            Height = 22,
            Font = Tema.Rotulo,
            ForeColor = Tema.TextoSuave,
            BackColor = Color.White
        };
        painel.Controls.Add(controle);
        painel.Controls.Add(lbl);
        return painel;
    }

    private static BotaoModerno CriarBotao(string texto, bool primario, int largura = 150, int altura = 42)
    {
        return new BotaoModerno
        {
            Text = texto,
            Primario = primario,
            Size = new Size(largura, altura),
            Margin = new Padding(8, 0, 0, 0),
            Anchor = AnchorStyles.None
        };
    }

    private void ConfigurarGrelhaArtigos()
    {
        dgvArtigos = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = true,
            AllowUserToDeleteRows = true,
            AllowUserToResizeRows = false,
            AutoGenerateColumns = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight = 40,
            EnableHeadersVisualStyles = false,
            GridColor = Tema.Borda,
            RowHeadersVisible = false,
            MultiSelect = false,
            EditMode = DataGridViewEditMode.EditOnEnter
        };
        dgvArtigos.RowTemplate.Height = 38;

        var cab = dgvArtigos.ColumnHeadersDefaultCellStyle;
        cab.BackColor = Tema.FundoSuave;
        cab.ForeColor = Tema.TextoSuave;
        cab.Font = Tema.Cabecalho;
        cab.SelectionBackColor = Tema.FundoSuave;
        cab.SelectionForeColor = Tema.TextoSuave;
        cab.Padding = new Padding(10, 0, 10, 0);

        var cel = dgvArtigos.DefaultCellStyle;
        cel.BackColor = Color.White;
        cel.ForeColor = Tema.Texto;
        cel.Font = Tema.Normal;
        cel.SelectionBackColor = Tema.PrimariaSuave;
        cel.SelectionForeColor = Tema.Texto;
        cel.Padding = new Padding(10, 0, 10, 0);

        var alt = dgvArtigos.AlternatingRowsDefaultCellStyle;
        alt.BackColor = Tema.FundoSuave;
        alt.SelectionBackColor = Tema.PrimariaSuave;
        alt.SelectionForeColor = Tema.Texto;

        var aDireita = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };

        dgvArtigos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "numero_item",
            HeaderText = "Item",
            ReadOnly = true,
            FillWeight = 8,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                ForeColor = Tema.TextoSuave
            }
        });
        dgvArtigos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "designacao",
            HeaderText = "Designação do artigo",
            FillWeight = 46
        });
        dgvArtigos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "quantidade",
            HeaderText = "Qtd.",
            FillWeight = 12,
            DefaultCellStyle = aDireita
        });
        dgvArtigos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "preco_unitario",
            HeaderText = "Preço unit. (MT)",
            FillWeight = 17,
            DefaultCellStyle = aDireita
        });
        dgvArtigos.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "preco_total",
            HeaderText = "Total (MT)",
            ReadOnly = true,
            FillWeight = 17,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Font = Tema.Negrito
            }
        });

        dgvArtigos.Columns["numero_item"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        foreach (var nome in new[] { "quantidade", "preco_unitario", "preco_total" })
            dgvArtigos.Columns[nome]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

        pnlArtigos.Controls.Clear();
        pnlArtigos.Controls.Add(dgvArtigos);

        dgvArtigos.CellValueChanged += dgvArtigos_CellValueChanged;
        dgvArtigos.CurrentCellDirtyStateChanged += dgvArtigos_CurrentCellDirtyStateChanged;
        dgvArtigos.RowsAdded += (s, e) =>
        {
            AtualizarTotais();
            if (IsHandleCreated) BeginInvoke(new Action(RenumerarItens));
        };
        dgvArtigos.RowsRemoved += (s, e) =>
        {
            AtualizarTotais();
            if (IsHandleCreated) BeginInvoke(new Action(RenumerarItens));
        };
    }

    private void RenumerarItens()
    {
        int n = 1;
        foreach (DataGridViewRow linha in dgvArtigos.Rows)
        {
            if (!linha.IsNewRow)
                linha.Cells["numero_item"].Value = n++;
        }
    }
    private void dgvArtigos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (dgvArtigos.IsCurrentCellDirty)
        {
            string? columnName = dgvArtigos.CurrentCell?.OwningColumn?.Name;
            if (columnName == "quantidade" || columnName == "preco_unitario")
            {
                dgvArtigos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }

    private void dgvArtigos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        string? changedColumn = dgvArtigos.Columns[e.ColumnIndex]?.Name;
        if (changedColumn != "quantidade" && changedColumn != "preco_unitario")
            return;

        DataGridViewRow row = dgvArtigos.Rows[e.RowIndex];
        string qtdText = row.Cells["quantidade"].Value?.ToString() ?? string.Empty;
        string precoText = row.Cells["preco_unitario"].Value?.ToString() ?? string.Empty;

        decimal.TryParse(qtdText, out decimal quantidade);
        decimal.TryParse(precoText, out decimal precoUnitario);

        row.Cells["preco_total"].Value = (quantidade * precoUnitario).ToString();

        AtualizarTotais();
    }
    private void dgvArtigos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (dgvArtigos.CurrentCell == null)
            return;

        string? columnName = dgvArtigos.CurrentCell.OwningColumn?.Name;
        if (e.Control is TextBox textBox)
        {
            // Remover handler anterior para evitar subscrições duplicadas
            textBox.KeyPress -= dgvArtigos_KeyPressNumerico;
            if (columnName == "quantidade" || columnName == "preco_unitario")
            {
                textBox.KeyPress += dgvArtigos_KeyPressNumerico;
            }
        }
    }

    private void dgvArtigos_KeyPressNumerico(object sender, KeyPressEventArgs e)
    {
        // Permitir teclas de controlo (Backspace, etc.)
        if (char.IsControl(e.KeyChar))
            return;

        // Permitir dígitos
        if (char.IsDigit(e.KeyChar))
            return;

        // Permitir um único separador decimal conforme a cultura
        char separadorDecimal = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        if (e.KeyChar == separadorDecimal)
        {
            if (sender is TextBox textBox && textBox.Text.Contains(separadorDecimal))
            {
                e.Handled = true;
            }
            return;
        }

        // Bloquear tudo o resto
        e.Handled = true;
    }

    private void AtualizarTotais()
    {
        decimal subtotal = 0m;
        foreach (DataGridViewRow row in dgvArtigos.Rows)
        {
            if (row.IsNewRow)
                continue;

            string totalText = row.Cells["preco_total"].Value?.ToString() ?? "0";
            if (decimal.TryParse(totalText, out decimal lineTotal))
                subtotal += lineTotal;
        }

        string taxaText = cmbTaxa?.Text ?? "0%";
        int percent = 0;
        if (taxaText.EndsWith("%"))
            int.TryParse(taxaText.TrimEnd('%'), out percent);

        decimal taxaPercent = percent;
        decimal valorTaxa = subtotal * (taxaPercent / 100m);
        decimal totalGeral = subtotal + valorTaxa;

        if (txtSubtotal != null)
            txtSubtotal.Text = $"{subtotal:0.00} MT";
        if (txtValorTaxa != null)
            txtValorTaxa.Text = $"{valorTaxa:0.00} MT";
        if (txtTotalGeral != null)
            txtTotalGeral.Text = $"{totalGeral:0.00} MT";
    }

    private void InicializarBaseDados()
    {
        using var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=cotacoes.db");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS configuracao_empresa (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                nome_empresa TEXT,
                nuit TEXT,
                endereco TEXT,
                contactos TEXT,
                email TEXT,
                informacao_bancaria TEXT,
                logotipo_path TEXT
            );

            CREATE TABLE IF NOT EXISTS cotacao (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                numero_cotacao TEXT,
                data_emissao TEXT,
                cliente_nome TEXT,
                cliente_localizacao TEXT,
                cliente_nuit TEXT,
                subtotal REAL,
                taxa_aplicavel REAL,
                valor_taxa REAL,
                total_geral REAL,
                responsavel_nome TEXT
            );

            CREATE TABLE IF NOT EXISTS cotacao_artigo (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                cotacao_id INTEGER,
                numero_item INTEGER,
                designacao TEXT,
                quantidade REAL,
                preco_unitario REAL,
                preco_total REAL,
                FOREIGN KEY (cotacao_id) REFERENCES cotacao(id)
            );";
        command.ExecuteNonQuery();
    }


    private void documentoImpressao_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        var fontDados = new System.Drawing.Font("Arial", 8);
        var fontTabela = new System.Drawing.Font("Arial", 9);
        var fontTitulo = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
        var fontBold9 = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
        var font7 = new System.Drawing.Font("Arial", 7);

        var vermelho = System.Drawing.Color.FromArgb(192, 0, 0);
        var verde = System.Drawing.Color.FromArgb(0, 128, 0);
        var cinzento = System.Drawing.Color.LightGray;

        var branco = System.Drawing.Brushes.White;
        var preto = System.Drawing.Brushes.Black;
        var penCinzento = new System.Drawing.Pen(cinzento);
        var brushVermelho = new System.Drawing.SolidBrush(vermelho);
        var brushVerde = new System.Drawing.SolidBrush(verde);
        var brushEscuro = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40));

        var sfRight = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Far };

        // 1. Cabeçalho
        try
        {
            if (System.IO.File.Exists("logotipo.png"))
            {
                using var img = System.Drawing.Image.FromFile("logotipo.png");
                g.DrawImage(img, 50, 40, 150, 60);
            }
            else
            {
                g.DrawString("LOGOTIPO", fontTitulo, preto, 50, 50);
            }
        }
        catch
        {
            g.DrawString("LOGOTIPO", fontTitulo, preto, 50, 50);
        }

        g.DrawString("COTAÇÃO N: " + txtNumeroCotacao.Text, fontTitulo, preto, 800, 40, sfRight);

        float yEmp = 40;
        g.DrawString(txtEmpresaNome.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;
        g.DrawString("NUIT: " + txtEmpresaNuit.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;
        g.DrawString(txtEmpresaEndereco.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;
        g.DrawString(txtEmpresaContactos.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;
        g.DrawString(txtEmpresaEmail.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;
        g.DrawString(txtEmpresaBanco.Text, font7, preto, 800, yEmp, sfRight); yEmp += 10;

        g.DrawLine(penCinzento, 50, 130, 800, 130);

        // 2. Dados do cliente e data
        g.DrawString("DADOS DO CLIENTE", fontBold9, preto, 50, 150);
        g.DrawString(txtClienteNome.Text, fontDados, preto, 50, 170);
        g.DrawString(txtClienteLocalizacao.Text, fontDados, preto, 50, 185);
        g.DrawString("NUIT: " + txtClienteNuit.Text, fontDados, preto, 50, 200);

        g.DrawString("DATA: " + dtpDataEmissao.Value.ToShortDateString(), fontBold9, preto, 800, 150, sfRight);

        // 3. Tabela de artigos
        float[] xs = { 50f, 100f, 510f, 610f, 710f };
        float headerY = 260;
        g.DrawLine(penCinzento, 50, headerY, 800, headerY);

        g.DrawString("Item", fontBold9, brushVermelho, xs[0], headerY + 2);
        g.DrawString("DESIGNAÇÃO DO ARTIGO", fontBold9, brushVermelho, xs[1], headerY + 2);
        g.DrawString("QUANTIDADE", fontBold9, brushVermelho, xs[2], headerY + 2);
        g.DrawString("PREÇO UNITÁRIO", fontBold9, brushVermelho, xs[3], headerY + 2);
        g.DrawString("PREÇO TOTAL", fontBold9, brushVermelho, xs[4], headerY + 2);

        g.DrawLine(penCinzento, 50, headerY + 20, 800, headerY + 20);

        float rowY = headerY + 22;
        int itemNum = 1;
        foreach (DataGridViewRow row in dgvArtigos.Rows)
        {
            if (row.IsNewRow) continue;

            string designacao = row.Cells["designacao"].Value?.ToString() ?? string.Empty;
            string qtd = row.Cells["quantidade"].Value?.ToString() ?? "0";
            string preco = row.Cells["preco_unitario"].Value?.ToString() ?? "0";
            string total = row.Cells["preco_total"].Value?.ToString() ?? "0";

            g.DrawString(itemNum.ToString(), fontTabela, preto, xs[0], rowY);

            var rectDesign = new System.Drawing.RectangleF(xs[1], rowY, 400, 36);
            g.DrawString(designacao, fontTabela, preto, rectDesign);

            g.DrawString(qtd, fontTabela, preto, xs[2], rowY);
            g.DrawString(preco, fontTabela, preto, xs[3], rowY);

            var rectTotal = new System.Drawing.RectangleF(xs[4], rowY, 90, 18);
            g.FillRectangle(brushVermelho, rectTotal);
            g.DrawString(total, fontTabela, branco, xs[4] + 2, rowY);

            g.DrawLine(penCinzento, 50, rowY + 18, 800, rowY + 18);
            foreach (float x in xs)
            {
                g.DrawLine(penCinzento, x, rowY, x, rowY + 18);
            }
            g.DrawLine(penCinzento, 800, rowY, 800, rowY + 18);

            rowY += 20;
            itemNum++;
            if (rowY > 560) break;
        }

        // 4. Resumo financeiro
        float barY = 600;
        float barH = 25;
        g.FillRectangle(brushVermelho, 50, barY, 750, barH);
        g.DrawString("Sub-Total:", fontTitulo, branco, 55, barY + 4);
        g.DrawString(txtSubtotal.Text, fontTitulo, branco, 800, barY + 4, sfRight);
        barY += barH;

        g.FillRectangle(brushEscuro, 50, barY, 750, barH);
        g.DrawString("IVA (" + cmbTaxa.Text + "):", fontTitulo, branco, 55, barY + 4);
        g.DrawString(txtValorTaxa.Text, fontTitulo, branco, 800, barY + 4, sfRight);
        barY += barH;

        g.FillRectangle(brushVerde, 50, barY, 750, barH);
        g.DrawString("Total Geral:", fontTitulo, branco, 55, barY + 4);
        g.DrawString(txtTotalGeral.Text, fontTitulo, branco, 800, barY + 4, sfRight);

        // 5. Rodapé
        float footY = 690;
        g.DrawString(txtEmpresaEmail.Text, fontDados, preto, 50, footY);
        g.DrawString(txtEmpresaContactos.Text, fontDados, preto, 50, footY + 15);
        g.DrawString("O Director Geral", fontDados, preto, 800, footY, sfRight);
        g.DrawString("____________________________", fontDados, preto, 800, footY + 30, sfRight);
        e.HasMorePages = false;
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void GerarProximoNumeroCotacao()
    {
        try
        {
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=cotacoes.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT numero_cotacao FROM cotacao ORDER BY id DESC LIMIT 1;";
            object? resultado = command.ExecuteScalar();

            string numeroGerado;
            if (resultado == null || string.IsNullOrWhiteSpace(resultado.ToString()))
            {
                numeroGerado = "COT-" + DateTime.Now.Year + "-0001";
            }
            else
            {
                string ultimoNumero = resultado.ToString()!;
                string[] partes = ultimoNumero.Split('-');
                int numeroFinal = 0;
                if (partes.Length > 0)
                {
                    int.TryParse(partes[partes.Length - 1], out numeroFinal);
                }
                int novoNumero = numeroFinal + 1;
                numeroGerado = "COT-" + DateTime.Now.Year + "-" + novoNumero.ToString("D4");
            }

            txtNumeroCotacao.Text = numeroGerado;
        }
        catch
        {
            // Em caso de erro (ex.: tabela inexistente), sugerir número inicial
            txtNumeroCotacao.Text = "COT-" + DateTime.Now.Year + "-0001";
        }
    }


    private async void btnImprimir_Click(object sender, EventArgs e)
    {
        try
        {
            decimal.TryParse(txtSubtotal.Text.Replace("MT", "").Trim(), out decimal subtotal);
            decimal.TryParse(txtValorTaxa.Text.Replace("MT", "").Trim(), out decimal valorTaxa);
            decimal.TryParse(txtTotalGeral.Text.Replace("MT", "").Trim(), out decimal totalGeral);

            string taxaText = cmbTaxa?.Text ?? "0%";
            int percent = 0;
            if (taxaText.EndsWith("%"))
                int.TryParse(taxaText.TrimEnd('%').Trim(), out percent);

            var cotacaoAtual = new Cotacao.Models.Cotacao
            {
                NumeroCotacao = txtNumeroCotacao.Text,
                DataEmissao = dtpDataEmissao.Value,
                ClienteNome = txtClienteNome.Text,
                ClienteLocalizacao = txtClienteLocalizacao.Text,
                ClienteNuit = txtClienteNuit.Text,
                Subtotal = subtotal,
                TaxaAplicavel = percent,
                ValorTaxa = valorTaxa,
                TotalGeral = totalGeral,
                ResponsavelNome = txtResponsavel.Text,
                EmpresaNome = txtEmpresaNome.Text,
                EmpresaNuit = txtEmpresaNuit.Text,
                EmpresaEndereco = txtEmpresaEndereco.Text,
                EmpresaContactos = txtEmpresaContactos.Text,
                EmpresaEmail = txtEmpresaEmail.Text,
                EmpresaBanco = txtEmpresaBanco.Text
            };

            var listaArtigos = new List<Cotacao.Models.CotacaoArtigo>();
            int numeroItem = 1;
            foreach (DataGridViewRow row in dgvArtigos.Rows)
            {
                if (row.IsNewRow) continue;

                string designacao = row.Cells["designacao"].Value?.ToString() ?? string.Empty;
                decimal.TryParse(row.Cells["quantidade"].Value?.ToString() ?? "0", out decimal quantidade);
                decimal.TryParse(row.Cells["preco_unitario"].Value?.ToString() ?? "0", out decimal precoUnitario);
                decimal.TryParse(row.Cells["preco_total"].Value?.ToString() ?? "0", out decimal precoTotal);

                listaArtigos.Add(new Cotacao.Models.CotacaoArtigo
                {
                    NumeroItem = numeroItem++,
                    Designacao = designacao,
                    Quantidade = quantidade,
                    PrecoUnitario = precoUnitario,
                    PrecoTotal = precoTotal
                });
            }

            using var saveDialog = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = "Cotacao_" + txtNumeroCotacao.Text + ".pdf"
            };
            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            await PdfExporter.ExportarParaPdfAsync(cotacaoAtual, listaArtigos, saveDialog.FileName);

            MessageBox.Show("PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao gerar PDF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void btnGuardar_Click(object sender, EventArgs e)
    {
        // Validações exaustivas de campos obrigatórios antes de gravar
        // --- Empresa ---
        if (string.IsNullOrWhiteSpace(txtEmpresaNome.Text))
        {
            MessageBox.Show("O campo 'Nome da Empresa' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaNome.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEmpresaNuit.Text))
        {
            MessageBox.Show("O campo 'NUIT' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaNuit.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEmpresaEndereco.Text))
        {
            MessageBox.Show("O campo 'Endereço' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaEndereco.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEmpresaContactos.Text))
        {
            MessageBox.Show("O campo 'Contactos' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaContactos.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEmpresaEmail.Text))
        {
            MessageBox.Show("O campo 'Email' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaEmail.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtEmpresaBanco.Text))
        {
            MessageBox.Show("O campo 'Dados Bancários' (secção Empresa) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtEmpresaBanco.Focus();
            return;
        }

        // --- Cliente ---
        if (string.IsNullOrWhiteSpace(txtClienteNome.Text))
        {
            MessageBox.Show("O campo 'Nome do Cliente' (secção Cliente) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtClienteNome.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtClienteNuit.Text))
        {
            MessageBox.Show("O campo 'NUIT' (secção Cliente) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtClienteNuit.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtClienteLocalizacao.Text))
        {
            MessageBox.Show("O campo 'Localização' (secção Cliente) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtClienteLocalizacao.Focus();
            return;
        }

        // --- Documento ---
        if (string.IsNullOrWhiteSpace(txtNumeroCotacao.Text))
        {
            MessageBox.Show("O campo 'Número de Cotação' (secção Documento) é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtNumeroCotacao.Focus();
            return;
        }

        // --- Artigos ---
        bool temArtigo = false;
        foreach (DataGridViewRow row in dgvArtigos.Rows)
        {
            if (row.IsNewRow) continue;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    temArtigo = true;
                    break;
                }
            }
            if (temArtigo) break;
        }

        if (!temArtigo)
        {
            MessageBox.Show("A grelha de artigos (secção Artigos) tem de conter pelo menos uma linha preenchida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            decimal.TryParse(txtSubtotal.Text.Replace("MT", "").Trim(), out decimal subtotal);
            decimal.TryParse(txtValorTaxa.Text.Replace("MT", "").Trim(), out decimal valorTaxa);
            decimal.TryParse(txtTotalGeral.Text.Replace("MT", "").Trim(), out decimal totalGeral);

            string taxaText = cmbTaxa?.Text ?? "0%";
            int percent = 0;
            if (taxaText.EndsWith("%"))
                int.TryParse(taxaText.TrimEnd('%').Trim(), out percent);
            decimal taxaAplicavel = percent;

            using var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=cotacoes.db");
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                using var cmdHeader = connection.CreateCommand();
                cmdHeader.Transaction = transaction;
                cmdHeader.CommandText = @"INSERT INTO cotacao
                    (numero_cotacao, data_emissao, cliente_nome, cliente_localizacao, cliente_nuit,
                     subtotal, taxa_aplicavel, valor_taxa, total_geral, responsavel_nome)
                    VALUES
                    (@numero_cotacao, @data_emissao, @cliente_nome, @cliente_localizacao, @cliente_nuit,
                     @subtotal, @taxa_aplicavel, @valor_taxa, @total_geral, @responsavel_nome);";

                cmdHeader.Parameters.AddWithValue("@numero_cotacao", txtNumeroCotacao.Text);
                cmdHeader.Parameters.AddWithValue("@data_emissao", dtpDataEmissao.Value.ToString("yyyy-MM-dd"));
                cmdHeader.Parameters.AddWithValue("@cliente_nome", txtClienteNome.Text);
                cmdHeader.Parameters.AddWithValue("@cliente_localizacao", txtClienteLocalizacao.Text);
                cmdHeader.Parameters.AddWithValue("@cliente_nuit", txtClienteNuit.Text);
                cmdHeader.Parameters.AddWithValue("@subtotal", subtotal);
                cmdHeader.Parameters.AddWithValue("@taxa_aplicavel", taxaAplicavel);
                cmdHeader.Parameters.AddWithValue("@valor_taxa", valorTaxa);
                cmdHeader.Parameters.AddWithValue("@total_geral", totalGeral);
                cmdHeader.Parameters.AddWithValue("@responsavel_nome", txtResponsavel.Text);

                cmdHeader.ExecuteNonQuery();

                using var cmdId = connection.CreateCommand();
                cmdId.Transaction = transaction;
                cmdId.CommandText = "SELECT last_insert_rowid();";
                long cotacaoId = (long)(cmdId.ExecuteScalar() ?? 0L);

                int numeroItem = 1;
                foreach (DataGridViewRow row in dgvArtigos.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string designacao = row.Cells["designacao"].Value?.ToString() ?? string.Empty;
                    decimal.TryParse(row.Cells["quantidade"].Value?.ToString() ?? "0", out decimal quantidade);
                    decimal.TryParse(row.Cells["preco_unitario"].Value?.ToString() ?? "0", out decimal precoUnitario);
                    decimal.TryParse(row.Cells["preco_total"].Value?.ToString() ?? "0", out decimal precoTotal);

                    using var cmdItem = connection.CreateCommand();
                    cmdItem.Transaction = transaction;
                    cmdItem.CommandText = @"INSERT INTO cotacao_artigo
                        (cotacao_id, numero_item, designacao, quantidade, preco_unitario, preco_total)
                        VALUES
                        (@cotacao_id, @numero_item, @designacao, @quantidade, @preco_unitario, @preco_total);";

                    cmdItem.Parameters.AddWithValue("@cotacao_id", cotacaoId);
                    cmdItem.Parameters.AddWithValue("@numero_item", numeroItem);
                    cmdItem.Parameters.AddWithValue("@designacao", designacao);
                    cmdItem.Parameters.AddWithValue("@quantidade", quantidade);
                    cmdItem.Parameters.AddWithValue("@preco_unitario", precoUnitario);
                    cmdItem.Parameters.AddWithValue("@preco_total", precoTotal);

                    cmdItem.ExecuteNonQuery();
                    numeroItem++;
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            txtNumeroCotacao.Clear();
            txtClienteNome.Clear();
            txtClienteLocalizacao.Clear();
            txtClienteNuit.Clear();
            txtResponsavel.Clear();
            dgvArtigos.Rows.Clear();
            cmbTaxa.SelectedIndex = 0;
            AtualizarTotais();

            MessageBox.Show("Cotação guardada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao guardar a cotação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void CarregarCotacaoComoTemplate(int cotacaoId)
    {
        using var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=cotacoes.db");
        connection.Open();

        using var cmdHeader = connection.CreateCommand();
        cmdHeader.CommandText = "SELECT cliente_nome, cliente_localizacao, cliente_nuit FROM cotacao WHERE id = @id";
        cmdHeader.Parameters.AddWithValue("@id", cotacaoId);
        using var readerHeader = cmdHeader.ExecuteReader();
        if (readerHeader.Read())
        {
            txtClienteNome.Text = readerHeader["cliente_nome"]?.ToString() ?? string.Empty;
            txtClienteLocalizacao.Text = readerHeader["cliente_localizacao"]?.ToString() ?? string.Empty;
            txtClienteNuit.Text = readerHeader["cliente_nuit"]?.ToString() ?? string.Empty;
        }

        dgvArtigos.Rows.Clear();

        using var cmdArtigos = connection.CreateCommand();
        cmdArtigos.CommandText = "SELECT designacao, quantidade, preco_unitario FROM cotacao_artigo WHERE cotacao_id = @id";
        cmdArtigos.Parameters.AddWithValue("@id", cotacaoId);
        using var readerArtigos = cmdArtigos.ExecuteReader();
        while (readerArtigos.Read())
        {
            string designacao = readerArtigos["designacao"]?.ToString() ?? string.Empty;
            decimal quantidade = Convert.ToDecimal(readerArtigos["quantidade"]);
            decimal precoUnitario = Convert.ToDecimal(readerArtigos["preco_unitario"]);
            decimal precoTotal = quantidade * precoUnitario;
               dgvArtigos.Rows.Add(null, designacao, quantidade, precoUnitario, precoTotal);
        }

        AtualizarTotais();
    }


}
