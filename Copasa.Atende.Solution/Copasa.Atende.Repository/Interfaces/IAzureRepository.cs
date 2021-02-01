using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface IAzureRepository - API Azure
    /// </summary>
    public interface IAzureRepository
    {
        ///// <summary>
        ///// Cria um Novo Identificador no Microsoft Dynamics 365.
        ///// <param name="_createDyn365IdentifierSend">CreateDyn365IdentifierSend</param>
        ///// </summary>
      //  BaseModelAzureCopaUserReceive CreateDyn365Identifier(DCadastraIdentificadorSend _createDyn365IdentifierSend);

        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_createDyn365PortalUserSend">CreateDyn365PortalUserSend</param>
        BaseModelAzureCopaUserReceive CreateDyn365PortalUser(Dyn365CreatePortalUserSend _createDyn365PortalUserSend);

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_authenticateDyn365UserSend">AuthenticateDyn365UserSend</param>
        BaseModelAzureCopaUserReceive AuthenticateDyn365User(Dyn365AuthenticateUserSend _authenticateDyn365UserSend);

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_changeDyn365UserPasswordSend">ChangeDyn365UserPasswordSend</param>
        BaseModelAzureCopaUserReceive ChangeDyn365UserPassword(Dyn365ChangeUserPasswordSend _changeDyn365UserPasswordSend);

        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_recoveryDyn365UserPasswordSend">RecoveryDyn365UserPasswordSend</param>
        BaseModelAzureCopaUserReceive RecoveryDyn365UserPassword(Dyn365RecoveryUserPasswordSend _recoveryDyn365UserPasswordSend);

        /// <summary>
        /// Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_updateDyn365PortalUserSend">UpdateDyn365PortalUserSend</param>
        BaseModelAzureCopaUserReceive UpdateDyn365PortalUser(Dyn365UpdatePortalUserSend _updateDyn365PortalUserSend);

        /// <summary>
        /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_associateDyn365IdentifierXUserSend">AssociateDyn365IdentifierXUserSend</param>
        BaseModelAzureCopaUserReceive AssociateDyn365IdentifierXUser(Dyn365AssociateIdentifierXUserSend _associateDyn365IdentifierXUserSend);

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_changeDyn365ControllerIdentifierStatusSend">ChangeDyn365ControllerIdentifierStatusSend</param>
        BaseModelAzureCopaUserReceive ChangeDyn365ControllerIdentifierStatus(Dyn365ChangeControllerIdentifierStatusSend _changeDyn365ControllerIdentifierStatusSend);
    }
}
