using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1ReceiveMatriculas Busca dados do cliente
    /// </summary>
    public class SCN4ISU1ReceiveMatriculas : SCN4ISU1ReceiveMatriculasUsuarios
    {
        /// <summary>
        /// TelefoneCelular.
        /// </summary>
        public string telefoneCelular { get; set; }

        /// <summary>
        /// TelefoneComercial.
        /// </summary>
        public string telefoneComercial { get; set; }

        /// <summary>
        /// TelefoneResidencia.
        /// </summary>
        public string telefoneResidencia { get; set; }
        /// <summary>
        /// Logradouro.
        /// </summary>
        public string logradouro { get; set; }

        /// <summary>
        /// CpfCnpj.
        /// </summary>
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Associacao.
        /// </summary>
        public string associacao { get; set; }

        /// <summary>
        /// TemInterrupcaoAgua.
        /// </summary>
        public string temInterrupcaoAgua { get; set; }
        
    }
}
