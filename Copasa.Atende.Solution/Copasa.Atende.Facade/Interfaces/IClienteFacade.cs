using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface Facade - Cliente.
    /// </summary>
    public interface IClienteFacade
    {
        /// <summary>
        /// Lista clientes de um CPF ou CNPJ ou Identificador.
        /// </summary>
        BaseResponse SCN3PCLI(SCN3PCLISend sCN3PCLISend);

        /// <summary>
        /// Informa identificador e retorna matriculas.
        /// </summary>
        BaseResponse SCN6UEFI(long identificador);

        /// <summary>
        /// Atualiza status para envio conta por email.
        /// </summary>
        BaseResponse SCN6ISCE(SCN6ISCESend sCN4FTE1Send);

        /// <summary>
        /// Altera email e telefone.
        /// </summary>
        BaseResponse SCN4ISAC(SCN4ISACSend sCN4ISACSend);

        /*
        /// <summary>
        /// Lista matriculas e identificadores de um CPF ou CNPJ.
        /// </summary>
        BaseResponse SCN3PCLIMatricula(long cpfCnpj);
        */

        /// <summary>
        /// Verifica se identificador é uma pessoa jurídica
        /// </summary>
        BaseResponse validaCNPJ(TrabValidaCNPJSend trabValidaCNPJSend);

        /// <summary>
        /// Lista matriculas e identificadores de um CPF ou CNPJ.
        /// </summary>
        BaseResponse SCN4ISU1(SCN4ISU1Send sCN4ISU1Send);

        /// <summary>
        /// Lista identificadores.
        /// </summary>
        BaseResponse SCN4ISU1Identificadores(SCN4ISU1_IdentificadoresSend sCN4ISU1_IdentificadoresSend);

        /// <summary>
        /// Lista nome de identificadores.
        /// </summary>
        BaseResponse SCN4ISU1Nomes(SCN4ISU1_NomesSend sCN4ISU1_NomesSend);

        /// <summary>
        /// Histórico de consumo.
        /// </summary>
        BaseResponse SCN5ISHC(SCN5ISHCSend sCN5ISHCSend);

        /// <summary>
        /// Certidão negativa de débito de um CPF ou CNPJ.
        /// </summary>
        BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend);

        /// <summary>
        /// Lista Pontos serviço.
        /// </summary>
        BaseResponse SCN3ISPS(SCN3ISPSSend sCN3ISPSSend);

        /// <summary>
        /// Busca matrícula pelo endereço.
        /// </summary>
        BaseResponse SCN3ISMT(SCN3ISMTSend sCN3ISMTSend);

        /// <summary>
        /// Quitação anual de débito.
        /// </summary>
        BaseResponse SCN6ISQA(SCN6ISQASend sCN6ISQASend);

        /// <summary>
        /// Verifica se há falta d'água em um endereço
        /// </summary>
        BaseResponse getSituacaoMatriculasEndereco(SCN3ISMTSend sCN3ISMTSend);

        /// <summary>
        /// Lista Pontos serviço.
        /// </summary>
        BaseResponse getSituacaoMatriculas(TrabPesquisaFaltaAguaSend situacaoMatriculaSend);

        /// <summary>
        /// Altera vencimento fatura.
        /// </summary>
        BaseResponse SCN6ISAV(SCN6ISAVSend sCN6ISAVSend);

        /// <summary>
        /// Consiste matrícula centralizadora.
        /// </summary>
        BaseResponse SCN6ISCC(SCN6ISCCSend sCN6ISCCSend);

        /// <summary>
        /// Valida Usuário
        /// </summary>
        BaseResponse validaUsuario(TrabValidaUsuarioSend trabValidaUsuarioSend);

        /// <summary>
        /// Lista dados de hidrômetro.
        /// </summary>
        BaseResponse listaDadosHidrometro(SCN5IS01Send sCN5IS01Send);

        ///// <summary>
        ///// Cria um Novo Identificador no Microsoft Dynamics 365.
        ///// </summary>
        //BaseResponse CadastraIdentificador(DCadastraIdentificadorSend createDyn365IdentifierSend);

        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse CadastraUsuario(Dyn365CreatePortalUserSend createDyn365PortalUserSend);

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend);

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AltualizaSenha(Dyn365ChangeUserPasswordSend changeDyn365UserPasswordSend);

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AlteraSenha(Dyn365ChangeUserPasswordCpfSend changeDyn365UserPasswordCpfSend);


        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse RecuperaSenha(Dyn365RecoveryUserPasswordSend recoveryDyn365UserPasswordSend);

        /// <summary>
        /// UpdateDyn365PortalUserSend - Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AtualizaUsuario(Dyn365UpdatePortalUserSend updateDyn365PortalUserSend);

        /// <summary>
        /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AssociaIdentificador(Dyn365AssociateIdentifierXUserSend associateDyn365IdentifierXUserSend);

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        BaseResponse AtualizaStatusIdentificador(Dyn365ChangeControllerIdentifierStatusSend changeDyn365ControllerIdentifierStatusSend);

        /// <summary>
        /// Obter Mensagem Informativa
        /// </summary>
        BaseResponse ObterInformativo(TrabParametroSend entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticateDyn365UserSend"></param>
        /// <returns></returns>
        BaseResponse DAutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend);

        /// <summary>
        /// Rotina para corrigir registros de protocolos no Dynamics365 que tenham algum erro
        /// </summary>
        BaseResponse corrigirProtocolos();
    }
}
