using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{

    /// <summary>
    /// Interface ISubTituloFacade.
    /// </summary>
    public interface ITelaFacade
    {
        /// <summary>
        /// Buscar tela por Id.
        /// </summary>
        /// <param name="idTela">Id da tela.</param>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <returns>Objeto BaseResponse com o tela encontrado.</returns>
        BaseResponse BuscarTelaPorId(int idTela, string loginUsuario, string origem);
    }
}
