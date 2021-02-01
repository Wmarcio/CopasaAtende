using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Vazamento.
    /// </summary>
    public class VazamentoFacade : IVazamentoFacade
    {
        private IVazamentoRule _vazamentoRuaRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="vazamentoRuaRule">IEstouSemAguaRule.</param>
        public VazamentoFacade(IVazamentoRule vazamentoRuaRule)
        {
            _vazamentoRuaRule = vazamentoRuaRule;
        }

        /// <summary>
        /// Vazamento no imovel - SCN4ISVI.
        /// </summary>
        public BaseResponse SCN4ISVI(SCN4ISVISend sCN4ISVISend)
        {
            return _vazamentoRuaRule.SCN4ISVI(sCN4ISVISend);
        }

        /// <summary>
        /// Vazamento na rua - SCN4ISVR
        /// </summary>
        public BaseResponse SCN4ISVR(SCN4ISVRSend sCN4ISVRSend)
        {
            return _vazamentoRuaRule.SCN4ISVR(sCN4ISVRSend);
        }
    }
}
