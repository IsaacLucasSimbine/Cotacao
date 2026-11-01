using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Cotacao.Models;

namespace Cotacao
{
    public class FrmHistorico : Form
    {
        private readonly FrmCotacao _frmCotacao;
        private DataGridView dgvHistorico = null!;
        private TextBox txtPesquisa = null!;
        private Label lblVazio = null!;
        private Button btnCarregarTemplate = null!;
        private Button btnReimprimir = null!;
        private Button btnEliminar = null!;
        private Button btnFechar = null!;

        public FrmHistorico(FrmCotacao frmCotacao)
        {
            _frmCotacao = frmCotacao ?? throw new ArgumentNullException(nameof(frmCotacao));
            InitializeComponent();
            CarregarHistorico();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Text = "Histórico de cotações";
            Font = Tema.Normal;
            BackColor = Tema.Fundo;
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(860, 520);
            var area = Screen.FromControl(this).WorkingArea;
            ClientSize = new Size(Math.Min(1080, area.Width - 40), Math.Min(680, area.Height - 80));

            // ── Cabeçalho ──
            var cabecalho = new Panel { Dock = DockStyle.Top, Height = 84, BackColor = Color.White };
            cabecalho.Controls.Add(new Label
            {
                Text = "Histórico de cotações",
                Font = Tema.Grande,
                ForeColor = Tema.Texto,
                BackColor = Color.White,
                AutoSize = true,
                Location = new Point(28, 14)
            });
            cabecalho.Controls.Add(new Label
            {
                Text = "Faz duplo clique numa cotação para a usar como template, ou escolhe uma ação em baixo.",
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

            // ── Grelha ──
            dgvHistorico = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false,
                GridColor = Tema.Borda
            };
            dgvHistorico.RowTemplate.Height = 42;

            var cab = dgvHistorico.ColumnHeadersDefaultCellStyle;
            cab.BackColor = Tema.FundoSuave;
            cab.ForeColor = Tema.TextoSuave;
            cab.Font = Tema.Cabecalho;
            cab.SelectionBackColor = Tema.FundoSuave;
            cab.SelectionForeColor = Tema.TextoSuave;
            cab.Padding = new Padding(10, 0, 10, 0);

            var cel = dgvHistorico.DefaultCellStyle;
            cel.BackColor = Color.White;
            cel.ForeColor = Tema.Texto;
            cel.Font = Tema.Normal;
            cel.SelectionBackColor = Tema.PrimariaSuave;
            cel.SelectionForeColor = Tema.Texto;
            cel.Padding = new Padding(10, 0, 10, 0);

            var alt = dgvHistorico.AlternatingRowsDefaultCellStyle;
            alt.BackColor = Tema.FundoSuave;
            alt.SelectionBackColor = Tema.PrimariaSuave;
            alt.SelectionForeColor = Tema.Texto;

            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Visible = false
            });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nº Cotação",
                HeaderText = "Nº cotação",
                DataPropertyName = "Numero",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { Font = Tema.Negrito }
            });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                FillWeight = 40
            });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Data Emissão",
                HeaderText = "Data de emissão",
                DataPropertyName = "Data",
                FillWeight = 20
            });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total Geral",
                HeaderText = "Total geral (MT)",
                DataPropertyName = "Total",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2",
                    Font = Tema.Negrito
                }
            });
            dgvHistorico.Columns["Total Geral"]!.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHistorico.CellDoubleClick += DgvHistorico_CellDoubleClick;

            // Mensagem quando não há resultados
            lblVazio = new Label
            {
                Dock = DockStyle.Fill,
                Visible = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = Tema.Titulo,
                ForeColor = Tema.TextoSuave,
                BackColor = Color.White
            };
            dgvHistorico.Controls.Add(lblVazio);

            // ── Pesquisa ──
            txtPesquisa = new TextBox
            {
                Name = "txtPesquisa",
                PlaceholderText = "Pesquisar por cliente ou nº de cotação"
            };
            txtPesquisa.TextChanged += TxtPesquisa_TextChanged;

            var barraPesquisa = new Panel { Dock = DockStyle.Top, Height = 54, BackColor = Color.White };
            barraPesquisa.Controls.Add(new CampoTexto(txtPesquisa)
            {
                Location = new Point(0, 0),
                Size = new Size(400, 42)
            });

            var cartao = new Cartao { Dock = DockStyle.Fill };
            cartao.Controls.Add(dgvHistorico);   // Fill primeiro
            cartao.Controls.Add(barraPesquisa);  // depois o Top

            var conteudo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Tema.Fundo,
                Padding = new Padding(24, 20, 24, 20)
            };
            conteudo.Controls.Add(cartao);

            // ── Rodapé com os botões ──
            btnCarregarTemplate = CriarBotao("Carregar como template", 200, primario: true);
            btnCarregarTemplate.Margin = new Padding(0);
            btnReimprimir = CriarBotao("Reimprimir / Ver PDF", 180);
            btnEliminar = CriarBotao("Eliminar cotação", 160, perigo: true);
            btnFechar = CriarBotao("Fechar", 110);

            btnCarregarTemplate.Click += BtnCarregarTemplate_Click;
            btnReimprimir.Click += BtnReimprimir_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnFechar.Click += (s, e) => Close();
            CancelButton = btnFechar;   // Esc fecha a janela

            var rodape = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(24, 0, 24, 0),
                ColumnCount = 5,
                RowCount = 1
            };
            rodape.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rodape.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rodape.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rodape.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            rodape.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rodape.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            rodape.Controls.Add(btnCarregarTemplate, 0, 0);
            rodape.Controls.Add(btnReimprimir, 1, 0);
            rodape.Controls.Add(btnEliminar, 2, 0);
            rodape.Controls.Add(btnFechar, 4, 0);
            rodape.Paint += (s, e) =>
            {
                using var caneta = new Pen(Tema.Borda);
                e.Graphics.DrawLine(caneta, 0, 0, rodape.Width, 0);
            };

            Controls.Add(conteudo);   // Fill primeiro
            Controls.Add(rodape);
            Controls.Add(cabecalho);

            ResumeLayout(false);
            PerformLayout();
        }

        private static BotaoModerno CriarBotao(string texto, int largura, bool primario = false, bool perigo = false)
        {
            return new BotaoModerno
            {
                Text = texto,
                Primario = primario,
                Perigo = perigo,
                Size = new Size(largura, 42),
                Margin = new Padding(8, 0, 0, 0),
                Anchor = AnchorStyles.None
            };
        } 
         // ==================== LOAD & SEARCH ====================
        private void CarregarHistorico(string? filtro = null)
        {
            try
            {
                dgvHistorico.Rows.Clear();

                using var connection = new SqliteConnection("Data Source=cotacoes.db");
                connection.Open();

                string sql = @"SELECT id, numero_cotacao, cliente_nome, data_emissao, total_geral
                               FROM cotacao";
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    sql += " WHERE cliente_nome LIKE @filtro OR numero_cotacao LIKE @filtro";
                }
                sql += " ORDER BY id DESC";

                using var command = connection.CreateCommand();
                command.CommandText = sql;
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    command.Parameters.AddWithValue("@filtro", $"%{filtro}%");
                }

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dgvHistorico.Rows.Add(
                        reader["id"],
                        reader["numero_cotacao"],
                        reader["cliente_nome"],
                        reader["data_emissao"],
                        reader["total_geral"]);
                }
                                lblVazio.Text = string.IsNullOrWhiteSpace(filtro)
                    ? "Ainda não há cotações guardadas."
                    : "Nenhuma cotação encontrada para esta pesquisa.";
                lblVazio.Visible = dgvHistorico.Rows.Count == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar hist\u00f3rico: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarHistorico(txtPesquisa.Text);
        }

        // ==================== HELPERS ====================
        private int ObterIdSelecionado()
        {
            if (dgvHistorico.CurrentRow == null || dgvHistorico.CurrentRow.IsNewRow)
                return -1;

            var value = dgvHistorico.CurrentRow.Cells["ID"].Value;
            return value == null ? -1 : Convert.ToInt32(value);
        }

        private Cotacao.Models.Cotacao? ObterCotacaoPorId(int id)
        {
            using var connection = new SqliteConnection("Data Source=cotacoes.db");
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT id, numero_cotacao, data_emissao, cliente_nome,
                                       cliente_localizacao, cliente_nuit, subtotal,
                                       taxa_aplicavel, valor_taxa, total_geral,
                                       responsavel_nome
                                FROM cotacao
                                WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            var cotacao = new Cotacao.Models.Cotacao
            {
                Id = id,
                NumeroCotacao = reader["numero_cotacao"]?.ToString() ?? string.Empty,
                DataEmissao = DateTime.Parse(reader["data_emissao"]?.ToString() ?? string.Empty),
                ClienteNome = reader["cliente_nome"]?.ToString() ?? string.Empty,
                ClienteLocalizacao = reader["cliente_localizacao"]?.ToString() ?? string.Empty,
                ClienteNuit = reader["cliente_nuit"]?.ToString(),
                Subtotal = Convert.ToDecimal(reader["subtotal"]),
                TaxaAplicavel = Convert.ToDecimal(reader["taxa_aplicavel"]),
                ValorTaxa = Convert.ToDecimal(reader["valor_taxa"]),
                TotalGeral = Convert.ToDecimal(reader["total_geral"]),
                ResponsavelNome = reader["responsavel_nome"]?.ToString() ?? string.Empty
            };

            using var cmdArtigos = connection.CreateCommand();
            cmdArtigos.CommandText = @"SELECT id, cotacao_id, numero_item, designacao,
                                              quantidade, preco_unitario, preco_total
                                       FROM cotacao_artigo
                                       WHERE cotacao_id = @id
                                       ORDER BY numero_item";
            cmdArtigos.Parameters.AddWithValue("@id", id);
            using var readerArtigos = cmdArtigos.ExecuteReader();
            while (readerArtigos.Read())
            {
                cotacao.Artigos.Add(new CotacaoArtigo
                {
                    Id = Convert.ToInt32(readerArtigos["id"]),
                    CotacaoId = id,
                    NumeroItem = Convert.ToInt32(readerArtigos["numero_item"]),
                    Designacao = readerArtigos["designacao"]?.ToString() ?? string.Empty,
                    Quantidade = Convert.ToDecimal(readerArtigos["quantidade"]),
                    PrecoUnitario = Convert.ToDecimal(readerArtigos["preco_unitario"]),
                    PrecoTotal = Convert.ToDecimal(readerArtigos["preco_total"])
                });
            }

            return cotacao;
        }        // ==================== ACTIONS ====================
        private void DgvHistorico_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = ObterIdSelecionado();
            if (id < 0) return;

            _frmCotacao.CarregarCotacaoComoTemplate(id);
            Close();
        }

        private void BtnCarregarTemplate_Click(object sender, EventArgs e)
        {
            int id = ObterIdSelecionado();
            if (id < 0)
            {
                MessageBox.Show("Selecione uma cota\u00e7\u00e3o na lista.", "Aten\u00e7\u00e3o",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _frmCotacao.CarregarCotacaoComoTemplate(id);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cota\u00e7\u00e3o como template: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnReimprimir_Click(object sender, EventArgs e)
        {
            int id = ObterIdSelecionado();
            if (id < 0)
            {
                MessageBox.Show("Selecione uma cota\u00e7\u00e3o na lista.", "Aten\u00e7\u00e3o",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cotacao.Models.Cotacao? cotacao = ObterCotacaoPorId(id);
                if (cotacao == null)
                {
                    MessageBox.Show("Cota\u00e7\u00e3o n\u00e3o encontrada.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var saveDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Cota\u00e7\u00e3o_{cotacao.NumeroCotacao}.pdf",
                    Title = "Guardar PDF"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await PdfExporter.ExportarParaPdfAsync(cotacao, cotacao.Artigos, saveDialog.FileName);
                    MessageBox.Show("PDF gerado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar PDF: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            int id = ObterIdSelecionado();
            if (id < 0)
            {
                MessageBox.Show("Selecione uma cota\u00e7\u00e3o na lista.", "Aten\u00e7\u00e3o",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Tem a certeza que deseja eliminar esta cota\u00e7\u00e3o e todos os seus artigos?",
                "Confirmar Elimina\u00e7\u00e3o",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var connection = new SqliteConnection("Data Source=cotacoes.db");
                connection.Open();

                using var transaction = connection.BeginTransaction();
                try
                {
                    using var cmdArtigos = connection.CreateCommand();
                    cmdArtigos.Transaction = transaction;
                    cmdArtigos.CommandText = "DELETE FROM cotacao_artigo WHERE cotacao_id = @id";
                    cmdArtigos.Parameters.AddWithValue("@id", id);
                    cmdArtigos.ExecuteNonQuery();

                    using var cmdCotacao = connection.CreateCommand();
                    cmdCotacao.Transaction = transaction;
                    cmdCotacao.CommandText = "DELETE FROM cotacao WHERE id = @id";
                    cmdCotacao.Parameters.AddWithValue("@id", id);
                    cmdCotacao.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                MessageBox.Show("Cota\u00e7\u00e3o eliminada com sucesso.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarHistorico(txtPesquisa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao eliminar cota\u00e7\u00e3o: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}