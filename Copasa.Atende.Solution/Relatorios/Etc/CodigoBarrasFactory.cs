using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Etc
{
    public class CodigoBarrasFactory
    {
        private int _altura = 200;
        private int _larguraBarra = 4;
        /// <summary>
        /// Retorna stream da imagem de código de barras do tipo Interleaved 2 of 5 em formato JPG
        /// </summary>
        public MemoryStream GerarImagemEmMemoria(string numero)
        {
            return GerarImagemEmMemoria(numero, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Retorna stream da imagem de código de barras do tipo Interleaved 2 of 5 no formato fornecido
        /// </summary>
        public MemoryStream GerarImagemEmMemoria(string numero, ImageFormat formato)
        {
            Bitmap bitmap = GeraBitmap(GeraArrayBytes(numero));
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, formato);
            long posicao = memoryStream.Position;
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Retorna stream da imagem de código de barras do tipo Interleaved 2 of 5 no formato fornecido
        /// </summary>
        public Bitmap GerarImagem(string numero)
        {
            Bitmap bitmap = GeraBitmap(GeraArrayBytes(numero));
            return bitmap;
        }

        /// <summary>
        /// Salva arquivo em formato JPG do código de barras do tipo Interleaved 2 of 5
        /// </summary>
        public bool SalvarImagem(string numero, string local)
        {
            return SalvarImagem(numero, local, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Salva arquivo no formato fornecido do código de barras do tipo Interleaved 2 of 5
        /// </summary>
        public bool SalvarImagem(string numero, string local, ImageFormat formato)
        {
            Bitmap bitmap = GeraBitmap(GeraArrayBytes(numero));
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(local);
            return true;
        }

        /// <summary>
        /// Gera a lista de bytes para montar a imagem do código de barras a partir do número informado
        /// </summary>
        private List<byte> GeraArrayBytes(string numeroCodigoBarra)
        {
            numeroCodigoBarra = numeroCodigoBarra.Trim();
            List<byte> bytes = new List<byte>();
            if (ValidaCodigo(numeroCodigoBarra))
            {
                int[][] tabelaEncoding = new int[10][];
                tabelaEncoding[0] = new int[5] { 1, 1, 2, 2, 1 };
                tabelaEncoding[1] = new int[5] { 2, 1, 1, 1, 2 };
                tabelaEncoding[2] = new int[5] { 1, 2, 1, 1, 2 };
                tabelaEncoding[3] = new int[5] { 2, 2, 1, 1, 1 };
                tabelaEncoding[4] = new int[5] { 1, 1, 2, 1, 2 };
                tabelaEncoding[5] = new int[5] { 2, 1, 2, 1, 1 };
                tabelaEncoding[6] = new int[5] { 1, 2, 2, 1, 1 };
                tabelaEncoding[7] = new int[5] { 1, 1, 1, 2, 2 };
                tabelaEncoding[8] = new int[5] { 2, 1, 1, 2, 1 };
                tabelaEncoding[9] = new int[5] { 1, 2, 1, 2, 1 };

                int tamanhoBranco = 8;
                // Branco
                for (int i = 0; i < tamanhoBranco; i++)
                    bytes.Add(0);

                // Start caracter
                bytes.Add(1);
                bytes.Add(0);
                bytes.Add(1);
                bytes.Add(0);


                byte ultimoByte = 1;
                for (int i = 1; i < numeroCodigoBarra.ToCharArray().Length; i += 2)
                {
                    int[] valorNumero = new int[2];
                    valorNumero[0] = int.Parse(numeroCodigoBarra.ToCharArray()[i - 1].ToString());
                    valorNumero[1] = int.Parse(numeroCodigoBarra.ToCharArray()[i].ToString());
                    for (int z = 0; z < 5; z++)
                    {
                        for (int w = 0; w < valorNumero.Length; w++)
                        {
                            int tamanhoBarra = tabelaEncoding[valorNumero[w]][z];
                            for (short y = 0; y < tamanhoBarra; y++)
                            {
                                bytes.Add(ultimoByte);
                            }
                            if (ultimoByte == 1)
                                ultimoByte = 0;
                            else
                                ultimoByte = 1;
                        }
                    }
                }

                // Stop caracter
                bytes.Add(1);
                bytes.Add(1);
                bytes.Add(0);
                bytes.Add(1);

                // Branco
                for (int i = 0; i < tamanhoBranco; i++)
                    bytes.Add(0);
            }
            else
            {
                // Imagem em branco porque o código não é numérico
                for (int i = 0; i < 200; i++)
                    bytes.Add(0);
            }
            return bytes;
        }

        /// <summary>
        /// Gera o Bitmap a partir da lista de bytes 
        /// </summary>
        private System.Drawing.Bitmap GeraBitmap(List<byte> bytes)
        {
            Color[] _cor = new Color[2];
            _cor[1] = Color.Black;
            _cor[0] = Color.White;
            byte[] arrayBytes = bytes.ToArray();
            int width = arrayBytes.Length * _larguraBarra;
            int height = _altura;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < arrayBytes.Length; i++)
                    using (SolidBrush myBrush = new SolidBrush(_cor[arrayBytes[i]]))
                    {
                        graphics.FillRectangle(myBrush, new Rectangle(i * _larguraBarra, 0, _larguraBarra, _altura));
                    }
            }
            return bitmap;
        }


        private bool ValidaCodigo(string codigo)
        {
            for (int i = 0; i < codigo.Length; i++)
            {
                try
                {
                    int.Parse(codigo.Substring(i, 1));
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}