using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNReceive - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNReceive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro do Sicom
        /// </summary>
        [XmlElement("_COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetorno.
        /// </summary>
        [XmlElement("_DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_IDENT-USUARIO")]
        [JsonIgnore]
        public string identificador { get; set; }

        /// <summary>
        /// Nome.
        /// </summary>
        [XmlElement("_NOME-USUARIO")]
        [JsonIgnore]
        public string nome { get; set; }

        /// <summary>
        /// MatriculasSicom.
        /// </summary>
        [XmlElement("_TABELA-FAT")]
        [JsonIgnore]
        public SCN6ISCNReceiveMatriculas[] matriculasSicom { get; set; }

        /// <summary>
        /// MatriculasSicom.
        /// </summary>
        [JsonIgnore]
        public List<SCN6ISCNReceiveMatriculas> matriculas { get; set; }

        /// <summary>
        /// Lista de lançamentos do Sicom
        /// </summary>
        [XmlElement("_TABELA-LANC")]
        [JsonIgnore]
        public SCN6ISCNReceiveLancamento[] lancamentosSicom { get; set; }

        /// <summary>
        /// Lista de lançamentos formatada
        /// </summary>
        [JsonIgnore]
        public List<SCN6ISCNReceiveLancamento> lancamentos { get; set; }

        /// <summary>
        /// Lista de parcelamentos do Sicom
        /// </summary>
        [XmlElement("_TABELA-PARC")]
        [JsonIgnore]
        public SCN6ISCNReceiveParcelamento[] parcelamentoSicom { get; set; }

        /// <summary>
        /// Lista de parcelamentos formatada
        /// </summary>
        [JsonIgnore]
        public List<SCN6ISCNReceiveParcelamento> parcelamento { get; set; }

        /// <summary>
        /// Lista de financiamentos do Sicom
        /// </summary>
        [XmlElement("_TABELA-FINANC")]
        [JsonIgnore]
        public SCN6ISCNReceiveFinanciamento[] financiamentoSicom { get; set; }

        /// <summary>
        /// Lista de financiamentos formatada
        /// </summary>
        [JsonIgnore]
        public List<SCN6ISCNReceiveFinanciamento> financiamento { get; set; }

        /// <summary>
        /// Lista de débitos a vencer
        /// </summary>
        [JsonIgnore]
        public List<SCN6ISCNReceiveMatriculas> debitosVencer { get; set; }        
    }
}
