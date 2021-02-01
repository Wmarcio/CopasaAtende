using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using System;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Util;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Copasa.Atende.Util;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Informar leitura.
    /// </summary>
    public class InformarLeituraRule : BaseRule, IInformarLeituraRule
    {
        private IISSCN5IS01Repository _isSCN5IS01Repository;
        private IISSCN5IS02Repository _isSCN5IS02Repository;
        private IISSCN5IS03Repository _isSCN5IS03Repository;
        private IORAEmpregadoRepository _oraEmpregadoRepository;
        private IMensagemRepository _mensagemRepository;
        private ILog _log;


        /// <summary>
        /// Construtor LoteOrdemServicoRule.
        /// </summary>
        /// <param name="isSCN5IS01Repository">IISSCN5IS01Repository.</param>
        /// <param name="isSCN5IS02Repository">IISSCN5IS02Repository.</param>
        /// <param name="isSCN5IS03Repository">IISSCN5IS03Repository.</param>
        /// <param name="oraEmpregadoRepository">IORAEmpregadoRepository</param>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="log">ILog</param>
        public InformarLeituraRule(
            IISSCN5IS01Repository isSCN5IS01Repository, 
            IISSCN5IS02Repository isSCN5IS02Repository, 
            IISSCN5IS03Repository isSCN5IS03Repository, 
            IORAEmpregadoRepository oraEmpregadoRepository, 
            IMensagemRepository mensagemRepository,
            ILog log)
        {
            _isSCN5IS01Repository = isSCN5IS01Repository;
            _isSCN5IS02Repository = isSCN5IS02Repository;
            _isSCN5IS03Repository = isSCN5IS03Repository;
            _oraEmpregadoRepository = oraEmpregadoRepository;
            _mensagemRepository = mensagemRepository;
            _log = log;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Entrar com matrícula e devolve dados para consistir leitura informada.
        /// </summary>
        public BaseResponse SCN5IS01(SCN5IS01Send sCN5IS01Send)
        {
            SCN5IS01Receive sCN5IS01Receive = (SCN5IS01Receive)_isSCN5IS01Repository.Connect(sCN5IS01Send);
            if (!"".Equals(sCN5IS01Receive.descricaoRetorno))
            {
                string mensagem = _mensagemRepository.parseMensagem(sCN5IS01Receive.descricaoRetorno.Trim(), sCN5IS01Receive);
                sCN5IS01Receive.descricaoRetorno = mensagem;
            }
            BaseResponse response = new BaseResponse();
            response.Model = sCN5IS01Receive;
            return response;
        }

        /// <summary>
        /// Recebe Leituras CF20 informadas.
        /// </summary>
        public BaseResponse SCN5IS03(SCN5IS03Send sCN5IS03Send)
        {
            BaseResponse response = new BaseResponse();
            SCN5IS03Receive sCN5IS03Receive = new SCN5IS03Receive();
            if (!"".Equals(sCN5IS03Send.numeroProtocolo) && (sCN5IS03Send.medidores == null || sCN5IS03Send.medidores.Length == 0))
            {
                //_log.AddLog(" Chamada sem ocorrencia de leitura:");
                //_log.AddLog(" " + sCN5IS03Send.getJson().Replace(",", "\r\n "));
                sCN5IS03Receive.descricaoRetorno = "Leitura não informada.";
            }
            else
            {
                sCN5IS03Send.telefone = retiraCaracteresMascara(sCN5IS03Send.telefone);
                if ("PORTAL DE ATENDIMENTO".Equals(sCN5IS03Send.origem.ToUpper()))
                    sCN5IS03Send.origem = "CRM";

                TrabUsuario trabUsuario = new TrabUsuario();
                trabUsuario.codigoUsuario = sCN5IS03Send.tipoOrigem;
                trabUsuario.nomeUsuario = sCN5IS03Send.origem;
                trabUsuario.agenciaUsuario = sCN5IS03Send.agenciaUsuario;
                sCN5IS03Send.usuarioInterno = _oraEmpregadoRepository.preencheDadosUsuario(trabUsuario);
                sCN5IS03Send.tipoOrigem = trabUsuario.codigoUsuario;
                sCN5IS03Send.origem = trabUsuario.nomeUsuario;
                sCN5IS03Send.agenciaUsuario = trabUsuario.agenciaUsuario;

                string retornoSCN5IS02 = "";
                if ("S".Equals(sCN5IS03Send.informarLeitura))
                {
                    SCN5IS02Send sCN5IS02Send = new SCN5IS02Send();
                    sCN5IS02Send.setValues(sCN5IS03Send);
                    sCN5IS02Send.medidores = sCN5IS03Send.medidores;
                    //_log.AddLog(" Chamada SCN5IS02:");
                    //_log.AddLog(" " + sCN5IS02Send.getJson().Replace(",", "\r\n "));
                    if (sCN5IS02Send.numeroProtocolo == null || "".Equals(sCN5IS02Send.numeroProtocolo))
                        sCN5IS02Send.numeroProtocolo = "0";
                    SCN5IS02Receive sCN5IS02Receive = (SCN5IS02Receive)_isSCN5IS02Repository.Connect(sCN5IS02Send);
                    //_log.AddLog(" " + _isSCN5IS02Repository.GetEnvio());
                    //_log.AddLog(" Retorno SCN5IS02:");
                    //_log.AddLog(" " + sCN5IS02Receive.getJson().Replace(",", "\r\n "));
                    retornoSCN5IS02 = sCN5IS02Receive.descricaoRetorno;
                }

                if ("S".Equals(sCN5IS03Send.CF20))
                {
                    //_log.AddLog(" Chamada SCN5IS03:");
                    //_log.AddLog(" " + sCN5IS03Send.getJson().Replace(",", "\r\n "));
                    sCN5IS03Receive = (SCN5IS03Receive)_isSCN5IS03Repository.Connect(sCN5IS03Send);
                    //_log.AddLog(" Retorno SCN5IS03:");
                    //_log.AddLog(" " + sCN5IS03Receive.getJson().Replace(",", "\r\n "));
                }

                if (string.IsNullOrEmpty(sCN5IS03Receive.descricaoRetorno) && !string.IsNullOrEmpty(retornoSCN5IS02))
                    sCN5IS03Receive.descricaoRetorno = retornoSCN5IS02;

                if (!string.IsNullOrEmpty(sCN5IS03Receive.descricaoRetorno))
                {
                    string mensagem = _mensagemRepository.parseMensagem(sCN5IS03Receive.descricaoRetorno.Trim());
                    sCN5IS03Receive.descricaoRetorno = mensagem;
                }
            }
            //_log.AddLog(" Retorno:");
            //_log.AddLog(" " + sCN5IS03Receive.getJson().Replace(",", "\r\n "));
            response.Model = sCN5IS03Receive;
            return response;
        }

        private string retiraCaracteresMascara(string cpfCnpj)
        {
            char[] chars = cpfCnpj.ToCharArray();
            List<char> retorno = new List<char>();
            foreach (char cr in chars)
            {
                long r;
                if (long.TryParse(cr.ToString(), out r))
                    retorno.Add(cr);
            }
            return new string(retorno.ToArray());
        }

    }
}
