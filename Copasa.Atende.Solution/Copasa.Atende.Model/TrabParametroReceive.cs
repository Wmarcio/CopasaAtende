using Copasa.Atende.Model.Core;
using System;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabParametroReceive - Busca Mensagem Informativa
    /// </summary>
    public class TrabParametroReceive : BaseModelReceive
    {
        /// <summary>
        /// Data Inicío
        /// </summary>
        public string DataInicio { get; set; }

        /// <summary>
        /// Data Fim
        /// </summary>
        public string DataFim { get; set; }

        /// <summary>
        /// Descricao Mensagem
        /// </summary>
        public string DescricaoMensagem { get; set; }

        /// <summary>
        /// Flag Permissão  
        /// Se 0 permite utilizar o app
        /// Se 1 não permite
        /// </summary>
        public string FlagPermissao { get; set; }

        /// <summary>
        /// Versão
        /// </summary>
        public string Versao { get; set; }

        /// <summary>
        /// Sistema Operacional - Apple ou Android
        /// </summary>
        public string SistemaOperacional { get; set; }
    }
}
