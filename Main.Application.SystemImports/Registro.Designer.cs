namespace Main.Application.SystemImports
{
    partial class Registro
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
            dungeonLabel1 = new ReaLTaiizor.Controls.DungeonLabel();
            txtNomeEmpresa = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel2 = new ReaLTaiizor.Controls.DungeonLabel();
            dungeonLabel3 = new ReaLTaiizor.Controls.DungeonLabel();
            txtRua = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel4 = new ReaLTaiizor.Controls.DungeonLabel();
            txtBairro = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel5 = new ReaLTaiizor.Controls.DungeonLabel();
            txtNumero = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel6 = new ReaLTaiizor.Controls.DungeonLabel();
            txtComplemento = new ReaLTaiizor.Controls.DungeonTextBox();
            dungeonLabel7 = new ReaLTaiizor.Controls.DungeonLabel();
            cbEstado = new ReaLTaiizor.Controls.DungeonComboBox();
            dungeonLabel8 = new ReaLTaiizor.Controls.DungeonLabel();
            dungeonLabel9 = new ReaLTaiizor.Controls.DungeonLabel();
            cbCidade = new ReaLTaiizor.Controls.DungeonComboBox();
            progressBarCnpj = new ReaLTaiizor.Controls.PoisonProgressSpinner();
            txtDescricaoEmpresa = new ReaLTaiizor.Controls.DungeonTextBox();
            txtCNPJ = new ReaLTaiizor.Controls.DungeonTextBox();
            btnLimpar = new ReaLTaiizor.Controls.DungeonButtonLeft();
            metroDivider1 = new ReaLTaiizor.Controls.MetroDivider();
            dungeonHeaderLabel1 = new ReaLTaiizor.Controls.DungeonHeaderLabel();
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
            btnRegistrar.Location = new Point(611, 341);
            btnRegistrar.Margin = new Padding(3, 4, 3, 4);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.PressedColorA = Color.FromArgb(226, 226, 226);
            btnRegistrar.PressedColorB = Color.FromArgb(237, 237, 237);
            btnRegistrar.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnRegistrar.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnRegistrar.Size = new Size(169, 40);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "Registrar-se";
            btnRegistrar.TextAlignment = StringAlignment.Center;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // dungeonLabel1
            // 
            dungeonLabel1.AutoSize = true;
            dungeonLabel1.BackColor = Color.Transparent;
            dungeonLabel1.Font = new Font("Segoe UI", 11F);
            dungeonLabel1.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel1.Location = new Point(14, 56);
            dungeonLabel1.Name = "dungeonLabel1";
            dungeonLabel1.Size = new Size(170, 25);
            dungeonLabel1.TabIndex = 1;
            dungeonLabel1.Text = "Nome da Empresa:";
            // 
            // txtNomeEmpresa
            // 
            txtNomeEmpresa.BackColor = Color.Transparent;
            txtNomeEmpresa.BorderColor = Color.FromArgb(180, 180, 180);
            txtNomeEmpresa.EdgeColor = Color.White;
            txtNomeEmpresa.Font = new Font("Tahoma", 11F);
            txtNomeEmpresa.ForeColor = Color.DimGray;
            txtNomeEmpresa.Location = new Point(14, 87);
            txtNomeEmpresa.Margin = new Padding(3, 4, 3, 4);
            txtNomeEmpresa.MaxLength = 32767;
            txtNomeEmpresa.Multiline = false;
            txtNomeEmpresa.Name = "txtNomeEmpresa";
            txtNomeEmpresa.ReadOnly = false;
            txtNomeEmpresa.Size = new Size(243, 33);
            txtNomeEmpresa.TabIndex = 4;
            txtNomeEmpresa.TextAlignment = HorizontalAlignment.Left;
            txtNomeEmpresa.UseSystemPasswordChar = false;
            // 
            // dungeonLabel2
            // 
            dungeonLabel2.AutoSize = true;
            dungeonLabel2.BackColor = Color.Transparent;
            dungeonLabel2.Font = new Font("Segoe UI", 11F);
            dungeonLabel2.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel2.Location = new Point(14, 155);
            dungeonLabel2.Name = "dungeonLabel2";
            dungeonLabel2.Size = new Size(203, 25);
            dungeonLabel2.TabIndex = 5;
            dungeonLabel2.Text = "Descreva sua Empresa:";
            // 
            // dungeonLabel3
            // 
            dungeonLabel3.AutoSize = true;
            dungeonLabel3.BackColor = Color.Transparent;
            dungeonLabel3.Font = new Font("Segoe UI", 11F);
            dungeonLabel3.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel3.Location = new Point(14, 311);
            dungeonLabel3.Name = "dungeonLabel3";
            dungeonLabel3.Size = new Size(59, 25);
            dungeonLabel3.TabIndex = 9;
            dungeonLabel3.Text = "CNPJ:";
            // 
            // txtRua
            // 
            txtRua.BackColor = Color.Transparent;
            txtRua.BorderColor = Color.FromArgb(180, 180, 180);
            txtRua.EdgeColor = Color.White;
            txtRua.Font = new Font("Tahoma", 11F);
            txtRua.ForeColor = Color.DimGray;
            txtRua.Location = new Point(286, 87);
            txtRua.Margin = new Padding(3, 4, 3, 4);
            txtRua.MaxLength = 32767;
            txtRua.Multiline = false;
            txtRua.Name = "txtRua";
            txtRua.ReadOnly = false;
            txtRua.Size = new Size(243, 33);
            txtRua.TabIndex = 12;
            txtRua.TextAlignment = HorizontalAlignment.Left;
            txtRua.UseSystemPasswordChar = false;
            // 
            // dungeonLabel4
            // 
            dungeonLabel4.AutoSize = true;
            dungeonLabel4.BackColor = Color.Transparent;
            dungeonLabel4.Font = new Font("Segoe UI", 11F);
            dungeonLabel4.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel4.Location = new Point(286, 56);
            dungeonLabel4.Name = "dungeonLabel4";
            dungeonLabel4.Size = new Size(48, 25);
            dungeonLabel4.TabIndex = 11;
            dungeonLabel4.Text = "Rua:";
            // 
            // txtBairro
            // 
            txtBairro.BackColor = Color.Transparent;
            txtBairro.BorderColor = Color.FromArgb(180, 180, 180);
            txtBairro.EdgeColor = Color.White;
            txtBairro.Font = new Font("Tahoma", 11F);
            txtBairro.ForeColor = Color.DimGray;
            txtBairro.Location = new Point(286, 185);
            txtBairro.Margin = new Padding(3, 4, 3, 4);
            txtBairro.MaxLength = 32767;
            txtBairro.Multiline = false;
            txtBairro.Name = "txtBairro";
            txtBairro.ReadOnly = false;
            txtBairro.Size = new Size(243, 33);
            txtBairro.TabIndex = 14;
            txtBairro.TextAlignment = HorizontalAlignment.Left;
            txtBairro.UseSystemPasswordChar = false;
            // 
            // dungeonLabel5
            // 
            dungeonLabel5.AutoSize = true;
            dungeonLabel5.BackColor = Color.Transparent;
            dungeonLabel5.Font = new Font("Segoe UI", 11F);
            dungeonLabel5.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel5.Location = new Point(286, 155);
            dungeonLabel5.Name = "dungeonLabel5";
            dungeonLabel5.Size = new Size(67, 25);
            dungeonLabel5.TabIndex = 13;
            dungeonLabel5.Text = "Bairro:";
            // 
            // txtNumero
            // 
            txtNumero.BackColor = Color.Transparent;
            txtNumero.BorderColor = Color.FromArgb(180, 180, 180);
            txtNumero.EdgeColor = Color.White;
            txtNumero.Font = new Font("Tahoma", 11F);
            txtNumero.ForeColor = Color.DimGray;
            txtNumero.Location = new Point(537, 87);
            txtNumero.Margin = new Padding(3, 4, 3, 4);
            txtNumero.MaxLength = 32767;
            txtNumero.Multiline = false;
            txtNumero.Name = "txtNumero";
            txtNumero.ReadOnly = false;
            txtNumero.Size = new Size(243, 33);
            txtNumero.TabIndex = 16;
            txtNumero.TextAlignment = HorizontalAlignment.Left;
            txtNumero.UseSystemPasswordChar = false;
            // 
            // dungeonLabel6
            // 
            dungeonLabel6.AutoSize = true;
            dungeonLabel6.BackColor = Color.Transparent;
            dungeonLabel6.Font = new Font("Segoe UI", 11F);
            dungeonLabel6.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel6.Location = new Point(537, 56);
            dungeonLabel6.Name = "dungeonLabel6";
            dungeonLabel6.Size = new Size(85, 25);
            dungeonLabel6.TabIndex = 15;
            dungeonLabel6.Text = "Número:";
            // 
            // txtComplemento
            // 
            txtComplemento.BackColor = Color.Transparent;
            txtComplemento.BorderColor = Color.FromArgb(180, 180, 180);
            txtComplemento.EdgeColor = Color.White;
            txtComplemento.Font = new Font("Tahoma", 11F);
            txtComplemento.ForeColor = Color.DimGray;
            txtComplemento.Location = new Point(537, 185);
            txtComplemento.Margin = new Padding(3, 4, 3, 4);
            txtComplemento.MaxLength = 32767;
            txtComplemento.Multiline = false;
            txtComplemento.Name = "txtComplemento";
            txtComplemento.ReadOnly = false;
            txtComplemento.Size = new Size(243, 33);
            txtComplemento.TabIndex = 18;
            txtComplemento.Text = "(Opcional)";
            txtComplemento.TextAlignment = HorizontalAlignment.Left;
            txtComplemento.UseSystemPasswordChar = false;
            // 
            // dungeonLabel7
            // 
            dungeonLabel7.AutoSize = true;
            dungeonLabel7.BackColor = Color.Transparent;
            dungeonLabel7.Font = new Font("Segoe UI", 11F);
            dungeonLabel7.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel7.Location = new Point(537, 155);
            dungeonLabel7.Name = "dungeonLabel7";
            dungeonLabel7.Size = new Size(135, 25);
            dungeonLabel7.TabIndex = 17;
            dungeonLabel7.Text = "Complemento:";
            // 
            // cbEstado
            // 
            cbEstado.BackColor = Color.FromArgb(246, 246, 246);
            cbEstado.ColorA = Color.FromArgb(246, 132, 85);
            cbEstado.ColorB = Color.FromArgb(231, 108, 57);
            cbEstado.ColorC = Color.FromArgb(242, 241, 240);
            cbEstado.ColorD = Color.FromArgb(253, 252, 252);
            cbEstado.ColorE = Color.FromArgb(239, 237, 236);
            cbEstado.ColorF = Color.FromArgb(180, 180, 180);
            cbEstado.ColorG = Color.FromArgb(119, 119, 118);
            cbEstado.ColorH = Color.FromArgb(224, 222, 220);
            cbEstado.ColorI = Color.FromArgb(250, 249, 249);
            cbEstado.DrawMode = DrawMode.OwnerDrawFixed;
            cbEstado.DropDownHeight = 100;
            cbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstado.Font = new Font("Segoe UI", 10F);
            cbEstado.ForeColor = Color.FromArgb(76, 76, 97);
            cbEstado.FormattingEnabled = true;
            cbEstado.HoverSelectionColor = Color.Empty;
            cbEstado.IntegralHeight = false;
            cbEstado.ItemHeight = 20;
            cbEstado.Location = new Point(286, 279);
            cbEstado.Margin = new Padding(3, 4, 3, 4);
            cbEstado.Name = "cbEstado";
            cbEstado.Size = new Size(243, 26);
            cbEstado.StartIndex = 0;
            cbEstado.TabIndex = 19;
            cbEstado.SelectedIndexChanged += cbEstado_SelectedIndexChanged;
            // 
            // dungeonLabel8
            // 
            dungeonLabel8.AutoSize = true;
            dungeonLabel8.BackColor = Color.Transparent;
            dungeonLabel8.Font = new Font("Segoe UI", 11F);
            dungeonLabel8.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel8.Location = new Point(286, 251);
            dungeonLabel8.Name = "dungeonLabel8";
            dungeonLabel8.Size = new Size(72, 25);
            dungeonLabel8.TabIndex = 20;
            dungeonLabel8.Text = "Estado:";
            // 
            // dungeonLabel9
            // 
            dungeonLabel9.AutoSize = true;
            dungeonLabel9.BackColor = Color.Transparent;
            dungeonLabel9.Enabled = false;
            dungeonLabel9.Font = new Font("Segoe UI", 11F);
            dungeonLabel9.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonLabel9.Location = new Point(537, 251);
            dungeonLabel9.Name = "dungeonLabel9";
            dungeonLabel9.Size = new Size(75, 25);
            dungeonLabel9.TabIndex = 22;
            dungeonLabel9.Text = "Cidade:";
            dungeonLabel9.Visible = false;
            // 
            // cbCidade
            // 
            cbCidade.BackColor = Color.FromArgb(246, 246, 246);
            cbCidade.ColorA = Color.FromArgb(246, 132, 85);
            cbCidade.ColorB = Color.FromArgb(231, 108, 57);
            cbCidade.ColorC = Color.FromArgb(242, 241, 240);
            cbCidade.ColorD = Color.FromArgb(253, 252, 252);
            cbCidade.ColorE = Color.FromArgb(239, 237, 236);
            cbCidade.ColorF = Color.FromArgb(180, 180, 180);
            cbCidade.ColorG = Color.FromArgb(119, 119, 118);
            cbCidade.ColorH = Color.FromArgb(224, 222, 220);
            cbCidade.ColorI = Color.FromArgb(250, 249, 249);
            cbCidade.DrawMode = DrawMode.OwnerDrawFixed;
            cbCidade.DropDownHeight = 100;
            cbCidade.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCidade.Enabled = false;
            cbCidade.Font = new Font("Segoe UI", 10F);
            cbCidade.ForeColor = Color.FromArgb(76, 76, 97);
            cbCidade.FormattingEnabled = true;
            cbCidade.HoverSelectionColor = Color.Empty;
            cbCidade.IntegralHeight = false;
            cbCidade.ItemHeight = 20;
            cbCidade.Location = new Point(537, 279);
            cbCidade.Margin = new Padding(3, 4, 3, 4);
            cbCidade.Name = "cbCidade";
            cbCidade.Size = new Size(243, 26);
            cbCidade.StartIndex = 0;
            cbCidade.TabIndex = 21;
            cbCidade.Visible = false;
            // 
            // progressBarCnpj
            // 
            progressBarCnpj.BackColor = SystemColors.Control;
            progressBarCnpj.CustomBackground = true;
            progressBarCnpj.EnsureVisible = false;
            progressBarCnpj.ForeColor = SystemColors.ControlText;
            progressBarCnpj.Location = new Point(264, 341);
            progressBarCnpj.Margin = new Padding(3, 4, 3, 4);
            progressBarCnpj.Maximum = 100;
            progressBarCnpj.Name = "progressBarCnpj";
            progressBarCnpj.Size = new Size(30, 40);
            progressBarCnpj.Speed = 2F;
            progressBarCnpj.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Orange;
            progressBarCnpj.TabIndex = 23;
            progressBarCnpj.Text = "poisonProgressSpinner1";
            progressBarCnpj.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            progressBarCnpj.UseCustomBackColor = true;
            progressBarCnpj.UseSelectable = true;
            // 
            // txtDescricaoEmpresa
            // 
            txtDescricaoEmpresa.BackColor = Color.Transparent;
            txtDescricaoEmpresa.BorderColor = Color.FromArgb(180, 180, 180);
            txtDescricaoEmpresa.EdgeColor = Color.White;
            txtDescricaoEmpresa.Font = new Font("Tahoma", 11F);
            txtDescricaoEmpresa.ForeColor = Color.DimGray;
            txtDescricaoEmpresa.Location = new Point(14, 185);
            txtDescricaoEmpresa.Margin = new Padding(3, 4, 3, 4);
            txtDescricaoEmpresa.MaxLength = 32767;
            txtDescricaoEmpresa.Multiline = true;
            txtDescricaoEmpresa.Name = "txtDescricaoEmpresa";
            txtDescricaoEmpresa.ReadOnly = false;
            txtDescricaoEmpresa.Size = new Size(243, 91);
            txtDescricaoEmpresa.TabIndex = 6;
            txtDescricaoEmpresa.Text = "O que sua empresa faz?";
            txtDescricaoEmpresa.TextAlignment = HorizontalAlignment.Left;
            txtDescricaoEmpresa.UseSystemPasswordChar = false;
            // 
            // txtCNPJ
            // 
            txtCNPJ.BackColor = Color.Transparent;
            txtCNPJ.BorderColor = Color.FromArgb(180, 180, 180);
            txtCNPJ.EdgeColor = Color.White;
            txtCNPJ.Font = new Font("Tahoma", 11F);
            txtCNPJ.ForeColor = Color.DimGray;
            txtCNPJ.Location = new Point(14, 341);
            txtCNPJ.Margin = new Padding(3, 4, 3, 4);
            txtCNPJ.MaxLength = 30;
            txtCNPJ.Multiline = false;
            txtCNPJ.Name = "txtCNPJ";
            txtCNPJ.ReadOnly = false;
            txtCNPJ.Size = new Size(243, 33);
            txtCNPJ.TabIndex = 24;
            txtCNPJ.TextAlignment = HorizontalAlignment.Left;
            txtCNPJ.UseSystemPasswordChar = false;
            txtCNPJ.TextChanged += txtCNPJ_TextChanged;
            // 
            // btnLimpar
            // 
            btnLimpar.BackColor = Color.Transparent;
            btnLimpar.BorderColor = Color.FromArgb(180, 180, 180);
            btnLimpar.Font = new Font("Segoe UI", 12F);
            btnLimpar.Image = null;
            btnLimpar.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpar.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnLimpar.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnLimpar.Location = new Point(435, 341);
            btnLimpar.Margin = new Padding(3, 4, 3, 4);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.PressedColorA = Color.FromArgb(226, 226, 226);
            btnLimpar.PressedColorB = Color.FromArgb(237, 237, 237);
            btnLimpar.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnLimpar.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnLimpar.Size = new Size(169, 40);
            btnLimpar.TabIndex = 25;
            btnLimpar.Text = "Limpar";
            btnLimpar.TextAlignment = StringAlignment.Center;
            btnLimpar.Click += btnLimpar_Click;
            // 
            // metroDivider1
            // 
            metroDivider1.IsDerivedStyle = true;
            metroDivider1.Location = new Point(1, 39);
            metroDivider1.Margin = new Padding(3, 4, 3, 4);
            metroDivider1.Name = "metroDivider1";
            metroDivider1.Orientation = ReaLTaiizor.Enum.Metro.DividerStyle.Horizontal;
            metroDivider1.Size = new Size(813, 4);
            metroDivider1.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            metroDivider1.StyleManager = null;
            metroDivider1.TabIndex = 3;
            metroDivider1.Text = "metroDivider1";
            metroDivider1.ThemeAuthor = "Taiizor";
            metroDivider1.ThemeName = "MetroLight";
            metroDivider1.Thickness = 1;
            // 
            // dungeonHeaderLabel1
            // 
            dungeonHeaderLabel1.AutoSize = true;
            dungeonHeaderLabel1.BackColor = Color.Transparent;
            dungeonHeaderLabel1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dungeonHeaderLabel1.ForeColor = Color.FromArgb(76, 76, 77);
            dungeonHeaderLabel1.Location = new Point(14, 12);
            dungeonHeaderLabel1.Name = "dungeonHeaderLabel1";
            dungeonHeaderLabel1.Size = new Size(198, 25);
            dungeonHeaderLabel1.TabIndex = 2;
            dungeonHeaderLabel1.Text = "Cadastro de Empresa";
            // 
            // Registro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 401);
            Controls.Add(btnLimpar);
            Controls.Add(txtCNPJ);
            Controls.Add(progressBarCnpj);
            Controls.Add(dungeonLabel9);
            Controls.Add(cbCidade);
            Controls.Add(dungeonLabel8);
            Controls.Add(cbEstado);
            Controls.Add(txtComplemento);
            Controls.Add(dungeonLabel7);
            Controls.Add(txtNumero);
            Controls.Add(dungeonLabel6);
            Controls.Add(txtBairro);
            Controls.Add(dungeonLabel5);
            Controls.Add(txtRua);
            Controls.Add(dungeonLabel4);
            Controls.Add(dungeonLabel3);
            Controls.Add(txtDescricaoEmpresa);
            Controls.Add(dungeonLabel2);
            Controls.Add(txtNomeEmpresa);
            Controls.Add(metroDivider1);
            Controls.Add(dungeonHeaderLabel1);
            Controls.Add(dungeonLabel1);
            Controls.Add(btnRegistrar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Registro";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registro";
            Load += Registro_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.DungeonButtonLeft btnRegistrar;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel1;
        private ReaLTaiizor.Controls.DungeonTextBox txtNomeEmpresa;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel2;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel3;
        private ReaLTaiizor.Controls.DungeonTextBox txtRua;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel4;
        private ReaLTaiizor.Controls.DungeonTextBox txtBairro;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel5;
        private ReaLTaiizor.Controls.DungeonTextBox txtNumero;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel6;
        private ReaLTaiizor.Controls.DungeonTextBox txtComplemento;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel7;
        private ReaLTaiizor.Controls.DungeonComboBox cbEstado;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel8;
        private ReaLTaiizor.Controls.DungeonLabel dungeonLabel9;
        private ReaLTaiizor.Controls.DungeonComboBox cbCidade;
        private ReaLTaiizor.Controls.PoisonProgressSpinner progressBarCnpj;
        private ReaLTaiizor.Controls.DungeonTextBox txtDescricaoEmpresa;
        private ReaLTaiizor.Controls.DungeonTextBox txtCNPJ;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnLimpar;
        private ReaLTaiizor.Controls.MetroDivider metroDivider1;
        private ReaLTaiizor.Controls.DungeonHeaderLabel dungeonHeaderLabel1;
    }
}