using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - Tela.
    /// </summary>
    public interface ITelaRule
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
