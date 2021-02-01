using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// ArquivoFTP.
    /// </summary>
    public class MPToken : BaseModel
    {
        /// <summary>
        /// NomeArquivo.
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// NomeArquivo.
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// NomeArquivo.
        /// </summary>
        public int expires_in { get; set; }
    }
}
