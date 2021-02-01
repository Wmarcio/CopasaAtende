using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1ReceiveMatriculas Busca dados do cliente
    /// </summary>
    public class SCN4ISU1ReceiveMatriculasUsuarios : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA-O")]
        public string matricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_IDENT-USUARIO-MAT-O")]
        public string identificador { get; set; }

        /// <summary>
        /// Nome.
        /// </summary>
        [XmlElement("_NOME-CLIENTE-MAT-O")]
        public string nome { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [XmlElement("_CRM-EMAIL-MAT-O")]
        public string email { get; set; }

        /// <summary>
        /// DDDResidencia.
        /// </summary>
        [XmlElement("_CRM-DDD-RES-MAT-O")]
        [JsonIgnore]
        public string DDDResidencia { get; set; }

        /// <summary>
        /// TelefoneResidenciaSicom.
        /// </summary>
        [XmlElement("_CRM-FONE-RES-MAT-O")]
        [JsonIgnore]
        public string telefoneResidenciaSicom { get; set; }

        /// <summary>
        /// DDDComercial.
        /// </summary>
        [XmlElement("_CRM-DDD-COM-MAT-O")]
        [JsonIgnore]
        public string DDDComercial { get; set; }

        /// <summary>
        /// TelefoneComercialSicom.
        /// </summary>
        [XmlElement("_CRM-FONE-COM-MAT-O")]
        [JsonIgnore]
        public string telefoneComercialSicom { get; set; }

        /// <summary>
        /// DDDRecado.
        /// </summary>
        [XmlElement("_CRM-DDD-RECADO-MAT-O")]
        [JsonIgnore]
        public string DDDRecado { get; set; }

        /// <summary>
        /// TelefoneRecadoSicom.
        /// </summary>
        [XmlElement("_CRM-FONE-RECADO-MAT-O")]
        [JsonIgnore]
        public string telefoneRecadoSicom { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_CRM-COD-LOGR-MAT-O")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_CRM-TIPO-LOGR-MAT-O")]
        [JsonIgnore]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [XmlElement("_CRM-NOME-LOGR-MAT-O")]
        [JsonIgnore]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouro.
        /// </summary>
        [XmlElement("_CRM-NUM-IMOVEL-MAT-O")]        
        public string numeroLogradouro { get; set; }

        /// <summary>
        /// TipoComplementoLogradouro.
        /// </summary>
        [XmlElement("_CRM-COMPL-TIPO-MAT-O")]
        public string tipoComplementoLogradouro { get; set; }

        /// <summary>
        /// ComplementoLogradouro.
        /// </summary>
        [XmlElement("_CRM-COMPL-INF-MAT-O")]        
        public string complementoLogradouro { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_CRM-BAIRRO-MAT-O")]
        public string bairro { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_CRM-COD-BAIRRO-MAT-O")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [XmlElement("_CRM-NOME-LOCAL-MAT-O")]
        public string Localidade { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_COD-LOCALIDADE-MAT-O")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>
        [XmlElement("_CEP-MAT-O")]
        public string CEP { get; set; }

        /// <summary>
        /// FaturaEntreguePorEmail.
        /// </summary>
        [XmlElement("_FLAG-CONTA-EMAIL-MAT-O")]
        public string faturaEntreguePorEmail { get; set; }

        /// <summary>
        /// SituacaoAgua.
        /// </summary>
        [XmlElement("_SITUACAO-AGUA-MAT-O")]
        [JsonIgnore]
        public string situacaoAgua { get; set; }

        /// <summary>
        /// ProdutoAgua.
        /// </summary>
        [XmlElement("_PROD-AGUA-MAT-O")]
        [JsonIgnore]
        public string produtoAgua { get; set; }

        /// <summary>
        /// SituacaoEsgoto.
        /// </summary>
        [XmlElement("_SITUACAO-ESGOTO-MAT-O")]
        [JsonIgnore]
        public string situacaoEsgoto { get; set; }

        /// <summary>
        /// Situacao.
        /// </summary>
        public string situacao { get; set; }

        /// <summary>
        /// ProdutoEsgoto.
        /// </summary>
        [XmlElement("_PROD-ESGOTO-MAT-O")]
        [JsonIgnore]
        public string produtoEsgoto { get; set; }

        /// <summary>
        /// NumeroEconomiasResidenciais.
        /// </summary>
        [XmlElement("_ECONOMIA-RES-MAT-O")]
        public string numeroEconomiasResidenciais { get; set; }

        /// <summary>
        /// NumeroEconomiasComerciais.
        /// </summary>
        [XmlElement("_ECONOMIA-COM-MAT-O")]
        public string numeroEconomiasComerciais { get; set; }

        /// <summary>
        /// NumeroEconomiasIndustriais.
        /// </summary>
        [XmlElement("_ECONOMIA-IND-MAT-O")]
        public string numeroEconomiasIndustriais { get; set; }

        /// <summary>
        /// NumeroEconomiasPublicas.
        /// </summary>
        [XmlElement("_ECONOMIA-PUB-MAT-O")]
        public string numeroEconomiasPublicas { get; set; }

        /// <summary>
        /// NumeroEconomiasSociais.
        /// </summary>
        [XmlElement("_ECONOMIA-SOC-MAT-O")]
        public string numeroEconomiasSociais { get; set; }

        /// <summary>
        /// DataInicioVigencia.
        /// </summary>
        [XmlElement("_DT-INI-VIGENCIA-MAT-O")]
        public string dataInicioVigencia { get; set; }

        /// <summary>
        /// DataFimVigencia.
        /// </summary>
        [XmlElement("_DT-FIM-VIGENCIA-MAT-O")]
        [JsonIgnore]
        public string dataFimVigencia { get; set; }

        /// <summary>
        /// DataVigencia.
        /// </summary>
        public string vigencia { get; set; }

        /// <summary>
        /// DataInicioVigenciaOrdenacao(usado apenas para ordenação.
        /// </summary>
        [JsonIgnore]
        public DateTime dataInicioVigenciaOrdenacao { get; set; }

        /// <summary>
        /// ValorDebito.
        /// </summary>
        public string valorDebito { get; set; }

        /// <summary>
        /// MensagemRetorno.
        /// </summary>
        public string mensagemRetorno { get; set; }
    }
}
