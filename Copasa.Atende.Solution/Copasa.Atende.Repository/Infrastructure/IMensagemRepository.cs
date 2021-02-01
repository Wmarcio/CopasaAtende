using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface - MensagemRepository.
    /// </summary>
    public interface IMensagemRepository
    {
        /// <summary>
        /// Gera mensagem
        /// </summary>
        string geraMensagem(string idMensagem);

         /// <summary>
        /// Gera mensagem
        /// </summary>
        string geraMensagemComDataPrazoEProtocolo(int idMensagem ,string dataPrevisao , bool usuarioInterno,BaseModel baseModel);

        /// <summary>
        /// Gera mensagem
        /// </summary>
        string geraMensagemComDataPrazo(int idMensagem, string dataPrevisao, BaseModel baseModel);

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        string parseMensagem(string idMensagem);

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        string parseMensagem(string idMensagem, BaseModel model);

        /// <summary>
        /// Parse de codigo de mensagem
        /// </summary>
        string parseMensagem(string idMensagem, bool usuarioInterno, BaseModel model);

        /// <summary>
        /// Trata mensagem
        /// </summary>
        string trataMensagem(string descricaoMensagem, BaseModel model);

        /// <summary>
        /// Trata mensagem
        /// </summary>
        string trataMensagem(string descricaoMensagem, List<BaseModel> models);

        /// <summary>
        /// Gera mensagem para codigos de comprimento igual a 1.
        /// </summary>
        string GeraMensagemCustom(string idMensagem);
    }
}
