using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface ITituloRule.
    /// </summary>
    public interface ITituloRule
    {
        /// <summary>
        /// Buscar Titulo por Id.
        /// </summary>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <param name="idTitulo">Id da entidade</param>
        /// <returns>Objeto BaseResponse com o Titulo encontrado.</returns>
        BaseResponse BuscarTituloPorId(string loginUsuario, string origem, int idTitulo);

        /// <summary>
        /// Listar Títulos.
        /// </summary>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <returns>Objeto BaseResponse com Título.</returns>
        BaseResponse ListarTitulos(string loginUsuario, string origem);
    }
}
