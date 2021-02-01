using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Repositories.Digital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Business.Rules
{

    /// <summary>
    /// Classe TituloRule
    /// </summary>
    public class TituloRule : BaseRule, ITituloRule
    {
        /// <summary>
        /// Deve ser implementada de forma a retornar o nome da entdiade que sera exibido nas mensagens.
        /// </summary>
        /// <returns></returns>
        public override string GetEntidadeNome()
        {
            return "Título";
        }

        /// <summary>
        /// Buscar Título por Id.
        /// </summary>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <param name="idTitulo">Id do título.</param>
        /// <returns>Objeto BaseResponse com o subTitulo encontrado.</returns>
        public BaseResponse BuscarTituloPorId(string loginUsuario, string origem, int idTitulo)
        {
            try
            {
                var baseResponse = new BaseResponse();

                baseResponse.Model = RepositoryFactory.UnitOfWork.TituloRepository.Get(x => x.IdTitulo.Equals(idTitulo));

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse(ex);
            }
        }

        /// <summary>
        /// Listar Títulos.
        /// </summary>
        /// <param name="loginUsuario">Login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <returns></returns>
        public BaseResponse ListarTitulos(string loginUsuario, string origem)
        {
            try
            {
                var baseResponse = new BaseResponse();

                baseResponse.Collection = RepositoryFactory.UnitOfWork.TituloRepository.GetAll().ToList<BaseModel>();

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse(ex);
            }
        }

    }
}