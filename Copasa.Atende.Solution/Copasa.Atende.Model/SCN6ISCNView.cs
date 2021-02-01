using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNView - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNView : BaseModel
    {
        /// <summary>
        /// Mensagem de Retorno.
        /// </summary>        
        public string mensagemRetorno { get; set; }

        /// <summary>
        /// Texto Corpo.
        /// </summary>
        public string textoCorpo { get; set; }

        /// <summary>
        /// Local.
        /// </summary>
        public string local { get; set; }

        /// <summary>
        /// Deferido ou indeferido
        /// </summary>
        public string deferimento { get; set; }

        /// <summary>
        /// Texto Débitos.
        /// </summary>
        public string textoDebitos { get; set; }

        /// <summary>
        /// Texto Débitos a Vencer.
        /// </summary>
        public string textoDebitosVencer { get; set; }

        /// <summary>
        /// Texto Parcelamentos.
        /// </summary>
        public string textoParcelamentos { get; set; }

        /// <summary>
        /// Texto Lançamentos.
        /// </summary>
        public string textoLancamentos { get; set; }

        /// <summary>
        /// Lista de matrículas e endereços
        /// </summary>
        public List<SCN6ISCNViewIdentificador> identificadores { get; set; }

        /// <summary>
        /// Texto do rodapé da página
        /// </summary>
        public string textoRodape { get; set; }

        /// <summary>
        /// Texto complementar
        /// </summary>
        public string textoComplementar { get; set; }

        /// <summary>
        /// Texto complementar 1
        /// </summary>
        public string textoComplementar1 { get; set; }

        /// <summary>
        /// TextoComplementar 2
        /// </summary>
        public string textoComplementar2 { get; set; }
    }
}
