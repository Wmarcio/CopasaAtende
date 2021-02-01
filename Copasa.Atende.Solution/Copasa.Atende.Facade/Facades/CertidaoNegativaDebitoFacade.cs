using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Busca a certidão negativa de débito
    /// </summary>
    public class CertidaoNegativaDebitoFacade : ICertidaoNegativaDebitoFacade
    {
        private ICertidaoNegativaDebitoRule _certidaoNegativaDebitoRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certidaoNegativaDebitoRule"></param>
        public CertidaoNegativaDebitoFacade(ICertidaoNegativaDebitoRule certidaoNegativaDebitoRule)
        {
            _certidaoNegativaDebitoRule = certidaoNegativaDebitoRule;
        }

        /// <summary>
        /// Método que retorna a certidão negativa
        /// </summary>
        /// <param name="sCN6ISCNSend"></param>
        /// <returns></returns>
        public BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend)
        {
            return _certidaoNegativaDebitoRule.SCN6ISCN(sCN6ISCNSend);
        }
    }
}
