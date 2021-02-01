using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceiveMedicaoEsgoto - Lista Pontos serviço - Medição esgoto
    /// </summary>
    public class SCN3ISPSReceiveMedicaoEsgoto : BaseModel
    {
        /// <summary>
        /// Tipo.
        /// </summary>
        [XmlElement("_TIPO-PS-MEDICAO")]
        public string tipo { get; set; }

        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_NUM-PS-MEDICAO")]
        public string numero { get; set; }

        /// <summary>
        /// DataInicioSituacaoPS.
        /// </summary>
        [XmlElement("_DATA-SITUACAO-PS-MEDICAO")]
        public string dataInicioSituacaoPS { get; set; }

        /// <summary>
        /// DescricaoSituacaoPS.
        /// </summary>
        [XmlElement("_DESC-SITUACAO-PS-MEDICAO")]
        public string descricaoSituacaoPS { get; set; }

        /// <summary>
        /// DescricaoProdutoPS.
        /// </summary>
        [XmlElement("_DESC-PRODUTO-PS-MEDICAO")]
        public string DescricaoProdutoPS { get; set; }

    }
}
