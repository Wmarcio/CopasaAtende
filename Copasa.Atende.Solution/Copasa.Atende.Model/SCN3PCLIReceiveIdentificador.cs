using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLIReceive - Dados básicos cliente - Usuario
    /// </summary>
    public class SCN3PCLIReceiveIdentificador : BaseModel
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        [Broker("identificador", 1, "N11")]
        public long identificador { get; set; }

        /// <summary>
        /// nome.
        /// </summary>
        [Broker("nome", 2, "A35")]
        public string nome { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Broker("email", 3, "A50")]
        public string email { get; set; }

        /// <summary>
        /// DDDResidenciaSicom.
        /// </summary>
        [Broker("DDDResidenciaSicom", 4, "A2")]
        [JsonIgnore]
        public string DDDResidenciaSicom { get; set; }

        /// <summary>
        /// TelefoneResidenciaSicom.
        /// </summary>
        [Broker("telefoneResidenciaSicom", 5, "N9")]
        [JsonIgnore]
        public int telefoneResidenciaSicom { get; set; }

        /// <summary>
        /// TelefoneResidencia.
        /// </summary>
        public string telefoneResidencia { get; set; }

        /// <summary>
        /// DDDComercialSicom.
        /// </summary>
        [Broker("DDDComercialSicom", 6, "A2")]
        [JsonIgnore]
        public string DDDComercialSicom { get; set; }

        /// <summary>
        /// TelefoneComercialSicom.
        /// </summary>
        [Broker("telefoneComercialSicom", 7, "N9")]
        [JsonIgnore]
        public int telefoneComercialSicom { get; set; }

        /// <summary>
        /// TelefoneComercial.
        /// </summary>
        public string telefoneComercial { get; set; }

        /// <summary>
        /// DDDCelular.
        /// </summary>
        [Broker("DDDCelularSicom", 8, "A2")]
        [JsonIgnore]
        public string DDDCelularSicom { get; set; }

        /// <summary>
        /// TelefoneCelularSicom.
        /// </summary>
        [Broker("telefoneCelularSicom", 9, "N9")]
        [JsonIgnore]
        public int telefoneCelularSicom { get; set; }

        /// <summary>
        /// TelefoneCelular.
        /// </summary>
        public string telefoneCelular { get; set; }

        /// <summary>
        /// Logradouro.
        /// </summary>
        public string logradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [Broker("tipoLogradouro", 10, "A2")]
        [JsonIgnore]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [Broker("nomeLogradouro", 11, "A40")]
        [JsonIgnore]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        [Broker("numeroImovel", 12, "N5")]
        public virtual int numeroImovel { get; set; }

        /// <summary>
        /// TipoComplementoImovel.
        /// </summary>
        [Broker("tipoComplementoImovel", 13, "A2")]
        [JsonIgnore]
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        [Broker("complementoImovel", 14, "A12")]
        public virtual string complementoImovel { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [Broker("bairro", 15, "A30")]
        public string bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [Broker("localidade", 16, "A30")]
        public string localidade { get; set; }

        /// <summary>
        /// UF.
        /// </summary>
        [Broker("UF", 17, "A2")]
        public virtual string UF { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>
        [Broker("CEP", 18, "A8")]
        public virtual string CEP { get; set; }

        /// <summary>
        /// DataAtualizacao.
        /// </summary>
        [Broker("dataAtualizacao", 19, "A10")]
        public string dataAtualizacao { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [Broker("codigoLocalidade", 20, "N9")]
        [JsonIgnore]
        public int codigoLocalidade { get; set; }

    }
}
