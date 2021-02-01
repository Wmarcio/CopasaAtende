using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFDReceiveFaturas  Lista faturas em débito
    /// </summary>
    public class SCN6ISFDReceiveFaturas : BaseModel
    {
        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_DT-REFER")]
        public string referencia { get; set; }

        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUM-FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// ValorFatura.
        /// </summary>
        [XmlElement("_VLR-TOT-FAT")]
        public string valorFatura { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        [XmlElement("_DT-VENCIMENTO")]
        public string dataVencimento { get; set; }

        /// <summary>
        /// Emitida.
        /// </summary>
        [XmlElement("_FLAG-EMISSAO")]
        public string emitida { get; set; }

        /// <summary>
        /// TemCF20.
        /// </summary>
        [XmlElement("_FLAG-TEM-CF20")]
        public string temCF20 { get; set; }

        /// <summary>
        /// TemCF20.
        /// </summary>
        [XmlElement("_DESCRICAO-TIPO-FATURA")]
        public string descricaoFatura { get; set; }

        /// <summary>
        /// NumeroCodigoBarrasFormatado.
        /// </summary>
        public string numeroCodigoBarrasFormatado { get; set; }

        /// <summary>
        /// numeroCodigoBarras.
        /// </summary>
        public string numeroCodigoBarras { get; set; }
    }
}
