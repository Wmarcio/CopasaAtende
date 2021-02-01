using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Dyn365;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Cliente.
    /// </summary>
    public class ClienteFacade : IClienteFacade
    {
        private IClienteRule _clienteRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="clienteRule">IClienteRule.</param>
        public ClienteFacade(IClienteRule clienteRule)
        {
            _clienteRule = clienteRule;
        }

        /// <summary>
        /// Lista clientes de um CPF ou CNPJ ou Identificador.
        /// </summary>
        public BaseResponse SCN3PCLI(SCN3PCLISend sCN3PCLISend)
        {
            return _clienteRule.SCN3PCLI(sCN3PCLISend);
        }

        /// <summary>
        /// Informa identificador e retorna matriculas.
        /// </summary>
        public BaseResponse SCN6UEFI(long identificador)
        {
            return _clienteRule.SCN6UEFI(identificador);
        }

        /// <summary>
        /// Atualiza status para envio conta por email.
        /// </summary>
        public BaseResponse SCN6ISCE(SCN6ISCESend sCN6ISCESend)
        {
            return _clienteRule.SCN6ISCE(sCN6ISCESend);
        }

        /// <summary>
        /// Altera email e telefone.
        /// </summary>
        public BaseResponse SCN4ISAC(SCN4ISACSend sCN4ISACSend)
        {
            return _clienteRule.SCN4ISAC(sCN4ISACSend);
        }

        /*
        /// <summary>
        /// Lista matriculas e identificadores de um CPF ou CNPJ.
        /// </summary>
        public BaseResponse SCN3PCLIMatricula(long cpfCnpj)
        {
            return _clienteRule.SCN3PCLIMatricula(cpfCnpj);
        }
        */

        /// <summary>
        /// Lista matriculas e identificadores de um CPF ou CNPJ.
        /// </summary>
        public BaseResponse SCN4ISU1(SCN4ISU1Send sCN4ISU1Send)
        {
            return _clienteRule.SCN4ISU1(sCN4ISU1Send);
        }

        /// <summary>
        /// Altera vencimento fatura.
        /// </summary>
        public BaseResponse SCN6ISAV(SCN6ISAVSend sCN6ISAVSend)
        {
            return _clienteRule.SCN6ISAV(sCN6ISAVSend);
        }

        /// <summary>
        /// Consiste matrícula centralizadora.
        /// </summary>
        public BaseResponse SCN6ISCC(SCN6ISCCSend sCN6ISCCSend)
        {
            return _clienteRule.SCN6ISCC(sCN6ISCCSend);
        }

        /// <summary>
        /// Lista identificadores.
        /// </summary>
        public BaseResponse SCN4ISU1Identificadores(SCN4ISU1_IdentificadoresSend sCN4ISU1_IdentificadoresSend)
        {
            return _clienteRule.SCN4ISU1Identificadores(sCN4ISU1_IdentificadoresSend);
        }

        /// <summary>
        /// Lista nome de identificadores.
        /// </summary>
        public BaseResponse SCN4ISU1Nomes(SCN4ISU1_NomesSend sCN4ISU1_NomesSend)
        {
            return _clienteRule.SCN4ISU1Nomes(sCN4ISU1_NomesSend);
        }

        /// <summary>
        /// Histórico de consumo.
        /// </summary>
        public BaseResponse SCN5ISHC(SCN5ISHCSend sCN5ISHCSend)
        {
            return _clienteRule.SCN5ISHC(sCN5ISHCSend);
        }

        /// <summary>
        /// Certidão negativa de débito de um CPF ou CNPJ.
        /// </summary>
        public BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend)
        {
            return _clienteRule.SCN6ISCN(sCN6ISCNSend);
        }

        /// <summary>
        /// Lista Pontos serviço.
        /// </summary>
        public BaseResponse SCN3ISPS(SCN3ISPSSend sCN3ISPSSend)
        {
            return _clienteRule.SCN3ISPS(sCN3ISPSSend);
        }


        /// <summary>
        /// Busca matrícula pelo endereço.
        /// </summary>
        public BaseResponse SCN3ISMT(SCN3ISMTSend sCN3ISMTSend)
        {
            return _clienteRule.SCN3ISMT(sCN3ISMTSend);
        }

        /// <summary>
        /// Quitação anual de débito.
        /// </summary>
        public BaseResponse SCN6ISQA(SCN6ISQASend sCN6ISQASend)
        {
            return _clienteRule.SCN6ISQA(sCN6ISQASend);
        }

        /// <summary>
        /// Verifica se há falta d'água em um endereço
        /// </summary>
        public BaseResponse getSituacaoMatriculasEndereco(SCN3ISMTSend sCN3ISMTSend)
        {
            return _clienteRule.getSituacaoMatriculasEndereco(sCN3ISMTSend);
        }

        /// <summary>
        /// Lista Pontos serviço.
        /// </summary>
        public BaseResponse getSituacaoMatriculas(TrabPesquisaFaltaAguaSend situacaoMatriculaSend)
        {
            return _clienteRule.getSituacaoMatriculas(situacaoMatriculaSend);
        }

        /// <summary>
        /// Valida Usuário
        /// </summary>
        public BaseResponse validaUsuario(TrabValidaUsuarioSend trabValidaUsuarioSend)
        {
            return _clienteRule.validaUsuario(trabValidaUsuarioSend);
        }

        /// <summary>
        /// Lista dados de hidrômetro.
        /// </summary>
        public BaseResponse listaDadosHidrometro(SCN5IS01Send sCN5IS01Send)
        {
            return _clienteRule.listaDadosHidrometro(sCN5IS01Send);
        }
        
        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse CadastraUsuario(Dyn365CreatePortalUserSend createDyn365PortalUserSend)
        {
            return _clienteRule.CadastraUsuario(createDyn365PortalUserSend);
        }

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend)
        {
            return _clienteRule.AutenticaUsuario(authenticateDyn365UserSend);
        }

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AltualizaSenha(Dyn365ChangeUserPasswordSend changeDyn365UserPasswordSend)
        {
            return _clienteRule.AltualizaSenha(changeDyn365UserPasswordSend);
        }
        
        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365 informando CPF - senha .
        /// </summary>
        public BaseResponse AlteraSenha(Dyn365ChangeUserPasswordCpfSend changeDyn365UserPasswordCpfSend)
        {
            return _clienteRule.AlteraSenha(changeDyn365UserPasswordCpfSend);
        }
        
        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse RecuperaSenha(Dyn365RecoveryUserPasswordSend recoveryDyn365UserPasswordSend)
        {
            return _clienteRule.RecuperaSenha(recoveryDyn365UserPasswordSend);
        }

        /// <summary>
        /// UpdateDyn365PortalUserSend - Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaUsuario(Dyn365UpdatePortalUserSend updateDyn365PortalUserSend)
        {
            return _clienteRule.AtualizaUsuario(updateDyn365PortalUserSend);
        }

        /// <summary>
        /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AssociaIdentificador(Dyn365AssociateIdentifierXUserSend associateDyn365IdentifierXUserSend)
        {
            return _clienteRule.AssociaIdentificador(associateDyn365IdentifierXUserSend);
        }

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaStatusIdentificador(Dyn365ChangeControllerIdentifierStatusSend changeDyn365ControllerIdentifierStatusSend)
        {
            return _clienteRule.AtualizaStatusIdentificador(changeDyn365ControllerIdentifierStatusSend);
        }

        /// <summary>
        /// Verifica se identificador é uma pessoa jurídica
        /// </summary>
        public BaseResponse validaCNPJ(TrabValidaCNPJSend trabValidaCNPJSend)
        {
            return _clienteRule.validaCNPJ(trabValidaCNPJSend);
        }

        /// <summary>
        /// Obter Mensagem Informativa
        /// </summary>
        public BaseResponse ObterInformativo(TrabParametroSend entrada)
        {
            return _clienteRule.ObterInformativo(entrada);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticateDyn365UserSend"></param>
        public BaseResponse DAutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend) { 
           return _clienteRule.DAutenticaUsuario(authenticateDyn365UserSend);
  
        }

        /// <summary>
        /// Rotina para corrigir registros de protocolos no Dynamics365 que tenham algum erro
        /// </summary>
        public BaseResponse corrigirProtocolos()
        {
            return _clienteRule.corrigirProtocolos();
        }
    }
}
