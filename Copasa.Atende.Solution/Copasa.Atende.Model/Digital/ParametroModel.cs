using Copasa.Atende.Model.Core;
using System;

namespace Copasa.Atende.Model.Digital
{

    /// <summary>
    /// Model representativo da tabela APP_PARAMETRO_ATENDE.
    /// </summary>
    [Serializable()]
    public class ParametroModel : BaseModel
    {
        /// <summary>
        /// IdParametro.
        /// </summary>
        public int IdParametro { get; set; }

        /// <summary>
        /// Data Inicío.
        /// </summary>
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data Fim.
        /// </summary>
        public DateTime DataFim { get; set; }

        /// <summary>
        /// Descricao Mensagem.
        /// </summary>
        public string DescricaoMensagem { get; set; }

        /// <summary>
        /// Flag Permissão  
        /// Se 0 permite utilizar o app
        /// Se 1 não permite
        /// </summary>
        public long FlagPermissao { get; set; }

        /// <summary>
        /// Versão.
        /// </summary>
        public string Versao { get; set; }

        /// <summary>
        /// Sistema Operacional - Apple ou Android
        /// </summary>
        public string SistemaOperacional { get; set; }
    }
}
