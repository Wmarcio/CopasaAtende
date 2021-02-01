using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS03Receive Informar leitura - Envia leitura CF20
    /// </summary>
    public class SCN5IS03Receive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-MSG")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// NomeCliente.
        /// </summary>
        [XmlElement("_NOME-CLIENTE")]
        public string nomeCliente { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_TIPO-LOGRADOURO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NumImovel.
        /// </summary>
        [XmlElement("_NUM-IMOVEL")]
        public string numImovel { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        [XmlElement("_TIPO-COMPLEMENTO")]
        public string tipoComplemento { get; set; }

        /// <summary>
        /// InfComplemento.
        /// </summary>
        [XmlElement("_INF-COMPLEMENTO")]
        public string infComplemento { get; set; }

        /// <summary>
        /// NomeBairro.
        /// </summary>
        [XmlElement("_NOME-BAIRRO")]
        public string NomeBairro { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        [XmlElement("_NOME-LOCALIDADE")]
        public string nomeLocalidade { get; set; }

        /// <summary>
        /// ValorServ1.
        /// </summary>
        [XmlElement("_VALOR-SERV-1")]
        public string valorServ1 { get; set; }

        /// <summary>
        /// DataPExec.
        /// </summary>
        [XmlElement("_DATA-P-EXEC")]
        public string dataPExec { get; set; }

        /// <summary>
        /// DebitoAutomatico.
        /// </summary>
        [XmlElement("_DEBITO-AUTOMATICO")]
        public string debitoAutomatico { get; set; }
    }
}
