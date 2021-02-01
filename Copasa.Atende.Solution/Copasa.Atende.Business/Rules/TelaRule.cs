using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Repositories.Digital;
using Copasa.Util.Enumerador;
using System;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Classe TelaRule.
    /// </summary>
    public class TelaRule : BaseRule, ITelaRule
    {
        
        /// <summary>
        /// Buscar tela por Id.
        /// </summary>
        /// <param name="idTela">Id da tela.</param>
        /// <param name="loginUsuario">login do usuário.</param>
        /// <param name="origem">Origem da chamada.</param>
        /// <returns>Objeto BaseResponse com o tela encontrado.</returns>
        public BaseResponse BuscarTelaPorId(int idTela, string loginUsuario, string origem)
        {
            try
            {
                var baseResponse = new BaseResponse();

                var telaModel = RepositoryFactory.UnitOfWork.TelaRepository.Get(x => x.IdTela.Equals(idTela));

                if (telaModel != null)
                {
                    baseResponse.Model = telaModel;
                }
                else
                {
                //    baseResponse.Message = new BaseMessage()
                //    {
                //     //   Message = "Nenhuma tela encontrada para o código informado.",
                //    //    TipoMensagem = TipoMensagem.W
                //    };

                    baseResponse.IsValid = false;
                    baseResponse.Erro = new Exception("Nenhuma tela encontrada para o código informado.");
                }

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse(ex);
            }
        }

        /// <summary>
        /// deve ser implementada de forma a retornar o nome da entdiade que sera exibido nas mensagens.
        /// </summary>
        /// <returns></returns>
        public override string GetEntidadeNome()
        {
            return "tela";
        }
    }
}
