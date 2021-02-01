using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Digital
{

    /// <summary>
    /// Model representativo da tabela APP_CLIENTE.
    /// </summary>
    /// <summary>
    /// Model representativo da tabela APP_CLIENTE.
    /// </summary>
    [Serializable()]
    public class ClienteModel : BaseModel
    {

        /// <summary>
        /// Recupera e grava o Id.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Recupera e grava a Descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Recupera e grava IsAtivo.
        /// </summary>
        public bool IsAtivo { get; set; }

        /// <summary>
        /// CPF ou CNPJ do Cliente.
        /// </summary>
        public long CpfCnpj { get; set; }

        /// <summary>
        /// Nome do Cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do Cliente.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Flag Termo de Aceite.
        /// </summary>
        public bool FlagTermoAceite { get; set; }

        /// <summary>
        /// Flag Política de Privacidade.
        /// </summary>
        public bool FlagPoliticaPrivacidade { get; set; }

        /// <summary>
        /// Imagem do Perfil Cliente na Base64.
        /// </summary>
        public string ImagemPerfilBase64 { get; set; }

        /// <summary>
        /// Imagem do Perfil Cliente.
        /// </summary>
        public byte[] ImagemPerfil { get; set; }

        /// <summary>
        /// Representante Comercial.
        /// </summary>
        public string RepresentanteComercial { get; set; }

        /// <summary>
        /// Lista de Telefones.
        /// </summary>
        public virtual List<TelefoneModel> Telefones { get; set; }

        /// <summary>
        /// Lista de Identificadores
        /// </summary>
        public virtual List<ClienteIdentificadorModel> Identificadores { get; set; }

        /// <summary>
        /// Tipo de Cliente.
        /// </summary>
        public string TipoCliente { get; set; }

        /// <summary>
        /// Data da alteração.
        /// </summary>
        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// Status do e-mail do usuário
        /// </summary>
        public string StatusEmail { get; set; }

        /// <summary>
        /// Status do e-mail do usuário
        /// </summary>
        public string Validado { get; set; }
    }


    /// <summary>
    /// Model representativo da tabela APP_CLIENTE_IDENTIFICADOR.
    /// </summary>
    [Serializable()]
    public class ClienteIdentificadorModel : BaseModel
    {
        /// <summary>
        /// Recupera e grava o Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Recupera e grava a Descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Recupera e grava IsAtivo.
        /// </summary>
        public bool IsAtivo { get; set; }

        /// <summary>
        /// Identificador do Cliente.
        /// </summary>
        public int Identificador { get; set; }

        /// <summary>
        /// CPF ou CNPJ.
        /// </summary>
        public long CpfCnpj { get; set; }

        /// <summary>
        /// Ativo.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        public long CodigoLocalidade { get; set; }

        /// <summary>
        /// CPF ou CNPJ do Cliente Copasa
        /// </summary>
        public long CpfCnpjClienteCopasa { get; set; }

        /// <summary>
        /// Nome do Cliente.
        /// </summary>
        public string NomeClienteCopasa { get; set; }

        /// <summary>
        /// Cliente.
        /// </summary>
        public virtual ClienteModel Cliente { get; set; }

        /// <summary>
        /// Endereço.
        /// </summary>
        public virtual EnderecoModel Endereco { get; set; }
    }

    /// <summary>
    /// Classe EnderecoModel.
    /// </summary>
    public class EnderecoModel : BaseModel
    {
        /// <summary>
        /// Tipo de Logradouro.
        /// </summary>        
        public string TipoLogradouro { get; set; }

        /// <summary>
        /// Logradouro.
        /// </summary>        
        public string Logradouro { get; set; }

        /// <summary>
        /// Numero do Logradouro.
        /// </summary>        
        public long NumeroLogradouro { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>        
        public string Bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>        
        public string Localidade { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>        
        public long CEP { get; set; }

        /// <summary>
        /// UF.
        /// </summary>        
        public string UF { get; set; }

    }

    /// <summary>
    /// Model representativo da tabela APP_TELEFONE.
    /// </summary>
    [Serializable()]
    public class TelefoneModel : BaseModel
    {

        /// <summary>
        /// Recupera e grava o Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Recupera e grava a Descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Recupera e grava IsAtivo.
        /// </summary>
        public bool IsAtivo { get; set; }

        /// <summary>
        /// IdTipoTelefone.
        /// </summary>
        public TipoTelefoneEnum IdTipoTelefone { get; set; }

        /// <summary>
        /// CPF ou CNPJ do Cliente.
        /// </summary>
        public long CpfCnpj { get; set; }

        /// <summary>
        /// Cliente.
        /// </summary>
        public virtual ClienteModel Cliente { get; set; }

        /// <summary>
        /// Telefone.
        /// </summary>
        public long NumeroTelefone { get; set; }

        /// <summary>
        /// TiposTelefone.
        /// </summary>
        public virtual TipoTelefoneModel TiposTelefone { get; set; }

        /// <summary>
        /// Id comunciacao.
        /// </summary>
        public long IdComunicacao { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class TipoTelefoneModel : BaseModel
    {
        /// <summary>
        /// ID do Tipo de Telefone.
        /// </summary>
        public TipoTelefoneEnum IdTipoTelefone { get; set; }

        /// <summary>
        /// Descricao do Tipo de Telefone.
        /// </summary>
        public string DescricaoTipoTelefone { get; set; }

        /// <summary>
        /// Lista de Tipos de Telefones.
        /// </summary>
        public virtual List<TelefoneModel> Telefones { get; set; }
    }

    /// <summary>
    /// Enumerador para Tipo de Telefone.
    /// </summary>
    public enum TipoTelefoneEnum
    {
        /// <summary>
        /// Celular.
        /// </summary>
        [Description("Celular.")]
        CELULAR = 1,
        /// <summary>
        /// Comercial.
        /// </summary>
        [Description("Comercial.")]
        COMERCIAL = 2,
        /// <summary>
        /// Residencial.
        /// </summary>
        [Description("Residencial.")]
        RESIDENCIAL = 3
    }
}
