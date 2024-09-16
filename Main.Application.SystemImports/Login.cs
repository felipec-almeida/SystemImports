using Main.Application.SystemImports.Controller;
using Main.Application.SystemImports.Model;

namespace Main.Application.SystemImports
{
    public partial class Login : Form
    {

        private readonly APIClient _client;
        private readonly ServiceLocator _services;
        private readonly LoginController _controller;
        private Dictionary<int, string> _empresas;


        public Login()
        {
            InitializeComponent();
            this._client = new APIClient();
            this._services = new ServiceLocator(this._client);
            this._controller = new LoginController(this._services);
        }

        private void btnLoginUser(object sender, EventArgs e)
        {
            int empresaId = this._empresas.FirstOrDefault(x => x.Value.ToUpper().Equals(this.cbEmpresa.SelectedItem?.ToString()), new KeyValuePair<int, string>(-1, "Default")).Key;
            this._controller.ValidateLogin(this.txtEmail.Text, this.txtSenha.Text, empresaId);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this._empresas = await this._controller.GetEmpresas(this._empresas);

            foreach (var empresa in this._empresas)
            {
                this.cbEmpresa.Items.Add(empresa.Value.ToUpper());
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registro formRegistro = new Registro(this._services);
            formRegistro.ShowDialog();
            this.Refresh();
            this.Form1_Load(sender, e);
        }

        private async void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int empresaId = this._empresas.FirstOrDefault(x => x.Value.ToUpper().Equals(this.cbEmpresa.SelectedItem?.ToString()), new KeyValuePair<int, string>(-1, "Default")).Key;

            Empresa empresaResult = await this._controller.GetEmpresa(empresaId);

            if (!string.IsNullOrEmpty(empresaResult.ImagemLogo))
            {
                this.progressBarImage.EnsureVisible = true;
                this.progressBarImage.Visible = true;
                this.progressBarImage.Enabled = true;
                this.progressBarImage.Invoke((Action)(() => this.progressBarImage.Value = 50));

                await Task.Run(() =>
                {
                    this.picLogoEmpresa = this._controller.UpdateLogoImage(this.picLogoEmpresa, empresaResult.ImagemLogo);
                });

                this.progressBarImage.Invoke((Action)(() => this.progressBarImage.Value = 100));

                this.progressBarImage.EnsureVisible = false;
                this.progressBarImage.Visible = false;
                this.progressBarImage.Enabled = false;

                this.picLogoEmpresa.Enabled = true;
                this.picLogoEmpresa.Visible = true;
                this.picLogoEmpresa.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
