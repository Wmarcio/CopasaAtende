using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365Protocolo - Tabela de protocolo/incident no Dynamics
    /// </summary>
    [Dyn365Name("incidents")]
    public class Dyn365ProtocoloApp : BaseModel
    {
       
        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [Dyn365IdCopasa("copasa_protocolo")]
        public string numeroProtocoloSS { get; set; }
                
        /// <summary>
        /// CodigoServico.
        /// </summary>
        [Dyn365Name("copasa_codigoservico")]
        public string codigoServico { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        [Dyn365Name("copasa_codigodass")]
        public string numeroSS { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        [Dyn365Name("copasa_codigostatusss")]
        public string situacaoSS { get; set; }

        /// <summary>
        /// DescSituacaoSolicitacao.
        /// </summary>
        [Dyn365Name("copasa_statusdoservico")]
        public string descSituacaoSolicitacao { get; set; }

        /// <summary>
        /// StatusProtocolo.
        /// </summary>
        [Dyn365Name("statecode")]
        public int statusProtocolo { get; set; }

        /// <summary>
        /// DescStatusProtocolo.
        /// </summary>
        [Dyn365DisplayBind("statecode")]
        public string descStatusProtocolo { get; set; }

        /// <summary>
        /// StatusSolicitacao.
        /// </summary>
        [Dyn365Name("statuscode")]
        public int statusSolicitacao { get; set; }

        /// <summary>
        /// DescStatusSolicitacao.
        /// </summary>
        [Dyn365DisplayBind("statuscode")]
        public string descStatusSolicitacao { get; set; }

        /// <summary>
        /// RespostaAoSolicitante
        /// </summary>
        [Dyn365Name("copasa_respostaaosolicitante")]
        public string RespostaAoSolicitante { get; set; }

        /// <summary>
        /// DataPrevicaoSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_previsaoatendimentoss")]
        public System.DateTime dataPrevisaoSSDyn365 { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [Dyn365Name("copasa_identificadorintegracao")]
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [Dyn365Name("copasa_matriculaintegracao")]
        public string matricula { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365Name("copasa_subtipoid")]
        public object IdSubTipoServicoExpand { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        [Dyn365Name("createdon")]
        public System.DateTime CreatedOn { get; set; }

    }
}
