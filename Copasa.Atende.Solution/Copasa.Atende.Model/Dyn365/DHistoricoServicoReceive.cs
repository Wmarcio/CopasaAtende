using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// 
    /// </summary>
    public class DHistoricoServicoReceive : BaseModelReceive
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
        public List<Dyn365Protocolo> Protocolos { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class DHistoricoServicoSend : BaseModelSend
    {
        /// <summary>
        /// cpfcnpj
        /// </summary>
        public string cpfcnpj { get; set; }

        /// <summary>
        /// idCpfCnpj
        /// </summary>
        public string idCpfCnpj { get; set; }

    }
}
