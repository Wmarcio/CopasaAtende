using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365OrdemServico- Tabela de ordens de serviço no Dynamics
    /// </summary>
    [Dyn365Name("copasa_ordemdeservicos")]
    public class Dyn365OrdemServico : BaseModel  
    {
        /// <summary>
        /// TipoInformacaoOS.
        /// </summary>
        public string tipoInformacaoOS { get; set; }

        /// <summary>
        /// NumeroProtocoloOS.
        /// </summary>
        public string numeroProtocoloOS { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServicoOS.
        /// </summary>
        public string numeroSolicitacaoServicoOS { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [Dyn365IdCopasa("copasa_numerodaos")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// CodigoServicoOS.
        /// </summary>
        [Dyn365Name("copasa_codigoservicoos")]
        public string codigoServicoOS { get; set; }

        /// <summary>
        /// DescricaoServicoOS.
        /// </summary>
        [Dyn365Name("copasa_name")]
        public string descricaoServicoOS { get; set; }

        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        public string dataGeracaoOS { get; set; }

        /// <summary>
        /// HoraGeracaoOS.
        /// </summary>
        public string horaGeracaoOS { get; set; }

        /// <summary>
        /// dataPrevisaoOSDyn365.
        /// </summary>
        [Dyn365Name("copasa_dataprevisao")]
        public string dataPrevisaoOSDyn365 { get; set; }

        /// <summary>
        /// DataPrevisaoOS.
        /// </summary>
        public string dataPrevisaoOS { get; set; }

        /// <summary>
        /// HoraPrevisaoOS.
        /// </summary>
        public string horaPrevisaoOS { get; set; }

        /// <summary>
        /// dataBaixaOSDyn365.
        /// </summary>
        [Dyn365Name("copasa_conclusaodaos")]
        public string dataBaixaOSDyn365 { get; set; }

        /// <summary>
        /// DataBaixaOS.
        /// </summary>
        public string dataBaixaOS { get; set; }

        /// <summary>
        /// HoraBaixaOS.
        /// </summary>
        public string horaBaixaOS { get; set; }

        /// <summary>
        /// SituacaoOS.
        /// </summary>
        [Dyn365Name("copasa_statusos")]
        public string situacaoOS { get; set; }

        /// <summary>
        /// DescricaoSituacaoOS.
        /// </summary>
        [Dyn365Name("copasa_descsituacao")]
        public string descricaoSituacaoOS { get; set; }

        /// <summary>
        /// DescricaoOcorrenciaBaixaOS.
        /// </summary>
        [Dyn365Name("copasa_descbaixa")]
        public string descricaoOcorrenciaBaixaOS { get; set; }

        /// <summary>
        /// ObservacaoBaixa.
        /// </summary>
        [Dyn365Name("copasa_obsbaixaos")]
        public string observacaoBaixa { get; set; }

        /// <summary>
        /// IdDyn365.
        /// </summary>
        [Dyn365Id("copasa_ordemdeservicoid")]
        public string idDyn365 { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365Bind("incidents", "copasa_Solicitacaoid")]
        public string idSolicitacaoBind { get; set; }

    }
}
