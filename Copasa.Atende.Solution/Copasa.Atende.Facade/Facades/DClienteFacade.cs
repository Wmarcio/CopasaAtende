using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Broker;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Model.Dyn365.Localidade;
using Copasa.Atende.Model.Dyn365.Protocolo;
using Copasa.Atende.Model.Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Facade.Facades
{

    /// <summary>
    /// Facade - Dynamics 365 Cliente.
    /// </summary>
    public class DClienteFacade : IDClienteFacade
    {
        private IDClienteRule _dclienteRule;


        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="dclienteRule">IClienteRule.</param>
        public DClienteFacade(IDClienteRule dclienteRule) { _dclienteRule = dclienteRule; }

        /// <summary>
        /// consulta um identificador no D365, retornando seu id
        /// </summary>
        /// <param name="dConsultaIdentificadorSend"></param>
        /// <returns></returns>
        public BaseResponse ConsultaIdentificador(DConsultaIdentificadorSend dConsultaIdentificadorSend) { return _dclienteRule.ConsultaIdentificador(dConsultaIdentificadorSend); }

        /// <summary>
        /// criação de protocolo.
        /// </summary>
        /// <param name="criaProtocoloSend"></param>
        /// <returns></returns>
        public BaseResponse CriaProtocolo(DCriaProtocoloSend criaProtocoloSend) { return _dclienteRule.CriaProtocolo(criaProtocoloSend); }

        /// <summary>
        /// Cria um Novo Identificador no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse CadastraIdentificador(DCadastraIdentificadorSend dCadastraIdentificadorSend) { return _dclienteRule.CadastraIdentificador(dCadastraIdentificadorSend); }

        /// <summary>
        /// Associa um identificador a um cpf/cnpj
        /// </summary>
        /// <returns></returns>
        public BaseResponse AssociaIdentificador(DAssociaIdentificadorSend dAssociaIdentificadorSend) { return _dclienteRule.DAssociaIdentificador(dAssociaIdentificadorSend); }

        /// <summary>
        /// Desassocia um identificador a um cpf/cnpj e um identificador
        /// </summary>
        /// <returns></returns>
        public BaseResponse DesassociaIdentificador(DDesassociaIdentificadorSend dDesassociaIdentificadorSend)
        {
            return _dclienteRule.DesassociaIdentificador(dDesassociaIdentificadorSend);
        }

        /// <summary>
        /// ws que trata e/ou verifica o cpf ou cnpj no Dyn365 e retorna os identificadores e matriculas vinculados a ele no SICOM
        /// </summary>
        /// <param name="listaUsuarioSend"></param>
        /// <returns></returns>
        public BaseResponse ListaUsuario(DListaUsuarioSend listaUsuarioSend) { return _dclienteRule.ListaUsuario(listaUsuarioSend); }

        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj no D365
        /// </summary>
        public BaseResponse ListaIdentificador(DListaIdentificadorSend dyn365ListaIdentificadorSend) { return _dclienteRule.ListaIdentificador(dyn365ListaIdentificadorSend); }

        /// <summary>
        /// Valida cpfCnpjDyn365
        /// </summary>
        public BaseResponse ValidaCpfCnpjDyn365(DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend) { return _dclienteRule.ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend); }

        /// <summary>
        /// Associa um identificador a um cpf/cnpj
        /// </summary>
        /// <returns></returns>
        public BaseResponse DAssociaIdentificador(DAssociaIdentificadorSend dAssociaIdentificadorSend) { return _dclienteRule.DAssociaIdentificador(dAssociaIdentificadorSend); }

        /// <summary>
        /// Retorna o Historico de faturas pagas do identificador
        /// </summary>
        /// <returns></returns>
        public BaseResponse DFaturas(DFaturasSend ConsultaConsumo) { return _dclienteRule.DFaturas(ConsultaConsumo); }

        /// <summary>
        /// Exibe as 12 ultimas faturas de um cliente
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        public BaseResponse DUltimasFaturas(DFaturasSend faturaSend) { return _dclienteRule.DUltimasFaturas(faturaSend); }

        /// <summary>
        /// retorna dados da certidão negativa
        /// </summary>
        /// <param name="dCertidaoNegativa"></param>
        /// <returns></returns>
        public BaseResponse CertidaoNegativa(DCertidaoNegativaSend dCertidaoNegativa) { return _dclienteRule.CertidaoNegativa(dCertidaoNegativa); }


        /// <summary>
        /// busca as localidades ativas do sistema de uma determinada empresa
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaLocalidade(DLocalidadeSend dLocalidadeSend) { return _dclienteRule.BuscaLocalidade(dLocalidadeSend); }

        /// <summary>
        /// busca os bairros de uma localidade
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaBairro(DBairroSend dBairroSend) { return _dclienteRule.BuscaBairro(dBairroSend); }

        /// <summary>
        /// busca os logradouros ativos de um bairro
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaLogradouro(DLogradouroSend dLogradouroSend) { return _dclienteRule.BuscaLogradouro(dLogradouroSend); }

        /// <summary>
        /// valida se endereço é coberto pela copasa
        /// </summary>
        /// <returns></returns>
        public BaseResponse ValidaEndereco(DEnderecoSend dEnderecoSend) { return _dclienteRule.ValidaEndereco(dEnderecoSend); }

        /// <summary>
        /// busca as pavimentações de um subtipo
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaPavimentacao(DPavimentacaoSend dPavimentacaoSend) { return _dclienteRule.BuscaPavimentacao(dPavimentacaoSend); }

        /// <summary>
        /// solicita serviço vazamento agua/esgoto
        /// </summary>
        /// <param name="dSolicitaServicoSend"></param>
        /// <returns></returns>
        public BaseResponse SolicitaServico(DSolicitaServicoSend dSolicitaServicoSend)
        {
            return _dclienteRule.SolicitaServico(dSolicitaServicoSend);
        }

        /// <summary>
        /// Cadastro e validação de emails para o envio de contas por email
        /// </summary>
        /// <param name="dContasEmailSend"></param>
        /// <returns></returns>
        public BaseResponse ContasPorEmail(DContasEmailSend dContasEmailSend)
        {
            return _dclienteRule.ContasPorEmail(dContasEmailSend);
        }

        /// <summary>
        /// Altera o status para envio de contas por email.
        /// </summary>
        /// <returns></returns>
        public BaseResponse ConfirmaContasEmail(DConfirmaEmailSend dConfirmaEmailSend)
        {
            return _dclienteRule.ConfirmaContasEmail(dConfirmaEmailSend);
        }

        /// <summary>
        /// Consulta dados de um usuário
        /// </summary>
        /// <param name="dValidaCpfCnpjSend"></param>
        /// <returns></returns>
        public BaseResponse ConsultaUsuario(DValidaCpfCnpjSend dValidaCpfCnpjSend) { return _dclienteRule.ConsultaUsuario(dValidaCpfCnpjSend); }

        /// <summary>
        /// Atualiza e-mail e telefones do usuário Copasa pelo Broker.
        /// </summary>
        /// <param name="usuarioCopasa">Objeto de Entrada.</param>
        /// <returns></returns>
        public BaseResponse AtualizacaoCadastral(UsuarioCopasaModel usuarioCopasa)
        {
            return _dclienteRule.AtualizacaoCadastral(usuarioCopasa, EnvironmentEnum.H);
        }

        /// <summary>
        /// Historico de consumo anexado com o codigo de baeeas do faturas/emdebito
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        public BaseResponse DHistoricoConsumoBarra(DFaturasSend faturaSend)
        {
            return _dclienteRule.DHistoricoConsumoBarra(faturaSend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dEnviaFaturaEmailSend"></param>
        /// <returns></returns>
        public BaseResponse EnviaFaturaEmail(DEnviaFaturaEmailSend dEnviaFaturaEmailSend)
        {
            return _dclienteRule.EnviaFaturaEmail(dEnviaFaturaEmailSend);
        }

        /// <summary>
        /// Lista de Status Fatura Email
        /// </summary>
        /// <param name="dListaStatusFaturaSend"></param>
        /// <returns></returns>
        public BaseResponse ListaStatusFaturaEmail(DListaStatusFaturaSend dListaStatusFaturaSend) { return _dclienteRule.ListaStatusFaturaEmail(dListaStatusFaturaSend); }

        /// <summary>
        /// solicita um servico de abertura ou cancelamento de falta de água de acordo com a situração da matricula
        /// </summary>
        /// <param name="verificaFaltaDaguaSend"></param>
        /// <returns></returns>
        public BaseResponse VerificaFaltaDagua(VerificaFaltaDaguaSend verificaFaltaDaguaSend) { return _dclienteRule.VerificaFaltaDagua(verificaFaltaDaguaSend); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geraProtocoloSend"></param>
        /// <returns></returns>
        public BaseResponse GeraProtocolo(GeraProtocoloSend geraProtocoloSend) { return _dclienteRule.GeraProtocolo(geraProtocoloSend); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historicoServicoSendApp"></param>
        /// <returns></returns>
        public BaseResponse GetHistoricoServico(DHistoricoServicoSendApp historicoServicoSendApp) { return _dclienteRule.GetHistoricoServico(historicoServicoSendApp); }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dHistoricoServicoDetalheSend"></param>
        /// <returns></returns>
        public BaseResponse GetHistoricoServicoDetalhe(DHistoricoServicoDetalheSend dHistoricoServicoDetalheSend) { return _dclienteRule.GetHistoricoServicoDetalhe(dHistoricoServicoDetalheSend); }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dComentariosSend"></param>
        /// <returns></returns>
        public BaseResponse GetComentarios(DComentariosSend dComentariosSend) { return _dclienteRule.GetComentarios(dComentariosSend); }

    }
}
