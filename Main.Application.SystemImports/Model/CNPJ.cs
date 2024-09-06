using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Main.Application.SystemImports.Model
{
    public class CNPJ
    {
        private string _cnpj_raiz;
        private string _razao_social;
        private string _capital_social;
        private string _responsavel_federativo;
        private DateTime _atualizado_em;
        private Porte _porte;
        private NaturezaJuridica _natureza_juridica;
        private QualificacaoDoResponsavel _qualificacao_do_responsavel;
        private List<Socio> _socios;
        private Simples _simples;
        private Estabelecimento _estabelecimento;

        [JsonPropertyName("cnpj_raiz")]
        public string CnpjRaiz { get { return this._cnpj_raiz; } set { this._cnpj_raiz = value; } }

        [JsonPropertyName("razao_social")]
        public string RazaoSocial { get { return this._razao_social; } set { this._razao_social = value; } }

        [JsonPropertyName("capital_social")]
        public string CapitalSocial { get { return this._capital_social; } set { this._capital_social = value; } }

        [JsonPropertyName("responsavel_federativo")]
        public string ResponsavelFederativo { get { return this._responsavel_federativo; } set { this._responsavel_federativo = value; } }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get { return this._atualizado_em; } set { this._atualizado_em = value; } }

        [JsonPropertyName("porte")]
        public Porte Porte { get { return this._porte; } set { this._porte = value; } }

        [JsonPropertyName("natureza_juridica")]
        public NaturezaJuridica NaturezaJuridica { get { return this._natureza_juridica; } set { this._natureza_juridica = value; } }

        [JsonPropertyName("qualificacao_do_responsavel")]
        public QualificacaoDoResponsavel QualificacaoDoResponsavel { get { return this._qualificacao_do_responsavel; } set { this._qualificacao_do_responsavel = value; } }

        [JsonPropertyName("socios")]
        public List<Socio> Socios { get { return this._socios; } set { this._socios = value; } }

        [JsonPropertyName("simples")]
        public Simples Simples { get { return this._simples; } set { this._simples = value; } }

        [JsonPropertyName("estabelecimento")]
        public Estabelecimento Estabelecimento { get { return this._estabelecimento; } set { this._estabelecimento = value; } }

        public CNPJ() { }

        public CNPJ(string cnpj_raiz, string razao_social, string capital_social, string responsavel_federativo, DateTime atualizado_em, Porte porte, NaturezaJuridica natureza_juridica, QualificacaoDoResponsavel qualificacao_do_responsavel, List<Socio> socios, Simples simples, Estabelecimento estabelecimento)
        {
            CnpjRaiz = cnpj_raiz;
            RazaoSocial = razao_social;
            CapitalSocial = capital_social;
            ResponsavelFederativo = responsavel_federativo;
            AtualizadoEm = atualizado_em;
            Porte = porte;
            NaturezaJuridica = natureza_juridica;
            QualificacaoDoResponsavel = qualificacao_do_responsavel;
            Socios = socios;
            Simples = simples;
            Estabelecimento = estabelecimento;
        }
    }

    public class Porte
    {
        private string _id;
        private string _descricao;

        [JsonPropertyName("id")]
        public string Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class NaturezaJuridica
    {
        private string _id;
        private string _descricao;

        [JsonPropertyName("id")]
        public string Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class QualificacaoDoResponsavel
    {
        private int _id;
        private string _descricao;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class Socio
    {
        private string _cpf_cnpj_socio;
        private string _nome;
        private string _tipo;
        private DateTime _data_entrada;
        private string _cpf_representante_legal;
        private string _nome_representante;
        private string _faixa_etaria;
        private DateTime _atualizado_em;
        private string _pais_id;
        private QualificacaoSocio _qualificacao_socio;
        private string _qualificacao_representante;
        private Pais _pais;

        [JsonPropertyName("cpf_cnpj_socio")]
        public string CpfCnpjSocio { get { return this._cpf_cnpj_socio; } set { this._cpf_cnpj_socio = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("tipo")]
        public string Tipo { get { return this._tipo; } set { this._tipo = value; } }

        [JsonPropertyName("data_entrada")]
        public DateTime DataEntrada { get { return this._data_entrada; } set { this._data_entrada = value; } }

        [JsonPropertyName("cpf_representante_legal")]
        public string CpfRepresentanteLegal { get { return this._cpf_representante_legal; } set { this._cpf_representante_legal = value; } }

        [JsonPropertyName("nome_representante")]
        public string NomeRepresentante { get { return this._nome_representante; } set { this._nome_representante = value; } }

        [JsonPropertyName("faixa_etaria")]
        public string FaixaEtaria { get { return this._faixa_etaria; } set { this._faixa_etaria = value; } }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get { return this._atualizado_em; } set { this._atualizado_em = value; } }

        [JsonPropertyName("pais_id")]
        public string PaisId { get { return this._pais_id; } set { this._pais_id = value; } }

        [JsonPropertyName("qualificacao_socio")]
        public QualificacaoSocio QualificacaoSocio { get { return this._qualificacao_socio; } set { this._qualificacao_socio = value; } }

        [JsonPropertyName("qualificacao_representante")]
        public string QualificacaoRepresentante { get { return this._qualificacao_representante; } set { this._qualificacao_representante = value; } }

        [JsonPropertyName("pais")]
        public Pais Pais { get { return this._pais; } set { this._pais = value; } }
    }

    public class QualificacaoSocio
    {
        private int _id;
        private string _descricao;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class Simples
    {
        private string _simples;
        private DateTime _data_opcao_simples;
        private DateTime _data_exclusao_simples;
        private string _mei;
        private DateTime? _data_opcao_mei;
        private DateTime? _data_exclusao_mei;
        private DateTime _atualizado_em;

        [JsonPropertyName("simples")]
        public string SimplesIndicator { get { return this._simples; } set { this._simples = value; } }

        [JsonPropertyName("data_opcao_simples")]
        public DateTime DataOpcaoSimples { get { return this._data_opcao_simples; } set { this._data_opcao_simples = value; } }

        [JsonPropertyName("data_exclusao_simples")]
        public DateTime DataExclusaoSimples { get { return this._data_exclusao_simples; } set { this._data_exclusao_simples = value; } }

        [JsonPropertyName("mei")]
        public string MeiIndicator { get { return this._mei; } set { this._mei = value; } }

        [JsonPropertyName("data_opcao_mei")]
        public DateTime? DataOpcaoMei { get { return this._data_opcao_mei; } set { this._data_opcao_mei = value; } }

        [JsonPropertyName("data_exclusao_mei")]
        public DateTime? DataExclusaoMei { get { return this._data_exclusao_mei; } set { this._data_exclusao_mei = value; } }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get { return this._atualizado_em; } set { this._atualizado_em = value; } }
    }

    public class Estabelecimento
    {
        private string _cnpj;
        private List<AtividadeSecundaria> _atividades_secundarias;
        private string _cnpj_raiz;
        private string _cnpj_ordem;
        private string _cnpj_digito_verificador;
        private string _tipo;
        private string _nome_fantasia;
        private string _situacao_cadastral;
        private DateTime _data_situacao_cadastral;
        private DateTime _data_inicio_atividade;
        private string _nome_cidade_exterior;
        private string _tipo_logradouro;
        private string _logradouro;
        private string _numero;
        private string _complemento;
        private string _bairro;
        private string _cep;
        private string _ddd1;
        private string _telefone1;
        private string _ddd2;
        private string _telefone2;
        private string _ddd_fax;
        private string _fax;
        private string _email;
        private string _situacao_especial;
        private DateTime? _data_situacao_especial;
        private DateTime _atualizado_em;
        private AtividadePrincipal _atividade_principal;
        private Pais _pais;
        private Estado _estado;
        private Cidade _cidade;
        private List<InscricaoEstadual> _inscricoes_estaduais;

        [JsonPropertyName("cnpj")]
        public string Cnpj { get { return this._cnpj; } set { this._cnpj = value; } }

        [JsonPropertyName("atividades_secundarias")]
        public List<AtividadeSecundaria> AtividadesSecundarias { get { return this._atividades_secundarias; } set { this._atividades_secundarias = value; } }

        [JsonPropertyName("cnpj_raiz")]
        public string CnpjRaiz { get { return this._cnpj_raiz; } set { this._cnpj_raiz = value; } }

        [JsonPropertyName("cnpj_ordem")]
        public string CnpjOrdem { get { return this._cnpj_ordem; } set { this._cnpj_ordem = value; } }

        [JsonPropertyName("cnpj_digito_verificador")]
        public string CnpjDigitoVerificador { get { return this._cnpj_digito_verificador; } set { this._cnpj_digito_verificador = value; } }

        [JsonPropertyName("tipo")]
        public string Tipo { get { return this._tipo; } set { this._tipo = value; } }

        [JsonPropertyName("nome_fantasia")]
        public string NomeFantasia { get { return this._nome_fantasia; } set { this._nome_fantasia = value; } }

        [JsonPropertyName("situacao_cadastral")]
        public string SituacaoCadastral { get { return this._situacao_cadastral; } set { this._situacao_cadastral = value; } }

        [JsonPropertyName("data_situacao_cadastral")]
        public DateTime DataSituacaoCadastral { get { return this._data_situacao_cadastral; } set { this._data_situacao_cadastral = value; } }

        [JsonPropertyName("data_inicio_atividade")]
        public DateTime DataInicioAtividade { get { return this._data_inicio_atividade; } set { this._data_inicio_atividade = value; } }

        [JsonPropertyName("nome_cidade_exterior")]
        public string NomeCidadeExterior { get { return this._nome_cidade_exterior; } set { this._nome_cidade_exterior = value; } }

        [JsonPropertyName("tipo_logradouro")]
        public string TipoLogradouro { get { return this._tipo_logradouro; } set { this._tipo_logradouro = value; } }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get { return this._logradouro; } set { this._logradouro = value; } }

        [JsonPropertyName("numero")]
        public string Numero { get { return this._numero; } set { this._numero = value; } }

        [JsonPropertyName("complemento")]
        public string Complemento { get { return this._complemento; } set { this._complemento = value; } }

        [JsonPropertyName("bairro")]
        public string Bairro { get { return this._bairro; } set { this._bairro = value; } }

        [JsonPropertyName("cep")]
        public string Cep { get { return this._cep; } set { this._cep = value; } }

        [JsonPropertyName("ddd1")]
        public string Ddd1 { get { return this._ddd1; } set { this._ddd1 = value; } }

        [JsonPropertyName("telefone1")]
        public string Telefone1 { get { return this._telefone1; } set { this._telefone1 = value; } }

        [JsonPropertyName("ddd2")]
        public string Ddd2 { get { return this._ddd2; } set { this._ddd2 = value; } }

        [JsonPropertyName("telefone2")]
        public string Telefone2 { get { return this._telefone2; } set { this._telefone2 = value; } }

        [JsonPropertyName("ddd_fax")]
        public string DddFax { get { return this._ddd_fax; } set { this._ddd_fax = value; } }

        [JsonPropertyName("fax")]
        public string Fax { get { return this._fax; } set { this._fax = value; } }

        [JsonPropertyName("email")]
        public string Email { get { return this._email; } set { this._email = value; } }

        [JsonPropertyName("situacao_especial")]
        public string SituacaoEspecial { get { return this._situacao_especial; } set { this._situacao_especial = value; } }

        [JsonPropertyName("data_situacao_especial")]
        public DateTime? DataSituacaoEspecial { get { return this._data_situacao_especial; } set { this._data_situacao_especial = value; } }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get { return this._atualizado_em; } set { this._atualizado_em = value; } }

        [JsonPropertyName("atividade_principal")]
        public AtividadePrincipal AtividadePrincipal { get { return this._atividade_principal; } set { this._atividade_principal = value; } }

        [JsonPropertyName("pais")]
        public Pais Pais { get { return this._pais; } set { this._pais = value; } }

        [JsonPropertyName("estado")]
        public Estado Estado { get { return this._estado; } set { this._estado = value; } }

        [JsonPropertyName("cidade")]
        public Cidade Cidade { get { return this._cidade; } set { this._cidade = value; } }

        [JsonPropertyName("inscricoes_estaduais")]
        public List<InscricaoEstadual> InscricoesEstaduais { get { return this._inscricoes_estaduais; } set { this._inscricoes_estaduais = value; } }
    }

    public class AtividadeSecundaria
    {
        private string _id;
        private string _secao;
        private string _divisao;
        private string _grupo;
        private string _classe;
        private string _subclasse;
        private string _descricao;

        [JsonPropertyName("id")]
        public string Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("secao")]
        public string Secao { get { return this._secao; } set { this._secao = value; } }

        [JsonPropertyName("divisao")]
        public string Divisao { get { return this._divisao; } set { this._divisao = value; } }

        [JsonPropertyName("grupo")]
        public string Grupo { get { return this._grupo; } set { this._grupo = value; } }

        [JsonPropertyName("classe")]
        public string Classe { get { return this._classe; } set { this._classe = value; } }

        [JsonPropertyName("subclasse")]
        public string Subclasse { get { return this._subclasse; } set { this._subclasse = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class AtividadePrincipal
    {
        private string _id;
        private string _secao;
        private string _divisao;
        private string _grupo;
        private string _classe;
        private string _subclasse;
        private string _descricao;

        [JsonPropertyName("id")]
        public string Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("secao")]
        public string Secao { get { return this._secao; } set { this._secao = value; } }

        [JsonPropertyName("divisao")]
        public string Divisao { get { return this._divisao; } set { this._divisao = value; } }

        [JsonPropertyName("grupo")]
        public string Grupo { get { return this._grupo; } set { this._grupo = value; } }

        [JsonPropertyName("classe")]
        public string Classe { get { return this._classe; } set { this._classe = value; } }

        [JsonPropertyName("subclasse")]
        public string Subclasse { get { return this._subclasse; } set { this._subclasse = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
    }

    public class Pais
    {
        private string _id;
        private string _iso2;
        private string _iso3;
        private string _nome;
        private string _comex_id;

        [JsonPropertyName("id")]
        public string Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("iso2")]
        public string Iso2 { get { return this._iso2; } set { this._iso2 = value; } }

        [JsonPropertyName("iso3")]
        public string Iso3 { get { return this._iso3; } set { this._iso3 = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("comex_id")]
        public string ComexId { get { return this._comex_id; } set { this._comex_id = value; } }
    }

    public class Estado
    {
        private int _id;
        private string _nome;
        private string _sigla;
        private int _ibge_id;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("sigla")]
        public string Sigla { get { return this._sigla; } set { this._sigla = value; } }

        [JsonPropertyName("ibge_id")]
        public int IbgeId { get { return this._ibge_id; } set { this._ibge_id = value; } }
    }

    public class Cidade
    {
        private int _id;
        private string _nome;
        private int _ibge_id;
        private string _siafi_id;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("ibge_id")]
        public int IbgeId { get { return this._ibge_id; } set { this._ibge_id = value; } }

        [JsonPropertyName("siafi_id")]
        public string SiafiId { get { return this._siafi_id; } set { this._siafi_id = value; } }
    }

    public class InscricaoEstadual
    {
        private string _inscricao_estadual;
        private bool _ativo;
        private DateTime _atualizado_em;
        private Estado _estado;

        [JsonPropertyName("inscricao_estadual")]
        public string InscricaoEstaduaL2 { get { return this._inscricao_estadual; } set { this._inscricao_estadual = value; } }

        [JsonPropertyName("ativo")]
        public bool Ativo { get { return this._ativo; } set { this._ativo = value; } }

        [JsonPropertyName("atualizado_em")]
        public DateTime AtualizadoEm { get { return this._atualizado_em; } set { this._atualizado_em = value; } }

        [JsonPropertyName("estado")]
        public Estado Estado { get { return this._estado; } set { this._estado = value; } }
    }
}
