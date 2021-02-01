using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{

    /// <summary>
    /// Dyn365ProtocoloDetalhe - Model resumida de protocolo/incident no Dynamics - 
    /// </summary>
    [Dyn365Name("incidents")]

    public class Dyn365ProtocoloHistoricoDetalhe : BaseModel
    {
        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [Dyn365IdCopasa("copasa_protocolo")]
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        public string descricaoServicoSS { get; set; }

        /// <summary>
        /// DataPrevicaoSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_previsaoatendimentoss")]
        public System.DateTime? dataPrevisaoSSDyn365 { get; set; }

        /// <summary>
        /// DataBaixaSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_terminodoatendimento")]
        public System.DateTime? dataTerminoAtendimentoDyn365 { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        [Dyn365Name("copasa_codigostatusss")]
        public string situacaoSS { get; set; }

        /// <summary>
        /// IdServicoExpand.
        /// </summary>
        [Dyn365DisplayBind("copasa_servicosid")]
        public object IdServicoExpand { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365DisplayBind("copasa_subtipoid")]
        public object IdSubTipoServicoExpand { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        [Dyn365Name("createdon")]
        public System.DateTime? CreatedOn { get; set; }

        /// <summary>
        /// RespostaAoSolicitante
        /// </summary>
        [Dyn365Name("copasa_respostaaosolicitante")]
        public string RespostaAoSolicitante { get; set; }
    }
}
