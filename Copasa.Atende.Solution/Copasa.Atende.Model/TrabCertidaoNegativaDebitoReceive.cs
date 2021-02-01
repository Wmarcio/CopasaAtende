using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabCertidaoNegativaDebitoReceive Dados para emissao da certidao negativa de débito
    /// </summary>
    public class TrabCertidaoNegativaDebitoReceive : BaseModel
    {
        /// <summary>
        /// Mensagem de Retorno.
        /// </summary>        
        public string MensagemRetorno { get; set; }

        /// <summary>
        /// Texto Corpo.
        /// </summary>
        public string TextoCorpo { get; set; }

        /// <summary>
        /// Local.
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Texto Débitos.
        /// </summary>
        public string TextoDebitos { get; set; }

        /// <summary>
        /// Texto Débitos.
        /// </summary>
        public string TextoParcelamentos { get; set; }

        /// <summary>
        /// Texto Débitos.
        /// </summary>
        public string TextoLancamentos { get; set; }

        //public List<CertidaoNegativaIdentificadorModel> Identificadores { get; set; }

        /// <summary>
        /// Texto do rodapé da página
        /// </summary>
        public string TextoRodape { get; set; }

        /// <summary>
        /// Texto complementar
        /// </summary>
        public string TextoComplementar { get; set; }

        /// <summary>
        /// Outro texto complementar
        /// </summary>
        public string TextoComplementar2 { get; set; }

        /// <summary>
        /// Campo para retornar mensagem caso existam mais de 10 débitos
        /// </summary>
        public string MultiplosDebitos { get; set; }
    }
}
