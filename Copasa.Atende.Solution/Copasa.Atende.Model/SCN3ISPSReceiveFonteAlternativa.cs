using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceiveFonteAlternativa - Lista Pontos serviço - Fonte alternativa
    /// </summary>
    public class SCN3ISPSReceiveFonteAlternativa : BaseModel
    {
        /// <summary>
        /// Tipo.
        /// </summary>
        [XmlElement("_TIPO-PS-FONTE-ALTERNATIVA")]
        public string tipo { get; set; }

        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_NUM-PS-FONTE")]
        public string numero { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        [XmlElement("_DESC-SITUACAO-PS-FONTE")]
        public string descricaoSituacao { get; set; }

        /// <summary>
        /// Descricao.
        /// </summary>
        [XmlElement("_TIPO-PS-FONTE")]
        public string descricao { get; set; }

        /// <summary>
        /// DescricaoProdutoEsgotado.
        /// </summary>
        [XmlElement("_DESC-PRODUTO-ESGOTADO-FONTE")]
        public string descricaoProdutoEsgotado { get; set; }

    }
}
