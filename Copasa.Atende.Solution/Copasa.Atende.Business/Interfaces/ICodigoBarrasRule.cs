using System.Drawing.Imaging;
using System.IO;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - Código de barras.
    /// </summary>
    public interface ICodigoBarrasRule
    {
        /// <summary>
        /// Retorna stream da imagem de código de barras do tipo Interleaved 2 of 5 em formato JPG
        /// </summary>
        MemoryStream GerarImagemEmMemoria(string numero);

        /// <summary>
        /// Retorna stream da imagem de código de barras do tipo Interleaved 2 of 5 no formato fornecido
        /// </summary>
        MemoryStream GerarImagemEmMemoria(string numero, ImageFormat formato);

        /// <summary>
        /// Salva arquivo em formato JPG do código de barras do tipo Interleaved 2 of 5
        /// </summary>
        bool SalvarImagem(string numero, string local);

        /// <summary>
        /// Salva arquivo no formato fornecido do código de barras do tipo Interleaved 2 of 5
        /// </summary>
        bool SalvarImagem(string numero, string local, ImageFormat formato);
    }
}
