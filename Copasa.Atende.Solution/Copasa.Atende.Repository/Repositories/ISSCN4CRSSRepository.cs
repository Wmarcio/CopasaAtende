using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CRSSRepository - Cria solicitação de serviço
    /// </summary>
    public class ISSCN4CRSSRepository : ISRepository<SCN4CRSSReceive>, IISSCN4CRSSRepository
    {
        private IMensagemRepository _mensagemRepository;
        private IORAEmpregadoRepository _oraEmpregadoRepository;

        /// <summary>
        /// Contrutor.
        /// </summary>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="oraEmpregadoRepository">IORAEmpregadoRepository</param>
        /// <param name="log">ILog</param>
        public ISSCN4CRSSRepository(
            IMensagemRepository mensagemRepository,
            IORAEmpregadoRepository oraEmpregadoRepository
            , ILog log)
         : base("GeracaoServicoSICOM:SCN4CRSS_WSD/GeracaoServicoSICOM_SCN4CRSS_WSD_Port", "SCN4CRSS",log)
        {
            _mensagemRepository = mensagemRepository;
            _oraEmpregadoRepository = oraEmpregadoRepository;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS cria solicitação de serviço";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CRSSReceive baseModelReceive)
        {
            int codigoRetorno = 0;
            bool usuarioInterno = ((SCN4CRSSSend)baseModelSend).usuarioInterno;
            try
            {
                if (!"".Equals(baseModelReceive.codigoRetornoSicom))
                    codigoRetorno = int.Parse(baseModelReceive.codigoRetornoSicom);
                else if (!"".Equals(baseModelReceive.descricaoRetornoSicom))
                {
                    if (baseModelReceive.descricaoRetornoSicom.Length > 4 && int.TryParse(baseModelReceive.descricaoRetornoSicom.Substring(0, 4).Trim(), out codigoRetorno))
                    { }
                }
            }
            catch (Exception) { }
            if (codigoRetorno == 0)
            {
                baseModelReceive.descricaoRetorno = _mensagemRepository.geraMensagemComDataPrazoEProtocolo(0002, baseModelReceive.dataPrevisaoSS, usuarioInterno, baseModelReceive);
            }
            else
            {
                baseModelReceive.descricaoRetorno = _mensagemRepository.geraMensagemComDataPrazoEProtocolo(codigoRetorno, baseModelReceive.dataPrevisaoSS, usuarioInterno, baseModelReceive);
                if ("".Equals(baseModelReceive.descricaoRetorno))
                {
                    if (!"".Equals(baseModelReceive.descricaoRetornoSicom))
                    {
                        baseModelReceive.descricaoRetorno = baseModelReceive.descricaoRetornoSicom;
                    }
                    else if (!"".Equals(baseModelReceive.codigoRetornoSicom))
                    {
                        baseModelReceive.descricaoRetorno = baseModelReceive.codigoRetornoSicom;
                    }
                }
            }
        }
    }
}
