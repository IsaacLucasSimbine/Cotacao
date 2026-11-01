namespace Cotacao
{
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            SuspendLayout();
            Text = "Elaboração de Cotação";
            ClientSize = new Size(1280, 900);
            MinimumSize = new Size(912, 784);
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(5);

            btnGuardar = new Button();
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Text = "Guardar Cotação";
            btnGuardar.Size = new Size(120, 40);
            btnGuardar.Location = new Point(1020, 850);
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar = new Button();
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.Location = new Point(1150, 850);
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Click += btnCancelar_Click;

            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnCancelar);

            ResumeLayout(false);
        }

        #endregion
    }
}