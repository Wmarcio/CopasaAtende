using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// URAHistoricoProtocoloSend - Histórico de protocolo no Dynamics no Dynamics
    /// </summary>
    public class URAHistoricoProtocoloSend : BaseModelSend
    {
        /// <summary>
        /// CodigoSicom
        /// </summary>
        public string codigoSicom { get; set; }

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
