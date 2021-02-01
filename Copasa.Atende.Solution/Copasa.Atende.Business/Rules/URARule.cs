using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Model.Dyn365.Protocolo;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Repositories;
using Copasa.Atende.Repository.Repositories.Dyn365;
using System;
using System.Collections.Generic;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - URA.
    /// </summary>
    public class URARule : BaseRule, IURARule
    {
        private IClienteRule _clienteRule;
        private IDClienteRule _dClienteRule;
        private IMensagemRepository _mensagemRepository;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="clienteRule">IClienteRule.</param>
        /// <param name="dClienteRule">IDClienteRule.</param>
        /// <param name="mensagemRepository">IMensagemRepository.</param>
        public URARule(
            IClienteRule clienteRule,
            IDClienteRule dClienteRule,
            IMensagemRepository mensagemRepository)
        {
            _clienteRule = clienteRule;
            _dClienteRule = dClienteRule;
            _mensagemRepository = mensagemRepository;
        }

        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj
        /// </summary>
        public BaseResponse ListaIdentificador(URAIdentificadorListaSend uRAIdentificadorListaSend)
        {
            DListaIdentificadorSend dyn365ListaIdentificadorSend = new DListaIdentificadorSend();
            dyn365ListaIdentificadorSend.IdCpfCnpj = uRAIdentificadorListaSend.IdCpfCnpj;
            DListaIdentificadorReceive dyn365ListaIdentificadorReceive = (DListaIdentificadorReceive)_dClienteRule.ListaIdentificador(dyn365ListaIdentificadorSend).Model;
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
            sCN4ISU1Send.cpfCnpj = uRAIdentificadorListaSend.cpfCnpj;
            sCN4ISU1Send.identificadores = new string[dyn365ListaIdentificadorReceive.Identificador.Count];
            int cont = 0;
            foreach (DIdentificador identificador in dyn365ListaIdentificadorReceive.Identificador)
            {
                sCN4ISU1Send.identificadores[cont] = identificador.Identificador;
                cont++;
            }
            return _clienteRule.SCN4ISU1(sCN4ISU1Send);
        }

        /// <summary>
        /// Histórico de protocolo no Dynamics
        /// </summary>
        public BaseResponse GetHistoricoProtocolo(URAHistoricoProtocoloSend uRAHistoricoProtocoloSend)
        {
            DHistoricoServicoSendApp dHistoricoServicoSendApp = new DHistoricoServicoSendApp();
            dHistoricoServicoSendApp.setValues(uRAHistoricoProtocoloSend);

            BaseResponse baseResponse = _dClienteRule.GetHistoricoServico(dHistoricoServicoSendApp);
            DHistoricoServicoReceiveApp dHistoricoServicoReceiveApp = (DHistoricoServicoReceiveApp)baseResponse.Model;
            URAHistoricoProtocoloReceive uRAHistoricoProtocoloReceive = new URAHistoricoProtocoloReceive();
            uRAHistoricoProtocoloReceive.setValues(dHistoricoServicoReceiveApp);
            if (uRAHistoricoProtocoloReceive.descricaoRetorno == null)
                uRAHistoricoProtocoloReceive.descricaoRetorno = "";
            List<URAHistoricoProtocoloReceiveProtocolo> protocolosFiltrados = new List<URAHistoricoProtocoloReceiveProtocolo>();
            foreach (Dyn365ProtocoloApp protocolo in dHistoricoServicoReceiveApp.Protocolos)
            {
                if ("".Equals(uRAHistoricoProtocoloSend.codigoSicom) ||
                    (!"".Equals(uRAHistoricoProtocoloSend.codigoSicom) && uRAHistoricoProtocoloSend.codigoSicom.Equals(protocolo.codigoServico)))
                {
                    URAHistoricoProtocoloReceiveProtocolo uRAHistoricoProtocoloReceiveProtocolo = new URAHistoricoProtocoloReceiveProtocolo();
                    uRAHistoricoProtocoloReceiveProtocolo.setValues(protocolo);
                    uRAHistoricoProtocoloReceiveProtocolo.dataCriacao = protocolo.CreatedOn.ToString("dd/MM/yyyy HH:mm");
                    if (protocolo.dataPrevisaoSSDyn365.Year > 1)
                        uRAHistoricoProtocoloReceiveProtocolo.dataPrevisaoAtendimento = protocolo.dataPrevisaoSSDyn365.ToString("dd/MM/yyyy HH:mm");
                    protocolosFiltrados.Add(uRAHistoricoProtocoloReceiveProtocolo);
                }
            }
            uRAHistoricoProtocoloReceive.Protocolos = protocolosFiltrados;
            if (!"".Equals(uRAHistoricoProtocoloReceive.descricaoRetorno) || uRAHistoricoProtocoloReceive.Protocolos.Count == 0)
            {
                string codigoRetorno = "0225";
                if (!"".Equals(uRAHistoricoProtocoloReceive.descricaoRetorno))
                    codigoRetorno = uRAHistoricoProtocoloReceive.descricaoRetorno;
                uRAHistoricoProtocoloReceive.descricaoRetorno = _mensagemRepository.geraMensagem(codigoRetorno);
            }
            baseResponse.Model = uRAHistoricoProtocoloReceive;
            return baseResponse;
        }

        /// <summary>
        /// Retorna Detalhes de um protocolo
        /// </summary>
        public BaseResponse GetHistoricoServicoDetalhe(URAHistoricoProtocoloDetalheSend uRAHistoricoProtocoloDetalheSend)
        {
            Dyn365Protocolo dyn365Protocolo = new Dyn365Protocolo();
            dyn365Protocolo.numeroProtocolo = uRAHistoricoProtocoloDetalheSend.protocolo;
            IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
            BaseResponse baseResponse = new BaseResponse();
            List<Dyn365Protocolo> retorno =  dyn365ProtocoloRepository.Pesquisar(dyn365Protocolo);
            if (retorno.Count > 0)
            {
                URAHistoricoProtocoloDetalheReceive uRAHistoricoProtocoloDetalheReceive = new URAHistoricoProtocoloDetalheReceive();
                uRAHistoricoProtocoloDetalheReceive.setValues(retorno.ToArray()[0]);
                baseResponse.Model = uRAHistoricoProtocoloDetalheReceive;
            }
            return baseResponse;
        }

        /// <summary>
        /// Atualiza protocolo no Dynamics
        /// </summary>
        public BaseResponse AtualizaProtocolo(Dyn365ProtocoloURASend dyn365ProtocoloURASend)
        {
            Dyn365ProtocoloURAReceive dyn365ProtocoloURAReceive = new Dyn365ProtocoloURAReceive();
            Dyn365Protocolo dyn365Protocolo = new Dyn365Protocolo();
            dyn365ProtocoloURASend.tipoComplementoImovel = "";
            if (dyn365Protocolo.setValues(dyn365ProtocoloURASend))
            {
                IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
                dyn365Protocolo.incidentid = dyn365ProtocoloURASend.idProtocolo;
                dyn365Protocolo.codigoSubtipoServico = dyn365ProtocoloURASend.codigoServico.Substring(0, 5);
                dyn365Protocolo.codigotipoLogradouro = dyn365ProtocoloRepository.getCodigoTipoLogradouro(dyn365Protocolo.tipoLogradouro);
                if (dyn365ProtocoloRepository.Atualizar(dyn365Protocolo))
                    dyn365ProtocoloURAReceive.descricaoRetorno = _mensagemRepository.geraMensagem("0226");
                else
                    dyn365ProtocoloURAReceive.descricaoRetorno = _mensagemRepository.geraMensagem("0227");
            }
            else
                dyn365ProtocoloURAReceive.descricaoRetorno = _mensagemRepository.geraMensagem("0227");
            BaseResponse baseResponse = new BaseResponse();
            baseResponse.Model = dyn365ProtocoloURAReceive;
            return baseResponse;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new System.NotImplementedException();
        }
    }
}
