namespace Main.Application.SystemImports
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbEmpresa = new ReaLTaiizor.Controls.DungeonComboBox();
            txtSenha = new ReaLTaiizor.Controls.DungeonTextBox();
            txtEmail = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel1 = new ReaLTaiizor.Controls.DungeonLabel();
            dungeonLabel2 = new ReaLTaiizor.Controls.DungeonLabel();
            dungeonLabel3 = new ReaLTaiizor.Controls.DungeonLabel();
            btnLogin = new ReaLTaiizor.Controls.DungeonButtonLeft();
            btnRegistrar = new ReaLTaiizor.Controls.DungeonButtonLeft();
            SuspendLayout();
            // 
            // cbEmpresa
            // 
            cbEmpresa.BackColor = Color.FromArgb(246, 246, 246);
            cbEmpresa.ColorA = Color.FromArgb(246, 132, 85);
            cbEmpresa.ColorB = Color.FromArgb(231, 108, 57);
            cbEmpresa.ColorC = Color.FromArgb(242, 241, 240);
            cbEmpresa.ColorD = Color.FromArgb(253, 252, 252);
            cbEmpresa.ColorE = Color.FromArgb(239, 237, 236);
            cbEmpresa.ColorF = Color.FromArgb(180, 180, 180);
            cbEmpresa.ColorG = Color.FromArgb(119, 119, 118);
            cbEmpresa.ColorH = Color.FromArgb(224, 222, 220);
            cbEmpresa.ColorI = Color.FromArgb(250, 249, 249);
            cbEmpresa.DrawMode = DrawMode.OwnerDrawFixed;
            cbEmpresa.DropDownHeight = 100;
            cbEmpresa.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEmpresa.Font = new Font("Segoe UI", 10F);
            cbEmpresa.ForeColor = Color.FromArgb(76, 76, 97);
            cbEmpresa.FormattingEnabled = true;
            cbEmpresa.HoverSelectionColor = Color.Empty;
            cbEmpresa.IntegralHeight = false;
            cbEmpresa.ItemHeight = 20;
            cbEmpresa.Location = new Point(12, 167);
            cbEmpresa.Name = "cbEmpresa";
            cbEmpresa.Size = new Size(237, 26);
            cbEmpresa.StartIndex = 0;
            cbEmpresa.TabIndex = 8;
            // 
            // txtSenha
            // 
            txtSenha.BackColor = Color.Transparent;
            txtSenha.BorderColor = Color.FromArgb(180, 180, 180);
            txtSenha.EdgeColor = Color.White;
            txtSenha.Font = new Font("Tahoma", 11F);
            txtSenha.ForeColor = Color.DimGray;
            txtSenha.Location = new Point(12, 97);
            txtSenha.MaxLength = 32767;
            txtSenha.Multiline = false;
            txtSenha.Name = "txtSenha";
            txtSenha.ReadOnly = false;
            txtSenha.Size = new Size(309, 28);
            txtSenha.TabIndex = 9;
            txtSenha.TextAlignment = HorizontalAlignment.Left;
            txtSenha.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.Transparent;
            txtEmail.BorderColor = Color.FromArgb(180, 180, 180);
            txtEmail.EdgeColor = Color.White;
            txtEmail.Font = new Font("Tahoma", 11F);
            txtEmail.ForeColor = Color.DimGray;
            txtEmail.Location = new Point(12, 32);
            txtEmail.MaxLength = 32767;
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = false;
            txtEmail.Size = new Size(309, 28);
            txtEmail.TabIndex = 10;
            txtEmail.TextAlignment = HorizontalAlignment.Left;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // dungeonLabel1
            // 
            dungeonLabel1.AutoSize = true;
            dungeonLabel1.BackColor = Color.Transparent;
            dungeonLabel1.Font = new Font("Segoe UI", 11F);
            dungeonLabel1.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel1.Location = new Point(12, 9);
            dungeonLabel1.Name = "dungeonLabel1";
            dungeonLabel1.Size = new Size(120, 20);
            dungeonLabel1.TabIndex = 11;
            dungeonLabel1.Text = "Digite seu Email:";
            // 
            // dungeonLabel2
            // 
            dungeonLabel2.AutoSize = true;
            dungeonLabel2.BackColor = Color.Transparent;
            dungeonLabel2.Font = new Font("Segoe UI", 11F);
            dungeonLabel2.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel2.Location = new Point(12, 144);
            dungeonLabel2.Name = "dungeonLabel2";
            dungeonLabel2.Size = new Size(149, 20);
            dungeonLabel2.TabIndex = 12;
            dungeonLabel2.Text = "Selecione a Empresa:";
            // 
            // dungeonLabel3
            // 
            dungeonLabel3.AutoSize = true;
            dungeonLabel3.BackColor = Color.Transparent;
            dungeonLabel3.Font = new Font("Segoe UI", 11F);
            dungeonLabel3.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel3.Location = new Point(12, 74);
            dungeonLabel3.Name = "dungeonLabel3";
            dungeonLabel3.Size = new Size(123, 20);
            dungeonLabel3.TabIndex = 13;
            dungeonLabel3.Text = "Digite sua Senha:";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BorderColor = Color.FromArgb(180, 180, 180);
            btnLogin.Font = new Font("Segoe UI", 12F);
            btnLogin.Image = null;
            btnLogin.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogin.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnLogin.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnLogin.Location = new Point(454, 167);
            btnLogin.Name = "btnLogin";
            btnLogin.PressedColorA = Color.FromArgb(226, 226, 226);
            btnLogin.PressedColorB = Color.FromArgb(237, 237, 237);
            btnLogin.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnLogin.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnLogin.Size = new Size(116, 37);
            btnLogin.TabIndex = 14;
            btnLogin.Text = "Login";
            btnLogin.TextAlignment = StringAlignment.Center;
            btnLogin.Click += btnLoginUser;
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
            btnRegistrar.Location = new Point(332, 167);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.PressedColorA = Color.FromArgb(226, 226, 226);
            btnRegistrar.PressedColorB = Color.FromArgb(237, 237, 237);
            btnRegistrar.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnRegistrar.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnRegistrar.Size = new Size(116, 37);
            btnRegistrar.TabIndex = 15;
            btnRegistrar.Text = "Registrar-se";
            btnRegistrar.TextAlignment = StringAlignment.Center;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 216);
            Controls.Add(btnRegistrar);
            Controls.Add(btnLogin);
            Controls.Add(dungeonLabel3);
            Controls.Add(dungeonLabel2);
            Controls.Add(dungeonLabel1);
            Controls.Add(txtEmail);
            Controls.Add(txtSenha);
            Controls.Add(cbEmpresa);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MinimumSize = new Size(261, 65);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "System Imports ";
            TransparencyKey = Color.Fuchsia;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.DungeonComboBox dungeonComboBox1;
        private ReaLTaiizor.Controls.DungeonComboBox cbEmpresa;
        private ReaLTaiizor.Controls.DungeonTextBox txtSenha;
        private ReaLTaiizor.Controls.DungeonTextBox txtEmail;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel1;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel2;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel3;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnLogin;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnRegistrar;
    }
}
