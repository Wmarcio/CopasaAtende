using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model.Digital
{
    /// <summary>
    /// Representação APP_Titulo.
    /// </summary>
    public class TituloModel : BaseModel
    {
        /// <summary>
        /// ID da Título.
        /// </summary>
        public int IdTitulo { get; set; }

        /// <summary>
        /// Descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Recupera e grava IsAtivo.
        /// </summary>
        public bool IsAtivo { get; set; }

        /// <summary>
        /// Informação.
        /// </summary>
        public string Informacao { get; set; }
        
        /// <summary>
        /// Coleção de relacionamentos.
        /// </summary>
        public virtual List<TelaTituloModel> TelaTitulos { get; set; }
    }
}
