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

namespace Copasa.Atende.Business.Interfaces
{

    /// <summary>
    /// Interface Rule - Dynamics 365 Cliente.
    /// </summary>
    public interface IDClienteRule
    {
        /// <summary>
        /// criação de protocolo.
        /// </summary>
        /// <param name="criaProtocoloSend"></param>
        /// <returns></returns>
        BaseResponse CriaProtocolo(DCriaProtocoloSend criaProtocoloSend);

        /// <summary>
        /// Cria um Novo Identificador no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse CadastraIdentificador(DCadastraIdentificadorSend dCadastraIdentificadorSend);

        /// <summary>
        /// consulta um identificador no D365, retornando seu id
        /// </summary>
        BaseResponse ConsultaIdentificador(DConsultaIdentificadorSend consultaIdentificadorSend);

        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj no D365
        /// </summary>
        BaseResponse ListaIdentificador(DListaIdentificadorSend dyn365ListaIdentificadorSend);

        /// <summary>
        /// Desassocia um identificador a um cpf/cnpj e um identificador
        /// </summary>
        /// <returns></returns>
        BaseResponse DesassociaIdentificador(DDesassociaIdentificadorSend dDesassociaIdentificadorSend);

        /// <summary>
        /// ws que trata e/ou verifica o cpf ou cnpj no Dyn365 e retorna os identificadores e matriculas vinculados a ele no SICOM
        /// </summary>
        /// <param name="listaUsuarioSend"></param>
        /// <returns></returns>
        BaseResponse ListaUsuario(DListaUsuarioSend listaUsuarioSend);

        /// <summary>
        /// Valida cpfCnpjDyn365
        /// </summary>
        BaseResponse ValidaCpfCnpjDyn365(DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend);

        /// <summary>
        /// Associa um identificador a um cpf/cnpj
        /// </summary>
        /// <returns></returns>
        BaseResponse DAssociaIdentificador(DAssociaIdentificadorSend dAssociaIdentificadorSend);

        /// <summary>
        /// Retorna a lista de faturas/consumo
        /// </summary>
        /// <returns></returns>
        BaseResponse DFaturas(DFaturasSend dFaturasSend);

        /// <summary>
        /// retorna dados da certidão negativa
        /// </summary>
        /// <param name="dCertidaoNegativa"></param>
        /// <returns></returns>
        BaseResponse CertidaoNegativa(DCertidaoNegativaSend dCertidaoNegativa);

        /// <summary>
        /// busca as localidades ativas do sistema de uma determinada empresa
        /// </summary>
        /// <returns></returns>
        BaseResponse BuscaLocalidade(DLocalidadeSend dLocalidadeSend);

        /// <summary>
        /// busca os bairros de uma localidade
        /// </summary>
        /// <returns></returns>
        BaseResponse BuscaBairro(DBairroSend dLocalidadeSend);

        /// <summary>
        /// busca de logradouros
        /// </summary>
        /// <param name="dLogradouroSend"></param>
        /// <returns></returns>
        BaseResponse BuscaLogradouro(DLogradouroSend dLogradouroSend);

        /// <summary>
        /// valida se endereço é coberto pela copasa
        /// </summary>
        /// <param name="dEnderecoSend"></param>
        /// <returns></returns>
        BaseResponse ValidaEndereco(DEnderecoSend dEnderecoSend);

        /// <summary>
        /// busca as pavimentações de um subtipo
        /// </summary>
        /// <returns></returns>
        BaseResponse BuscaPavimentacao(DPavimentacaoSend dPavimentacaoSend);

        /// <summary>
        /// solicita serviço vazamento agua/esgoto
        /// </summary>
        /// <param name="dSolicitaServicoSend"></param>
        /// <returns></returns>
        BaseResponse SolicitaServico(DSolicitaServicoSend dSolicitaServicoSend);

        ///// <summary>
        ///// atualiza o protocolo.
        ///// </summary>
        ///// <param name="dSolicitaServicoSend"></param>
        ///// <returns></returns>
        //BaseResponse CancelaFaltaDagua(DSolicitaServicoSend dSolicitaServicoSend);

        /// <summary>
        /// Cadastro e validação de emails para o envio de contas por email
        /// </summary>
        /// <param name="dContasEmailSend"></param>
        /// <returns></returns>
        BaseResponse ContasPorEmail(DContasEmailSend dContasEmailSend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dConfirmaEmailSend"></param>
        /// <returns></returns>
        BaseResponse ConfirmaContasEmail(DConfirmaEmailSend dConfirmaEmailSend);

        /// <summary>
        /// Consulta dados de um usuário
        /// </summary>
        /// <param name="dValidaCpfCnpjSend"></param>
        /// <returns></returns>
        BaseResponse ConsultaUsuario(DValidaCpfCnpjSend dValidaCpfCnpjSend);

        /// <summary>
        /// Atualiza e-mail e telefones do usuário Copasa pelo Broker.
        /// </summary>
        /// <param name="usuarioCopasa">Objeto de Entrada.</param>
        /// <param name="ambiente">Ambiente.</param>
        /// <returns></returns>
        BaseResponse AtualizacaoCadastral(UsuarioCopasaModel usuarioCopasa, EnvironmentEnum ambiente);

        /// <summary>
        /// Exibe as 12 ultimas faturas de um cliente
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        BaseResponse DUltimasFaturas(DFaturasSend faturaSend);

        /// <summary>
        /// Historico de consumo anexado com o codigo de baeeas do faturas/emdebito
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        BaseResponse DHistoricoConsumoBarra(DFaturasSend faturaSend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dEnviaFaturaEmailSend"></param>
        /// <returns></returns>
        BaseResponse EnviaFaturaEmail(DEnviaFaturaEmailSend dEnviaFaturaEmailSend);

        /// <summary>
        /// Lista de Status Fatura Email
        /// </summary>
        /// <param name="dListaStatusFaturaSend"></param>
        /// <returns></returns>
        BaseResponse ListaStatusFaturaEmail(DListaStatusFaturaSend dListaStatusFaturaSend);

        /// <summary>
        /// solicita um servico de abertura ou cancelamento de falta de água de acordo com a situração da matricula
        /// </summary>
        /// <param name="verificaFaltaDaguaSend"></param>
        /// <returns></returns>
        BaseResponse VerificaFaltaDagua(VerificaFaltaDaguaSend verificaFaltaDaguaSend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geraProtocoloSend"></param>
        /// <returns></returns>
        BaseResponse GeraProtocolo(GeraProtocoloSend geraProtocoloSend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historicoServicoSendApp"></param>
        /// <returns></returns>
        BaseResponse GetHistoricoServico(DHistoricoServicoSendApp historicoServicoSendApp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dComentarioSend"></param>
        /// <returns></returns>
        BaseResponse GetComentarios(DComentariosSend dComentarioSend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historicoServicoDetalheSend"></param>
        /// <returns></returns>
        BaseResponse GetHistoricoServicoDetalhe(DHistoricoServicoDetalheSend historicoServicoDetalheSend);
    }
}
