namespace Main.Application.SystemImports
{
    partial class RegistroUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRegistrar = new ReaLTaiizor.Controls.DungeonButtonLeft();
            lblEmail = new ReaLTaiizor.Controls.DungeonLabel();
            txtEmail = new ReaLTaiizor.Controls.DungeonTextBox();
            metroDivider1 = new ReaLTaiizor.Controls.MetroDivider();
            headerLabel = new ReaLTaiizor.Controls.DungeonHeaderLabel();
            txtSenha = new ReaLTaiizor.Controls.DungeonTextBox();
            lblSenha = new ReaLTaiizor.Controls.DungeonLabel();
            SuspendLayout();
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.Transparent;
            btnRegistrar.BorderColor = Color.FromArgb(180, 180, 180);
            btnRegistrar.Font = new Font("Segoe UI", 12F);
            btnRegistrar.Image = null;
            btnRegistrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnRegistrar.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnRegistrar.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnRegistrar.Location = new Point(353, 243);
            btnRegistrar.Margin = new Padding(3, 4, 3, 4);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.PressedColorA = Color.FromArgb(226, 226, 226);
            btnRegistrar.PressedColorB = Color.FromArgb(237, 237, 237);
            btnRegistrar.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnRegistrar.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnRegistrar.Size = new Size(144, 40);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.TextAlignment = StringAlignment.Center;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.BackColor = Color.Transparent;
            lblEmail.Font = new Font("Segoe UI", 11F);
            lblEmail.ForeColor = Color.FromArgb(76, 76, 77);
            lblEmail.Location = new Point(14, 72);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(62, 25);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.Transparent;
            txtEmail.BorderColor = Color.FromArgb(180, 180, 180);
            txtEmail.EdgeColor = Color.White;
            txtEmail.Font = new Font("Tahoma", 11F);
            txtEmail.ForeColor = Color.DimGray;
            txtEmail.Location = new Point(14, 103);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.MaxLength = 32767;
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = false;
            txtEmail.Size = new Size(309, 33);
            txtEmail.TabIndex = 2;
            txtEmail.TextAlignment = HorizontalAlignment.Left;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // metroDivider1
            // 
            metroDivider1.IsDerivedStyle = true;
            metroDivider1.Location = new Point(1, 39);
            metroDivider1.Margin = new Padding(3, 4, 3, 4);
            metroDivider1.Name = "metroDivider1";
            metroDivider1.Orientation = ReaLTaiizor.Enum.Metro.DividerStyle.Horizontal;
            metroDivider1.Size = new Size(517, 4);
            metroDivider1.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            metroDivider1.StyleManager = null;
            metroDivider1.TabIndex = 5;
            metroDivider1.Text = "metroDivider1";
            metroDivider1.ThemeAuthor = "Taiizor";
            metroDivider1.ThemeName = "MetroLight";
            metroDivider1.Thickness = 1;
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.BackColor = Color.Transparent;
            headerLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            headerLabel.ForeColor = Color.FromArgb(76, 76, 77);
            headerLabel.Location = new Point(14, 8);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(210, 25);
            headerLabel.TabIndex = 4;
            headerLabel.Text = "Cadastro de Usuário - ";
            // 
            // txtSenha
            // 
            txtSenha.BackColor = Color.Transparent;
            txtSenha.BorderColor = Color.FromArgb(180, 180, 180);
            txtSenha.EdgeColor = Color.White;
            txtSenha.Font = new Font("Tahoma", 11F);
            txtSenha.ForeColor = Color.DimGray;
            txtSenha.Location = new Point(14, 199);
            txtSenha.Margin = new Padding(3, 4, 3, 4);
            txtSenha.MaxLength = 32767;
            txtSenha.Multiline = false;
            txtSenha.Name = "txtSenha";
            txtSenha.ReadOnly = false;
            txtSenha.Size = new Size(309, 33);
            txtSenha.TabIndex = 7;
            txtSenha.TextAlignment = HorizontalAlignment.Left;
            txtSenha.UseSystemPasswordChar = false;
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.BackColor = Color.Transparent;
            lblSenha.Font = new Font("Segoe UI", 11F);
            lblSenha.ForeColor = Color.FromArgb(76, 76, 77);
            lblSenha.Location = new Point(14, 168);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(68, 25);
            lblSenha.TabIndex = 6;
            lblSenha.Text = "Senha:";
            // 
            // RegistroUsuario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 299);
            Controls.Add(txtSenha);
            Controls.Add(lblSenha);
            Controls.Add(metroDivider1);
            Controls.Add(headerLabel);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(btnRegistrar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "RegistroUsuario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registar Usuário";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.DungeonButtonLeft btnRegistrar;
        private ReaLTaiizor.Controls.DungeonLabel lblEmail;
        private ReaLTaiizor.Controls.DungeonTextBox txtEmail;
        private ReaLTaiizor.Controls.MetroDivider metroDivider1;
        private ReaLTaiizor.Controls.DungeonHeaderLabel headerLabel;
        private ReaLTaiizor.Controls.DungeonTextBox txtSenha;
        private ReaLTaiizor.Controls.DungeonLabel lblSenha;
    }
}