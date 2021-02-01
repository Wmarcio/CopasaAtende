using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISNFReceive - Nota fiscal fatura
    /// </summary>
    public class SCN6ISNFReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_NUM-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_IDENT-USUARIO")]
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// NomeCliente.
        /// </summary>
        [XmlElement("_NOME-CLIENTE")]
        public string nomeCliente { get; set; }

        /// <summary>
        /// NumeroFaturaSaida.
        /// </summary>
        [XmlElement("_NUM-FATURA-SAIDA")]
        public string numeroFaturaSaida { get; set; }

        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_NUM-CPF-CNPJ")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// NotasFiscais.
        /// </summary>
        [XmlElement("_TAB-NOTA-FISCAL")]
        public SCN6ISNFReceiveNotaFiscal[] notasFiscais { get; set; }
    }
}
