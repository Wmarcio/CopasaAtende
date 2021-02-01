using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRATReceive - Enviar serviço
    /// </summary>
    public class SCN4CRATReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// LoteServicos.
        /// </summary>
        [XmlElement("_SND-LOTE-SERVICOS")]
        public List<SCN4CRATReceiveServicos> loteServicos { get; set; }

        /// <summary>
        /// TipoInformacaoLote.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-LOTE")]
        public string tipoInformacaoLote { get; set; }

        /// <summary>
        /// DataEnvioLote.
        /// </summary>
        [XmlElement("_SND-DATA-ENVIO-LOTE")]
        public string dataEnvioLote { get; set; }

        /// <summary>
        /// HoraEnvioLote.
        /// </summary>
        [XmlElement("_SND-HORA-ENVIO-LOTE")]
        public string horaEnvioLote { get; set; }

        /// <summary>
        /// NumeroLote.
        /// </summary>
        [XmlElement("_SND-NUM-LOTE")]
        public string numeroLote { get; set; }

        /// <summary>
        /// QuantidadeOSEnviada.
        /// </summary>
        [XmlElement("_SND-QTDE-OS-ENV")]
        public string quantidadeOSEnviada { get; set; }

    }
}
