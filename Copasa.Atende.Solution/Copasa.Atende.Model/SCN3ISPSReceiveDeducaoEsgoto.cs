using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceiveDeducaoEsgoto - Lista Pontos serviço - Dedução esgoto
    /// </summary>
    public class SCN3ISPSReceiveDeducaoEsgoto : BaseModel
    {
        /// <summary>
        /// Tipo.
        /// </summary>
        [XmlElement("_TIPO-PS-DEDUCAO")]
        public string tipo { get; set; }

        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_NUM-PS-DEDUCAO")]
        public string numero { get; set; }

        /// <summary>
        /// DataInicioSituacaoPS.
        /// </summary>
        [XmlElement("_DATA-SITUACAO-PS-DEDUCAO")]
        public string dataInicioSituacaoPS { get; set; }

        /// <summary>
        /// DescricaoSituacaoPS.
        /// </summary>
        [XmlElement("_DESC-SITUACAO-PS-DEDUCAO")]
        public string descricaoSituacaoPS { get; set; }

        /// <summary>
        /// DescricaoProdutoPS.
        /// </summary>
        [XmlElement("_DESC-PRODUTO-PS-DEDUCAO")]
        public string descricaoProdutoPS { get; set; }

    }
}
