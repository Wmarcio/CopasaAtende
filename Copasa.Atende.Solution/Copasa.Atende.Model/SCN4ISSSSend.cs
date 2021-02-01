using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISSSSend - Busca solicitações de serviços de uma matrícula
    /// </summary>
    public class SCN4ISSSSend : BaseModelSend
    {
        /// <summary>
        /// MatriculaImovel.
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA-IMOVEL")]
        public string matriculaImovel { get; set; }

    }
}
