namespace Cotacao;

partial class FrmCotacao
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tblMain = new TableLayoutPanel();
        tblTop = new TableLayoutPanel();
        grpEmpresa = new GroupBox();
        tblEmpresa = new TableLayoutPanel();
        picLogotipo = new PictureBox();
        lblNomeEmpresa = new Label();
        txtNomeEmpresa = new TextBox();
        lblEndereco = new Label();
        txtEndereco = new TextBox();
        lblInformacoesBancarias = new Label();
        txtInformacoesBancarias = new TextBox();
        lblNuitEmpresa = new Label();
        txtNuitEmpresa = new TextBox();
        lblContactos = new Label();
        txtContactos = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        grpDocumento = new GroupBox();
        tblDocumento = new TableLayoutPanel();
        lblNumeroCotacao = new Label();
        txtNumeroCotacao = new TextBox();
        lblDataEmissao = new Label();
        dtpDataEmissao = new DateTimePicker();
        grpCliente = new GroupBox();
        tblCliente = new TableLayoutPanel();
        lblClienteNome = new Label();
        txtClienteNome = new TextBox();
        lblClienteLocalizacao = new Label();
        txtClienteLocalizacao = new TextBox();
        lblClienteNuit = new Label();
        txtClienteNuit = new TextBox();
        pnlPlaceholder = new Panel();
        lblPlaceholder = new Label();
        grpRodape = new GroupBox();
        tblRodape = new TableLayoutPanel();
        lblResponsavel = new Label();
        txtResponsavelNome = new TextBox();
        lblAssinatura = new Label();
        tblMain.SuspendLayout();
        tblTop.SuspendLayout();
        grpEmpresa.SuspendLayout();
        tblEmpresa.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picLogotipo).BeginInit();
        grpDocumento.SuspendLayout();
        tblDocumento.SuspendLayout();
        grpCliente.SuspendLayout();
        tblCliente.SuspendLayout();
        pnlPlaceholder.SuspendLayout();
        grpRodape.SuspendLayout();
        tblRodape.SuspendLayout();
        SuspendLayout();
        // 
        // tblMain
        // 
        tblMain.ColumnCount = 1;
        tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblMain.Controls.Add(tblTop, 0, 0);
        tblMain.Controls.Add(grpCliente, 0, 1);
        tblMain.Controls.Add(pnlPlaceholder, 0, 2);
        tblMain.Controls.Add(grpRodape, 0, 3);
        tblMain.Dock = DockStyle.Fill;
        tblMain.Location = new Point(5, 5);
        tblMain.Margin = new Padding(3, 4, 3, 4);
        tblMain.Name = "tblMain";
        tblMain.Padding = new Padding(9, 11, 9, 11);
        tblMain.RowCount = 4;
        tblMain.RowStyles.Add(new RowStyle());
        tblMain.RowStyles.Add(new RowStyle());
        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tblMain.RowStyles.Add(new RowStyle());
        tblMain.Size = new Size(1247, 990);
        tblMain.TabIndex = 0;
        // 
        // tblTop
        // 
        tblTop.ColumnCount = 2;
        tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tblTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tblTop.Controls.Add(grpEmpresa, 0, 0);
        tblTop.Controls.Add(grpDocumento, 1, 0);
        tblTop.Dock = DockStyle.Fill;
        tblTop.Location = new Point(12, 15);
        tblTop.Margin = new Padding(3, 4, 3, 4);
        tblTop.Name = "tblTop";
        tblTop.RowCount = 1;
        tblTop.RowStyles.Add(new RowStyle());
        tblTop.Size = new Size(1223, 1);
        tblTop.TabIndex = 0;
        // 
        // grpEmpresa
        // 
        grpEmpresa.Controls.Add(tblEmpresa);
        grpEmpresa.Dock = DockStyle.Fill;
        grpEmpresa.Location = new Point(3, 4);
        grpEmpresa.Margin = new Padding(3, 4, 3, 4);
        grpEmpresa.Name = "grpEmpresa";
        grpEmpresa.Padding = new Padding(9, 11, 9, 11);
        grpEmpresa.Size = new Size(605, 1);
        grpEmpresa.TabIndex = 0;
        grpEmpresa.TabStop = false;
        grpEmpresa.Text = "Empresa";
        // 
        // tblEmpresa
        // 
        tblEmpresa.ColumnCount = 3;
        tblEmpresa.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
        tblEmpresa.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblEmpresa.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
        tblEmpresa.Controls.Add(picLogotipo, 2, 0);
        tblEmpresa.Controls.Add(lblNomeEmpresa, 0, 0);
        tblEmpresa.Controls.Add(txtNomeEmpresa, 1, 0);
        tblEmpresa.Controls.Add(lblEndereco, 0, 1);
        tblEmpresa.Controls.Add(txtEndereco, 1, 1);
        tblEmpresa.Controls.Add(lblInformacoesBancarias, 0, 2);
        tblEmpresa.Controls.Add(txtInformacoesBancarias, 1, 2);
        tblEmpresa.Controls.Add(lblNuitEmpresa, 0, 3);
        tblEmpresa.Controls.Add(txtNuitEmpresa, 1, 3);
        tblEmpresa.Controls.Add(lblContactos, 0, 4);
        tblEmpresa.Controls.Add(txtContactos, 1, 4);
        tblEmpresa.Controls.Add(lblEmail, 0, 5);
        tblEmpresa.Controls.Add(txtEmail, 1, 5);
        tblEmpresa.Dock = DockStyle.Fill;
        tblEmpresa.Location = new Point(9, 31);
        tblEmpresa.Margin = new Padding(3, 4, 3, 4);
        tblEmpresa.Name = "tblEmpresa";
        tblEmpresa.RowCount = 6;
        tblEmpresa.RowStyles.Add(new RowStyle());
        tblEmpresa.RowStyles.Add(new RowStyle());
        tblEmpresa.RowStyles.Add(new RowStyle());
        tblEmpresa.RowStyles.Add(new RowStyle());
        tblEmpresa.RowStyles.Add(new RowStyle());
        tblEmpresa.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tblEmpresa.Size = new Size(587, 0);
        tblEmpresa.TabIndex = 0;
        // 
        // picLogotipo
        // 
        picLogotipo.BorderStyle = BorderStyle.FixedSingle;
        picLogotipo.Dock = DockStyle.Fill;
        picLogotipo.Location = new Point(464, 4);
        picLogotipo.Margin = new Padding(3, 4, 3, 4);
        picLogotipo.Name = "picLogotipo";
        tblEmpresa.SetRowSpan(picLogotipo, 6);
        picLogotipo.Size = new Size(120, 351);
        picLogotipo.SizeMode = PictureBoxSizeMode.Zoom;
        picLogotipo.TabIndex = 0;
        picLogotipo.TabStop = false;
        // 
        // lblNomeEmpresa
        // 
        lblNomeEmpresa.AutoSize = true;
        lblNomeEmpresa.Dock = DockStyle.Fill;
        lblNomeEmpresa.Location = new Point(3, 0);
        lblNomeEmpresa.Name = "lblNomeEmpresa";
        lblNomeEmpresa.Size = new Size(131, 81);
        lblNomeEmpresa.TabIndex = 1;
        lblNomeEmpresa.Text = "Nome da Empresa:";
        lblNomeEmpresa.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtNomeEmpresa
        // 
        txtNomeEmpresa.Dock = DockStyle.Fill;
        txtNomeEmpresa.Location = new Point(140, 4);
        txtNomeEmpresa.Margin = new Padding(3, 4, 3, 4);
        txtNomeEmpresa.Multiline = true;
        txtNomeEmpresa.Name = "txtNomeEmpresa";
        txtNomeEmpresa.ScrollBars = ScrollBars.Vertical;
        txtNomeEmpresa.Size = new Size(318, 73);
        txtNomeEmpresa.TabIndex = 2;
        // 
        // lblEndereco
        // 
        lblEndereco.AutoSize = true;
        lblEndereco.Dock = DockStyle.Fill;
        lblEndereco.Location = new Point(3, 81);
        lblEndereco.Name = "lblEndereco";
        lblEndereco.Size = new Size(131, 35);
        lblEndereco.TabIndex = 3;
        lblEndereco.Text = "Endereço:";
        lblEndereco.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtEndereco
        // 
        txtEndereco.Dock = DockStyle.Fill;
        txtEndereco.Location = new Point(140, 85);
        txtEndereco.Margin = new Padding(3, 4, 3, 4);
        txtEndereco.Name = "txtEndereco";
        txtEndereco.Size = new Size(318, 27);
        txtEndereco.TabIndex = 4;
        // 
        // lblInformacoesBancarias
        // 
        lblInformacoesBancarias.AutoSize = true;
        lblInformacoesBancarias.Dock = DockStyle.Fill;
        lblInformacoesBancarias.Location = new Point(3, 116);
        lblInformacoesBancarias.Name = "lblInformacoesBancarias";
        lblInformacoesBancarias.Size = new Size(131, 100);
        lblInformacoesBancarias.TabIndex = 5;
        lblInformacoesBancarias.Text = "Informações Bancárias:";
        lblInformacoesBancarias.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtInformacoesBancarias
        // 
        txtInformacoesBancarias.Dock = DockStyle.Fill;
        txtInformacoesBancarias.Location = new Point(140, 120);
        txtInformacoesBancarias.Margin = new Padding(3, 4, 3, 4);
        txtInformacoesBancarias.Multiline = true;
        txtInformacoesBancarias.Name = "txtInformacoesBancarias";
        txtInformacoesBancarias.ScrollBars = ScrollBars.Vertical;
        txtInformacoesBancarias.Size = new Size(318, 92);
        txtInformacoesBancarias.TabIndex = 6;
        // 
        // lblNuitEmpresa
        // 
        lblNuitEmpresa.AutoSize = true;
        lblNuitEmpresa.Dock = DockStyle.Fill;
        lblNuitEmpresa.Location = new Point(3, 216);
        lblNuitEmpresa.Name = "lblNuitEmpresa";
        lblNuitEmpresa.Size = new Size(131, 35);
        lblNuitEmpresa.TabIndex = 7;
        lblNuitEmpresa.Text = "NUIT:";
        lblNuitEmpresa.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtNuitEmpresa
        // 
        txtNuitEmpresa.Dock = DockStyle.Fill;
        txtNuitEmpresa.Location = new Point(140, 220);
        txtNuitEmpresa.Margin = new Padding(3, 4, 3, 4);
        txtNuitEmpresa.Name = "txtNuitEmpresa";
        txtNuitEmpresa.Size = new Size(318, 27);
        txtNuitEmpresa.TabIndex = 8;
        // 
        // lblContactos
        // 
        lblContactos.AutoSize = true;
        lblContactos.Dock = DockStyle.Fill;
        lblContactos.Location = new Point(3, 251);
        lblContactos.Name = "lblContactos";
        lblContactos.Size = new Size(131, 88);
        lblContactos.TabIndex = 9;
        lblContactos.Text = "Contactos:";
        lblContactos.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtContactos
        // 
        txtContactos.Dock = DockStyle.Fill;
        txtContactos.Location = new Point(140, 255);
        txtContactos.Margin = new Padding(3, 4, 3, 4);
        txtContactos.Name = "txtContactos";
        txtContactos.Size = new Size(318, 27);
        txtContactos.TabIndex = 10;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Dock = DockStyle.Fill;
        lblEmail.Location = new Point(3, 339);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(131, 20);
        lblEmail.TabIndex = 11;
        lblEmail.Text = "Email:";
        lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtEmail
        // 
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Location = new Point(140, 343);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(318, 27);
        txtEmail.TabIndex = 12;
        // 
        // grpDocumento
        // 
        grpDocumento.Controls.Add(tblDocumento);
        grpDocumento.Dock = DockStyle.Fill;
        grpDocumento.Location = new Point(614, 4);
        grpDocumento.Margin = new Padding(3, 4, 3, 4);
        grpDocumento.Name = "grpDocumento";
        grpDocumento.Padding = new Padding(9, 11, 9, 11);
        grpDocumento.Size = new Size(606, 1);
        grpDocumento.TabIndex = 1;
        grpDocumento.TabStop = false;
        grpDocumento.Text = "Documento";
        // 
        // tblDocumento
        // 
        tblDocumento.ColumnCount = 2;
        tblDocumento.ColumnStyles.Add(new ColumnStyle());
        tblDocumento.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblDocumento.Controls.Add(lblNumeroCotacao, 0, 0);
        tblDocumento.Controls.Add(txtNumeroCotacao, 1, 0);
        tblDocumento.Controls.Add(lblDataEmissao, 0, 1);
        tblDocumento.Controls.Add(dtpDataEmissao, 1, 1);
        tblDocumento.Dock = DockStyle.Fill;
        tblDocumento.Location = new Point(9, 31);
        tblDocumento.Margin = new Padding(3, 4, 3, 4);
        tblDocumento.Name = "tblDocumento";
        tblDocumento.RowCount = 2;
        tblDocumento.RowStyles.Add(new RowStyle());
        tblDocumento.RowStyles.Add(new RowStyle());
        tblDocumento.Size = new Size(588, 0);
        tblDocumento.TabIndex = 0;
        // 
        // lblNumeroCotacao
        // 
        lblNumeroCotacao.AutoSize = true;
        lblNumeroCotacao.Dock = DockStyle.Fill;
        lblNumeroCotacao.Location = new Point(3, 0);
        lblNumeroCotacao.Name = "lblNumeroCotacao";
        lblNumeroCotacao.Size = new Size(146, 35);
        lblNumeroCotacao.TabIndex = 0;
        lblNumeroCotacao.Text = "Número da Cotação:";
        lblNumeroCotacao.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtNumeroCotacao
        // 
        txtNumeroCotacao.Dock = DockStyle.Fill;
        txtNumeroCotacao.Location = new Point(155, 4);
        txtNumeroCotacao.Margin = new Padding(3, 4, 3, 4);
        txtNumeroCotacao.Name = "txtNumeroCotacao";
        txtNumeroCotacao.Size = new Size(430, 27);
        txtNumeroCotacao.TabIndex = 1;
        // 
        // lblDataEmissao
        // 
        lblDataEmissao.AutoSize = true;
        lblDataEmissao.Dock = DockStyle.Fill;
        lblDataEmissao.Location = new Point(3, 35);
        lblDataEmissao.Name = "lblDataEmissao";
        lblDataEmissao.Size = new Size(146, 35);
        lblDataEmissao.TabIndex = 2;
        lblDataEmissao.Text = "Data de Emissão:";
        lblDataEmissao.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dtpDataEmissao
        // 
        dtpDataEmissao.Dock = DockStyle.Fill;
        dtpDataEmissao.Format = DateTimePickerFormat.Short;
        dtpDataEmissao.Location = new Point(155, 39);
        dtpDataEmissao.Margin = new Padding(3, 4, 3, 4);
        dtpDataEmissao.Name = "dtpDataEmissao";
        dtpDataEmissao.Size = new Size(430, 27);
        dtpDataEmissao.TabIndex = 3;
        // 
        // grpCliente
        // 
        grpCliente.Controls.Add(tblCliente);
        grpCliente.Dock = DockStyle.Fill;
        grpCliente.Location = new Point(12, 24);
        grpCliente.Margin = new Padding(3, 4, 3, 4);
        grpCliente.Name = "grpCliente";
        grpCliente.Padding = new Padding(9, 11, 9, 11);
        grpCliente.Size = new Size(1223, 1);
        grpCliente.TabIndex = 1;
        grpCliente.TabStop = false;
        grpCliente.Text = "Cliente";
        // 
        // tblCliente
        // 
        tblCliente.ColumnCount = 2;
        tblCliente.ColumnStyles.Add(new ColumnStyle());
        tblCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblCliente.Controls.Add(lblClienteNome, 0, 0);
        tblCliente.Controls.Add(txtClienteNome, 1, 0);
        tblCliente.Controls.Add(lblClienteLocalizacao, 0, 1);
        tblCliente.Controls.Add(txtClienteLocalizacao, 1, 1);
        tblCliente.Controls.Add(lblClienteNuit, 0, 2);
        tblCliente.Controls.Add(txtClienteNuit, 1, 2);
        tblCliente.Dock = DockStyle.Fill;
        tblCliente.Location = new Point(9, 31);
        tblCliente.Margin = new Padding(3, 4, 3, 4);
        tblCliente.Name = "tblCliente";
        tblCliente.RowCount = 3;
        tblCliente.RowStyles.Add(new RowStyle());
        tblCliente.RowStyles.Add(new RowStyle());
        tblCliente.RowStyles.Add(new RowStyle());
        tblCliente.Size = new Size(1205, 0);
        tblCliente.TabIndex = 0;
        // 
        // lblClienteNome
        // 
        lblClienteNome.AutoSize = true;
        lblClienteNome.Dock = DockStyle.Fill;
        lblClienteNome.Location = new Point(3, 0);
        lblClienteNome.Name = "lblClienteNome";
        lblClienteNome.Size = new Size(90, 35);
        lblClienteNome.TabIndex = 0;
        lblClienteNome.Text = "Nome:";
        lblClienteNome.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtClienteNome
        // 
        txtClienteNome.Dock = DockStyle.Fill;
        txtClienteNome.Location = new Point(99, 4);
        txtClienteNome.Margin = new Padding(3, 4, 3, 4);
        txtClienteNome.Name = "txtClienteNome";
        txtClienteNome.Size = new Size(1103, 27);
        txtClienteNome.TabIndex = 1;
        // 
        // lblClienteLocalizacao
        // 
        lblClienteLocalizacao.AutoSize = true;
        lblClienteLocalizacao.Dock = DockStyle.Fill;
        lblClienteLocalizacao.Location = new Point(3, 35);
        lblClienteLocalizacao.Name = "lblClienteLocalizacao";
        lblClienteLocalizacao.Size = new Size(90, 35);
        lblClienteLocalizacao.TabIndex = 2;
        lblClienteLocalizacao.Text = "Localização:";
        lblClienteLocalizacao.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtClienteLocalizacao
        // 
        txtClienteLocalizacao.Dock = DockStyle.Fill;
        txtClienteLocalizacao.Location = new Point(99, 39);
        txtClienteLocalizacao.Margin = new Padding(3, 4, 3, 4);
        txtClienteLocalizacao.Name = "txtClienteLocalizacao";
        txtClienteLocalizacao.Size = new Size(1103, 27);
        txtClienteLocalizacao.TabIndex = 2;
        // 
        // lblClienteNuit
        // 
        lblClienteNuit.AutoSize = true;
        lblClienteNuit.Dock = DockStyle.Fill;
        lblClienteNuit.Location = new Point(3, 70);
        lblClienteNuit.Name = "lblClienteNuit";
        lblClienteNuit.Size = new Size(90, 35);
        lblClienteNuit.TabIndex = 4;
        lblClienteNuit.Text = "NUIT:";
        lblClienteNuit.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtClienteNuit
        // 
        txtClienteNuit.Dock = DockStyle.Fill;
        txtClienteNuit.Location = new Point(99, 74);
        txtClienteNuit.Margin = new Padding(3, 4, 3, 4);
        txtClienteNuit.Name = "txtClienteNuit";
        txtClienteNuit.Size = new Size(1103, 27);
        txtClienteNuit.TabIndex = 3;
        // 
        // pnlPlaceholder
        // 
        pnlPlaceholder.BackColor = SystemColors.ControlLight;
        pnlPlaceholder.BorderStyle = BorderStyle.FixedSingle;
        pnlPlaceholder.Controls.Add(lblPlaceholder);
        pnlPlaceholder.Dock = DockStyle.Fill;
        pnlPlaceholder.Location = new Point(12, 33);
        pnlPlaceholder.Margin = new Padding(3, 4, 3, 4);
        pnlPlaceholder.Name = "pnlPlaceholder";
        pnlPlaceholder.Size = new Size(1223, 933);
        pnlPlaceholder.TabIndex = 2;
        // 
        // lblPlaceholder
        // 
        lblPlaceholder.Dock = DockStyle.Fill;
        lblPlaceholder.Location = new Point(0, 0);
        lblPlaceholder.Name = "lblPlaceholder";
        lblPlaceholder.Size = new Size(1221, 931);
        lblPlaceholder.TabIndex = 0;
        lblPlaceholder.Text = "Espaço reservado para Tabela de Artigos e Resumo Financeiro (próxima tarefa)";
        lblPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // grpRodape
        // 
        grpRodape.Controls.Add(tblRodape);
        grpRodape.Dock = DockStyle.Fill;
        grpRodape.Location = new Point(12, 974);
        grpRodape.Margin = new Padding(3, 4, 3, 4);
        grpRodape.Name = "grpRodape";
        grpRodape.Padding = new Padding(9, 11, 9, 11);
        grpRodape.Size = new Size(1223, 1);
        grpRodape.TabIndex = 3;
        grpRodape.TabStop = false;
        grpRodape.Text = "Responsável";
        // 
        // tblRodape
        // 
        tblRodape.ColumnCount = 2;
        tblRodape.ColumnStyles.Add(new ColumnStyle());
        tblRodape.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblRodape.Controls.Add(lblResponsavel, 0, 0);
        tblRodape.Controls.Add(txtResponsavelNome, 1, 0);
        tblRodape.Controls.Add(lblAssinatura, 0, 1);
        tblRodape.Dock = DockStyle.Fill;
        tblRodape.Location = new Point(9, 31);
        tblRodape.Margin = new Padding(3, 4, 3, 4);
        tblRodape.Name = "tblRodape";
        tblRodape.RowCount = 2;
        tblRodape.RowStyles.Add(new RowStyle());
        tblRodape.RowStyles.Add(new RowStyle());
        tblRodape.Size = new Size(1205, 0);
        tblRodape.TabIndex = 0;
        // 
        // lblResponsavel
        // 
        lblResponsavel.AutoSize = true;
        lblResponsavel.Dock = DockStyle.Fill;
        lblResponsavel.Location = new Point(3, 0);
        lblResponsavel.Name = "lblResponsavel";
        lblResponsavel.Size = new Size(199, 35);
        lblResponsavel.TabIndex = 0;
        lblResponsavel.Text = "Responsável pela Assinatura:";
        lblResponsavel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtResponsavelNome
        // 
        txtResponsavelNome.Dock = DockStyle.Fill;
        txtResponsavelNome.Location = new Point(208, 4);
        txtResponsavelNome.Margin = new Padding(3, 4, 3, 4);
        txtResponsavelNome.Name = "txtResponsavelNome";
        txtResponsavelNome.Size = new Size(994, 27);
        txtResponsavelNome.TabIndex = 1;
        // 
        // lblAssinatura
        // 
        lblAssinatura.AutoSize = true;
        tblRodape.SetColumnSpan(lblAssinatura, 2);
        lblAssinatura.Dock = DockStyle.Fill;
        lblAssinatura.Location = new Point(3, 35);
        lblAssinatura.Name = "lblAssinatura";
        lblAssinatura.Size = new Size(1199, 20);
        lblAssinatura.TabIndex = 2;
        lblAssinatura.Text = "Assinatura: ________________________";
        lblAssinatura.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // FrmCotacao
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1257, 1000);
        Controls.Add(tblMain);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(912, 784);
        Name = "FrmCotacao";
        Padding = new Padding(5, 5, 5, 5);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Elaboração de Cotação";
        tblMain.ResumeLayout(false);
        tblTop.ResumeLayout(false);
        grpEmpresa.ResumeLayout(false);
        tblEmpresa.ResumeLayout(false);
        tblEmpresa.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picLogotipo).EndInit();
        grpDocumento.ResumeLayout(false);
        tblDocumento.ResumeLayout(false);
        tblDocumento.PerformLayout();
        grpCliente.ResumeLayout(false);
        tblCliente.ResumeLayout(false);
        tblCliente.PerformLayout();
        pnlPlaceholder.ResumeLayout(false);
        grpRodape.ResumeLayout(false);
        tblRodape.ResumeLayout(false);
        tblRodape.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TableLayoutPanel tblMain;
    private System.Windows.Forms.TableLayoutPanel tblTop;
    private System.Windows.Forms.GroupBox grpEmpresa;
    private System.Windows.Forms.TableLayoutPanel tblEmpresa;
    private System.Windows.Forms.PictureBox picLogotipo;
    private System.Windows.Forms.Label lblNomeEmpresa;
    private System.Windows.Forms.TextBox txtNomeEmpresa;
    private System.Windows.Forms.Label lblEndereco;
    private System.Windows.Forms.TextBox txtEndereco;
    private System.Windows.Forms.Label lblInformacoesBancarias;
    private System.Windows.Forms.TextBox txtInformacoesBancarias;
    private System.Windows.Forms.Label lblNuitEmpresa;
    private System.Windows.Forms.TextBox txtNuitEmpresa;
    private System.Windows.Forms.Label lblContactos;
    private System.Windows.Forms.TextBox txtContactos;
    private System.Windows.Forms.Label lblEmail;
    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.GroupBox grpDocumento;
    private System.Windows.Forms.TableLayoutPanel tblDocumento;
    private System.Windows.Forms.Label lblNumeroCotacao;
    private System.Windows.Forms.TextBox txtNumeroCotacao;
    private System.Windows.Forms.Label lblDataEmissao;
    private System.Windows.Forms.DateTimePicker dtpDataEmissao;
    private System.Windows.Forms.GroupBox grpCliente;
    private System.Windows.Forms.TableLayoutPanel tblCliente;
    private System.Windows.Forms.Label lblClienteNome;
    private System.Windows.Forms.TextBox txtClienteNome;
    private System.Windows.Forms.Label lblClienteLocalizacao;
    private System.Windows.Forms.TextBox txtClienteLocalizacao;
    private System.Windows.Forms.Label lblClienteNuit;
    private System.Windows.Forms.TextBox txtClienteNuit;
    private System.Windows.Forms.Panel pnlPlaceholder;
    private System.Windows.Forms.Label lblPlaceholder;
    private System.Windows.Forms.GroupBox grpRodape;
    private System.Windows.Forms.TableLayoutPanel tblRodape;
    private System.Windows.Forms.Label lblResponsavel;
    private System.Windows.Forms.TextBox txtResponsavelNome;
    private System.Windows.Forms.Label lblAssinatura;
}
