using Main.Application.SystemImports.Controller;
using Main.Application.SystemImports.Model;

namespace Main.Application.SystemImports
{
    public partial class RegistroUsuario : Form
    {
        private readonly ServiceLocator _services;
        private readonly RegistroController _controller;
        private readonly int _companyId;

        public RegistroUsuario(ServiceLocator services, int companyId, string companyName)
        {
            InitializeComponent();
            this.headerLabel.Text += $" [{companyName}]";
            this._companyId = companyId;
            this._services = services;
            this._controller = new RegistroController(this._services);
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            bool validouDados = this._controller.ValidaUsuario(this.txtEmail.Text, this.txtSenha.Text);

            if (validouDados)
            {
                // To Do : Inserir usuário
                MessageBox.Show("Aviso, este usuário será definido como administrador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await this._controller.InsereUsuario(this.txtEmail.Text, BaseConverter.Encrypt(this.txtSenha.Text, new byte[16], new byte[16]), this._companyId, 2);
                MessageBox.Show("Usuário inserido com sucesso!", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
