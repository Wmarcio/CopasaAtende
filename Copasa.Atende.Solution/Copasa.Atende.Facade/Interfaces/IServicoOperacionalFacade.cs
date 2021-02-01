﻿using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface Facade - Serviços operacionais.
    /// </summary>
    public interface IServicoOperacionalFacade
    {
        /// <summary>
        /// Busca solicitações de serviços de uma matrícula.
        /// </summary>
        BaseResponse SCN4ISSS(SCN4ISSSSend sCN4ISSSSend);

        /// <summary>
        /// Busca OS's de uma solicitação de serviço.
        /// </summary>
        BaseResponse SCN4ISOR(SCN4ISORSend sCN4ISORSend);

        /// <summary>
        /// Gera eventos prioridade.
        /// </summary>
        BaseResponse SCN4CRAL(SCN4CRALSend sCN4CRALSend);

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        BaseResponse AtualizaDynamicsBaxiasOS();

        /// <summary>
        /// Busca serviço gerados no Sicom para atualizar o Dynamics 365 .
        /// </summary>
        BaseResponse AtualizaDynamicsOSGeradas();

        /// <summary>
        /// Cria solicitação de serviço.
        /// </summary>
        BaseResponse SCN4CRSS(SCN4CRSSSend sCN4CRSSSend);

        /// <summary>
        /// Cancela a solicitação de serviço
        /// </summary>
        /// <param name="sCN4CASSSend"></param>
        /// <returns></returns>
        BaseResponse SCN4CASS(SCN4CASSSend sCN4CASSSend);

        /// <summary>
        /// Gera OS extra para SS existente
        /// </summary>
        /// <param name="sCN4CREXSend"></param>
        /// <returns></returns>
        BaseResponse SCN4CREX(SCN4CREXSend sCN4CREXSend);

        /// <summary>
        /// Gera alteração de economias
        /// </summary>
        /// <param name="sCN4ISAESend"></param>
        /// <returns></returns>
        BaseResponse SCN4ISAE(SCN4ISAESend sCN4ISAESend);


    }
}