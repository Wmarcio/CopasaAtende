using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISSSReceive - Busca solicitações de serviços de uma matrícula
    /// </summary>
    public class SCN4ISSSReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// SolicitacoesServicoSicom.
        /// </summary>
        [XmlElement("_TAB-SS-MATRICULA")]
        [JsonIgnore]
        public SCN4ISSSReceiveSolicitacaoServico[] solicitacoesServicoSicom { get; set; }

        /// <summary>
        /// SolicitacoesServico.
        /// </summary>
        public List<SCN4ISSSReceiveSolicitacaoServico> solicitacoesServico { get; set; }
    }
}
