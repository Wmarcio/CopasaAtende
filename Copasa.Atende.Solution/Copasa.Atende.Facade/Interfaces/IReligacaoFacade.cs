using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface Facade - Religação.
    /// </summary>
    public interface IReligacaoFacade
    {
        /// <summary>
        /// Religação - Busca Dados para religação leitura
        /// </summary>
        BaseResponse SCN4ISRL(SCN4ISRLSend _sCN4ISRLSend);

        /// <summary>
        /// Religação - Busca dados parcelamento religação
        /// </summary>
        /// <param name="_sCN4ISCPSend"></param>
        /// <returns></returns>
        BaseResponse SCN4ISCP(SCN4ISCPSend _sCN4ISCPSend);

        /// <summary>
        /// Salva dados religação
        /// </summary>
        /// <param name="_sCN4ISRESend"></param>
        /// <returns></returns>
        BaseResponse SCN4ISRE(SCN4ISRESend _sCN4ISRESend);

        /// <summary>
        /// Busca todos os dados sobre religação de água
        /// </summary>
        /// <param name="_buscaReligacaoSend"></param>
        /// <returns></returns>
        BaseResponse BuscaReligacao(TrabBuscaReligacaoSend _buscaReligacaoSend);
    }
}
