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
    public class DHistoricoServicoDetalheReceive : BaseModelReceive
    {
        /// <summary>
        /// Protocolos
        /// </summary>
        public List<Dyn365ProtocoloDetalhe> Protocolos { get; set; }
    }

    /// <summary>
    /// numeroProtocoloSS
    /// </summary>
    public class DHistoricoServicoDetalheSend : BaseModelSend
    {
        /// <summary>
        /// cpfcnpj
        /// </summary>
        public string cpfcnpj { get; set; }

    }
}

