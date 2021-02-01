using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISAVReceive Altera vencimento fatura
    /// </summary>
    public class SCN6ISAVReceive : BaseModelReceive
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
        /// DiaVencimento.
        /// </summary>
        [XmlElement("_SND-DIA-VENCTO-FAT-ATUAL")]
        public string diaVencimento { get; set; }

        /// <summary>
        /// DiasInvalidos.
        /// </summary>
        [XmlElement("_SND-DIAS-INVALIDOS-FAT")]
        public string[] diasInvalidos { get; set; }

    }
}
