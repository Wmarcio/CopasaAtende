using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model.Core;


namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Classe TelaFacade.
    /// </summary>
    public class TelaFacade : ITelaFacade
    {

        private ITelaRule _telaRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="telaRule">IClienteRule.</param>
        public TelaFacade(ITelaRule telaRule)
        {
            _telaRule = telaRule;
        }

        /// <summary>
        /// Buscar por ID da tela.
        /// </summary>
        /// <param name="idTela">ID da tela.</param>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <returns>Objeto BaseResponse</returns>
        public BaseResponse BuscarTelaPorId(int idTela, string loginUsuario, string origem)
        {
            return _telaRule.BuscarTelaPorId(idTela, loginUsuario, origem);
        }
    }
}
