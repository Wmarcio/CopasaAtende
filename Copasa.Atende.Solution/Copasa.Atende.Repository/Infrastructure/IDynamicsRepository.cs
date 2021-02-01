using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;
using System;
using System.Collections.Generic;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface DynamicsRepository
    /// </summary>
    public interface IDynamicsRepository<T> where T : class
    {
        /// <summary>
        /// Incluir registro em tabela do Dynamics 365
        /// </summary>
        bool Incluir(T baseModel);

        /// <summary>
        /// Incluir registro em tabela do Dynamics 365
        /// </summary>
        bool Incluir(T baseModel, ILog log);

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        bool Atualizar(T baseModel);

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        bool Atualizar(T baseModel, ILog log);

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        bool Atualizar(T baseModel, string valorBind, ILog log);

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        void AtualizarAsync(T baseModel, ILog log);

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        void AtualizarAsync(T baseModel, string valorBind, ILog log);

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        List<T> Pesquisar(string nomeCampo, string valor);

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        List<T> Pesquisar(List<string> listaNomeCampo, List<string> listaValor);

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        List<T> Pesquisar(BaseModel baseModel);

        /// <summary>
        /// Pesquisa lista no ws
        /// </summary>
        List<BaseModel> DPesquisarLista(string filtro, Type tipoRetorno);
                        
        /// <summary>
        /// Executa serviço de uma tabela do Dynamics 365 e retorna uma instância da classe de retorno
        /// </summary>
        BaseModel ExecutarServico(string nomeServico, T baseModelRequest, Type tipoRetorno);

        /// <summary>
        /// Fornece o conteúdo do retorno da chamada do método callWebService
        /// </summary>
        string getDadosRetorno();

        /// <summary>
        /// Fornece o conteúdo do que é enviado na chamada do método callWebService
        /// </summary>
        string getDadosEnvio();

        /// <summary>
        /// Fornece o endereço de chamada da conexão com o Dynamicas 365
        /// </summary>
        string getEnderecoConexao();
    }
}
