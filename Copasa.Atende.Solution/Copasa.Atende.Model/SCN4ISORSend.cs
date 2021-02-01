using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISORSend - Busca OS's de uma solicitação de serviço
    /// </summary>
    public class SCN4ISORSend : BaseModelSend
    {
        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_RCV-NRO-SOLIC-SERV")]
        public string numeroSolicitacaoServico { get; set; }
    }
}
