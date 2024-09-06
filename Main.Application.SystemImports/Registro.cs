using Main.Application.SystemImports.Controller;
using Main.Application.SystemImports.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.Application.SystemImports
{
    public partial class Registro : Form
    {
        private readonly ServiceLocator _services;
        private readonly RegistroController _controller;
        private Dictionary<string, string> _estados;
        private Dictionary<string, string> _cidades;

        public Registro(ServiceLocator services)
        {
            InitializeComponent();
            this._services = services;
            this._controller = new RegistroController(this._services);
        }

        private async void Registro_Load(object sender, EventArgs e)
        {
            this._estados = await this._controller.GetEstados(this._estados);

            foreach (var estado in this._estados)
            {
                this.cbEstado.Items.Add(estado.Value);
            }
        }

        private async void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbCidade.Items.Clear();
            if (this._cidades != null)
                this._cidades.Clear();

            this._cidades = await this._controller.GetCidades(this._cidades, this._estados.First(x => x.Value.Equals(this.cbEstado.SelectedItem.ToString())).Key);

            this.cbCidade.Visible = true;
            this.cbCidade.Enabled = true;
            this.dungeonLabel9.Enabled = true;
            this.dungeonLabel9.Visible = true;

            foreach (var cidade in this._cidades)
            {
                this.cbCidade.Items.Add(cidade.Key);
            }
        }

        private async void txtCNPJ_TextChanged(object sender, EventArgs e)
        {
            string cleanCnpj = this._controller.ValidaCNPJ(this.txtCNPJ.Text);

            if (cleanCnpj.Count().Equals(14))
            {
                this.txtCNPJ.Enabled = false;
                this.txtBairro.Enabled = false;
                this.txtComplemento.Enabled = false;
                this.txtDescricaoEmpresa.Enabled = false;
                this.txtNomeEmpresa.Enabled = false;
                this.txtNumero.Enabled = false;
                this.txtRua.Enabled = false;
                this.cbCidade.Enabled = false;
                this.cbEstado.Enabled = false;

                this.progressBarCnpj.EnsureVisible = true;
                this.progressBarCnpj.Visible = true;
                this.progressBarCnpj.Enabled = true;
                this.progressBarCnpj.Invoke((Action)(() => this.progressBarCnpj.Value = 50));
                var tmpCnpj = await this._controller.GetCNPJ(cleanCnpj);

                this.txtNomeEmpresa.Text = tmpCnpj.RazaoSocial;

                if (tmpCnpj.Estabelecimento.AtividadePrincipal != null)
                    this.txtDescricaoEmpresa.Text = !string.IsNullOrEmpty(tmpCnpj.Estabelecimento.AtividadePrincipal.Descricao) ? tmpCnpj.Estabelecimento.AtividadePrincipal.Descricao : tmpCnpj.Estabelecimento.AtividadesSecundarias[0].Descricao;
                else
                    this.txtDescricaoEmpresa.Text = tmpCnpj.Estabelecimento.AtividadesSecundarias[0].Descricao;

                this.txtRua.Text = $"{tmpCnpj.Estabelecimento.TipoLogradouro} {tmpCnpj.Estabelecimento.Logradouro}";
                this.txtNumero.Text = tmpCnpj.Estabelecimento.Numero;
                this.txtBairro.Text = tmpCnpj.Estabelecimento.Bairro;
                this.txtComplemento.Text = tmpCnpj.Estabelecimento.Complemento;

                this.progressBarCnpj.EnsureVisible = false;
                this.progressBarCnpj.Visible = false;
                this.progressBarCnpj.Enabled = false;

                this.txtCNPJ.Enabled = true;
                this.txtBairro.Enabled = true;
                this.txtComplemento.Enabled = true;
                this.txtDescricaoEmpresa.Enabled = true;
                this.txtNomeEmpresa.Enabled = true;
                this.txtNumero.Enabled = true;
                this.txtRua.Enabled = true;
                this.cbCidade.Enabled = true;
                this.cbEstado.Enabled = true;
            }
        }

        private async void btnLimpar_Click(object sender, EventArgs e)
        {
            this.txtNomeEmpresa.Text = string.Empty;
            this.txtNumero.Text = string.Empty;
            this.txtCNPJ.Text = string.Empty;
            this.txtDescricaoEmpresa.Text = "O que sua empresa faz?";
            this.txtBairro.Text = string.Empty;
            this.txtRua.Text = string.Empty;
            this.txtComplemento.Text = "(Opcional)";

            this.cbEstado.Items.Clear();
            if (this._estados != null)
                this._estados.Clear();

            this._estados = await this._controller.GetEstados(this._estados);

            foreach (var estado in this._estados)
            {
                this.cbEstado.Items.Add(estado.Value);
            }

            this.cbCidade.Items.Clear();
            if (this._cidades != null)
                this._cidades.Clear();

            this.Refresh();
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            int enderecoId;
            int empresaId;

            bool validouDados = this._controller.ValidaDados(this.txtNomeEmpresa.Text, this.txtDescricaoEmpresa.Text, this._controller.ValidaCNPJ(this.txtCNPJ.Text), this.txtRua.Text, this.txtNumero.Text, this.txtComplemento.Text, this.txtBairro.Text, this.cbCidade.Items.Count > 0 ? this.cbCidade.SelectedItem.ToString() : string.Empty);
            if (validouDados)
            {
                enderecoId = await this._controller.InsereEndereco(this.txtRua.Text, this.txtNumero.Text, this.txtComplemento.Text, this.txtBairro.Text, this.cbCidade.SelectedItem.ToString());
                empresaId = await this._controller.InsereEmpresa(this.txtNomeEmpresa.Text, this.txtDescricaoEmpresa.Text, this._controller.ValidaCNPJ(this.txtCNPJ.Text), enderecoId);

                RegistroUsuario frmUsuario = new RegistroUsuario(this._services, empresaId, this.txtNomeEmpresa.Text);
                frmUsuario.ShowDialog();

                MessageBox.Show("Empresa inserida com sucesso!", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
