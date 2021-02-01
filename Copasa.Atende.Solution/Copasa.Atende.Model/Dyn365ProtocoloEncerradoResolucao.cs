using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365ProtocoloEncerradoResolucao - Tabela de resolução de protocolo/incident encerrados no Dynamics
    /// </summary>
    public class Dyn365ProtocoloEncerradoResolucao : BaseModel
    {
        /// <summary>
        /// Sujeito.
        /// </summary>
        [Dyn365Name("subject")]
        public string sujeito { get; set; }

        /// <summary>
        /// idProtocoloRelacionadoBind.
        /// </summary>
        [Dyn365Bind("/incidents", "incidentid")]
        public string idProtocoloRelacionadoBind { get; set; }

        /// <summary>
        /// TempoGasto.
        /// </summary>
        [Dyn365Name("timespent")]
        public int tempoGasto { get; set; }

        /// <summary>
        /// Descricao.
        /// </summary>
        [Dyn365Name("description")]
        public string descricao { get; set; }

    }
}
