using System;
using System.Data;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace Copasa.Atende.WebService.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public class Funcoes
    {
        public string serverMapPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverMapPath"></param>
        public Funcoes(string serverMapPath)
        {
            this.serverMapPath = serverMapPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string byteArrayToString(byte[] b)
        {
            return Encoding.UTF8.GetString(b, 0, b.Length);
        }

        public byte[] stringToByteArray(string StringToConvert)
        {
            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }

        public void gravaArquivo(string nomeArquivo, string conteudo)
        {
            gravaArquivo(nomeArquivo, stringToByteArray(conteudo));
        }

        public void gravaArquivo(string nomeArquivo, byte[] conteudo)
        {
            BinaryWriter Writer = null;
            Writer = new BinaryWriter(File.OpenWrite(serverMapPath + nomeArquivo));
            Writer.Write(conteudo);
            Writer.Flush();
            Writer.Close();
        }

        public StreamReader readStreamFileSystem(string caminhoArquivo)
        {
            return new System.IO.StreamReader(caminhoArquivo);
        }

        public string readStringFileSystem(string caminhoArquivo)
        {
            System.IO.StreamReader myFile = new System.IO.StreamReader(caminhoArquivo);
            return myFile.ReadToEnd();
        }

        public void gravaLogListeners(string s)
        {
            try
            {
                DateTime now = DateTime.Now;
                string LogFilePath;
                LogFilePath = serverMapPath + "Sicom\\logListeners.txt";
                // Create a temporary file, and put some data into it.
                using (FileStream fs = File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(now + " " + s + "\r\n");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception) { }
        }

        public void gravaLog(string s)
        {
            try
            {
                DateTime now = DateTime.Now;
                string LogFilePath;
                LogFilePath = serverMapPath + "log.txt";
                // Create a temporary file, and put some data into it.
                using (FileStream fs = File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(now + " " + s + "\r\n");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex) { }
        }

        public void geraXsd(DataSet ds, string nomeXsd)
        {
            ds.WriteXmlSchema(nomeXsd + ".xsd");
        }

        public string repeat(string caracter, int numTabs)
        {
            StringBuilder sb = new StringBuilder();
            for (uint i = 0; i < numTabs; i++)
                sb.Append(caracter);

            return sb.ToString();
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        /*
        public byte[] compress(byte[] bytes)
        {
            MemoryStream fileToCompress = new MemoryStream();
            using (GZipStream compressionStream = new GZipStream(fileToCompress, CompressionMode.Compress))
            {
                compressionStream.Write(bytes, 0, bytes.Length);
            }
            byte[] b = fileToCompress.ToArray();

            return b;
        }
        */

        public byte[] compress(byte[] bytes)
        {
            MemoryStream fileToCompress2 = new MemoryStream();

            DeflateStream compressStream = new DeflateStream(fileToCompress2, CompressionMode.Compress, true);
            compressStream.Write(bytes, 0, bytes.Length);
            compressStream.Close();
            byte[] retVal = new byte[fileToCompress2.Length];
            fileToCompress2.Position = 0L;
            fileToCompress2.Read(retVal, 0, retVal.Length);
            fileToCompress2.Close();
            compressStream.Close();
            return retVal;
        }

        public byte[] decompress2(byte[] input)
        {
            byte[] buffer;

            using (var compressStream = new MemoryStream(input))
            using (var decompressor = new DeflateStream(compressStream, CompressionMode.Decompress))
            {
                int length = 1024 * 4;  // get file length
                buffer = new byte[1024 * 4];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read
                return ReadToEnd(decompressor);
            }
        }
    }
}
