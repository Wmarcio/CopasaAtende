using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// URAHistoricoProtocoloReceive
    /// </summary>
    public class URAHistoricoProtocoloReceive : BaseModelReceive
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProtocoloId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Protocolo { get; set; }
        /// <summary>
        /// Protocolos
        /// </summary>
        public List<URAHistoricoProtocoloReceiveProtocolo> Protocolos { get; set; }
    }
}
