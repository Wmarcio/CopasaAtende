using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365ProtocoloEncerrado - Tabela de protocolo/incident encerrados no Dynamics
    /// </summary>
    [Dyn365Name("CloseIncident")]
    public class Dyn365ProtocoloEncerrado : BaseModel
    {
        /// <summary>
        /// Resolucao.
        /// </summary>
        [Dyn365Name("IncidentResolution")]
        public Dyn365ProtocoloEncerradoResolucao resolucao { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        [Dyn365Name("Status")]
        public int status { get; set; }

    }
}
