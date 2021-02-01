using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Religação.
    /// </summary>
    public class ReligacaoFacade : IReligacaoFacade
    {
        private IReligacaoRule _religacaoRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="religacaoRule">IReligacaoRule.</param>
        public ReligacaoFacade(IReligacaoRule religacaoRule)
        {
            _religacaoRule = religacaoRule;
        }

        /// <summary>
        /// Religação - Busca Dados para religação leitura
        /// </summary>
        public BaseResponse SCN4ISRL(SCN4ISRLSend _sCN4ISRLSend)
        {
            return _religacaoRule.SCN4ISRL(_sCN4ISRLSend);
        }

        /// <summary>
        /// Religação - Busca dados parcelamento religação
        /// </summary>
        /// <param name="_sCN4ISCPSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISCP(SCN4ISCPSend _sCN4ISCPSend)
        {
            return _religacaoRule.SCN4ISCP(_sCN4ISCPSend);
        }

        /// <summary>
        /// Salva dados religação
        /// </summary>
        /// <param name="_sCN4ISRESend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISRE(SCN4ISRESend _sCN4ISRESend)
        {
            return _religacaoRule.SCN4ISRE(_sCN4ISRESend);
        }

        /// <summary>
        /// Busca todos os dados sobre religação de água
        /// </summary>
        /// <param name="_buscaReligacaoSend"></param>
        /// <returns></returns>
        public BaseResponse BuscaReligacao(TrabBuscaReligacaoSend _buscaReligacaoSend)
        {
            return _religacaoRule.BuscaReligacao(_buscaReligacaoSend);
        }
    }
}
