using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Informar leitura.
    /// </summary>
    public class InformarLeituraFacade : IInformarLeituraFacade
    {
        private IInformarLeituraRule _informarLeituraRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="informarLeituraRule">IInformarLeituraRule.</param>
        public InformarLeituraFacade(IInformarLeituraRule informarLeituraRule)
        {
            _informarLeituraRule = informarLeituraRule;
        }

        /// <summary>
        /// Entrar com matrícula e devolver dados para consistir leitura informada.
        /// </summary>
        public BaseResponse SCN5IS01(SCN5IS01Send sCN5IS01Send)
        {
            return _informarLeituraRule.SCN5IS01(sCN5IS01Send);
        }

        /// <summary>
        /// Recebe Leituras CF20 informadas.
        /// </summary>
        public BaseResponse SCN5IS03(SCN5IS03Send sCN5IS03Send)
        {
            return _informarLeituraRule.SCN5IS03(sCN5IS03Send);
        }
    }
}
