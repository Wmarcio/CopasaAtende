﻿using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - Informar leitura.
    /// </summary>
    public interface IInformarLeituraRule
    {
        /// <summary>
        /// Entrar com matrícula e devolver dados para consistir leitura informada.
        /// </summary>
        BaseResponse SCN5IS01(SCN5IS01Send sCN5IS01Send);

        /// <summary>
        /// Recebe Leituras CF20 informadas.
        /// </summary>
        BaseResponse SCN5IS03(SCN5IS03Send sCN5IS03Send);
    }
}