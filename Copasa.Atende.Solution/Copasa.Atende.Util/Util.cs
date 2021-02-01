using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Copasa.Atende.Util
{
    /// <summary>
    /// Classe Util
    /// </summary>
    public static class Util
    {
        public static string trataTextoIBM(string texto)
        {
            try
            {
                if (texto != null)
                {
                    StringBuilder sb = new StringBuilder();
                    char anterior = 'x';
                    char[] caracteres = texto.ToUpper().ToCharArray();
                    foreach (char caracter in caracteres)
                    {
                        char caracterTratado = trataCaracter(caracter);
                        if (anterior != ' ' || (anterior == ' ' && caracterTratado != ' '))
                            sb.Append(caracterTratado);
                        anterior = caracterTratado;
                    }
                    /*
                    for (int i = 0; i < texto.Trim().Length; i++)
                    {
                        char caracter = trataCaracter(texto.Substring(i, 1));
                        if (anterior != ' ' || (anterior == ' ' && caracter != ' '))
                            sb.Append(caracter);
                        anterior = caracter;
                    }
                    */
                    return sb.ToString();
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static char trataCaracter(string texto)
        {
            return trataCaracter(texto.ToUpper()[0]);
        }

        public static char trataCaracter(char caracter)
        {
            switch (caracter)
            {
                case ('À'):
                    return 'A';
                case ('Á'):
                    return 'A';
                case ('Ã'):
                    return 'A';
                case ('Â'):
                    return 'A';
                case ('É'):
                    return 'E';
                case ('È'):
                    return 'E';
                case ('Ê'):
                    return 'E';
                case ('Ì'):
                    return 'I';
                case ('Í'):
                    return 'I';
                case ('Î'):
                    return 'I';
                case ('Ó'):
                    return 'O';
                case ('Õ'):
                    return 'O';
                case ('Ò'):
                    return 'O';
                case ('Ô'):
                    return 'O';
                case ('Ú'):
                    return 'U';
                case ('Û'):
                    return 'U';
                case ('Ù'):
                    return 'U';
                case ('Ç'):
                    return 'C';
                case ('/'):
                    return ' ';
                case (':'):
                    return ' ';
                case ('\''):
                    return ' ';
                case ('\"'):
                    return ' ';
                case ('{'):
                    return ' ';
                case ('}'):
                    return ' ';
                case ('['):
                    return ' ';
                case (']'):
                    return ' ';
            }
            int codigo = Convert.ToInt32(caracter);

            if (codigo > 126 || codigo < 35)
                return ' ';
            else
                return caracter;
        }

        public static string retiraCaracteresMascara(string cpfCnpj)
        {
            char[] chars = cpfCnpj.ToCharArray();
            List<char> retorno = new List<char>();
            foreach (char cr in chars)
            {
                long r;
                if (long.TryParse(cr.ToString(), out r))
                    retorno.Add(cr);
            }
            return new string(retorno.ToArray());
        }

        public static string formataNumero(string parm, int tamanho)
        {
            parm = parm.Trim();
            int zeros = tamanho - parm.Length;
            if (zeros > 0)
            {
                return new String('0', zeros) + parm;
            }
            else
                return parm;
        }

        public static bool isInArray(string[] strArray, string key)
        {
            for (int i = 0; i <= strArray.Length - 1; i++)
                if (strArray[i].Equals(key))
                    return true;
            return false;
        }
        public static bool isInArray(int[] strArray, int key)
        {
            for (int i = 0; i <= strArray.Length - 1; i++)
                if (strArray[i] == key)
                    return true;
            return false;
        }

        public static int parseInt(string valor)
        {
            int retorno = 0;
            try
            {
                retorno = int.Parse(valor);
            }
            catch (Exception e)
            {
                string erro = e.Message;
            }
            return retorno;
        }

        /// <summary>
        /// Colocar primeira letra dos nomes em maiúsculo e o restante munúsculo
        /// </summary>
        public static string formataTamanhoLetrasNomes(string entrada)
        {
            StringBuilder retorno = new StringBuilder();
            if (entrada != null && entrada.Length > 0)
            {
                retorno.Append(entrada.Substring(0, 1).ToUpper());
                for (int i = 1; i < entrada.Length; i++)
                {
                    if (" ".Equals(entrada.Substring(i - 1, 1)))
                        retorno.Append(entrada.Substring(i, 1).ToUpper());
                    else
                        retorno.Append(entrada.Substring(i, 1).ToLower());
                }
            }
            return retorno.ToString();
        }

        public static string InsereMascaraCpfCnpj(string cpfCnpj)
        {
            if (!string.IsNullOrEmpty(cpfCnpj))
            {
                ////insere máscara no cpf ou cnpj
                //if (cpfCnpj.Length == 11)
                //{

                //    cpfCnpj = $"{cpfCnpj.Substring(0, 3)}.{cpfCnpj.Substring(3, 3)}." +
                //        $"{cpfCnpj.Substring(6, 3)}-{cpfCnpj.Substring(9, 2)}";
                //}
                //else if (cpfCnpj.IndexOf('.') == -1)
                //{
                //    cpfCnpj = $"{cpfCnpj.Substring(0, 2)}.{cpfCnpj.Substring(2, 3)}.{cpfCnpj.Substring(5, 3)}" +
                //        $"/{cpfCnpj.Substring(8, 4)}-{cpfCnpj.Substring(12, 2)}";
                //}

                //insere máscara no cpf ou cnpj
                if (cpfCnpj.Length <= 11)
                {
                    cpfCnpj = cpfCnpj.PadLeft(11, '0');
                    cpfCnpj = $"{cpfCnpj.Substring(0, 3)}.{cpfCnpj.Substring(3, 3)}." +
                        $"{cpfCnpj.Substring(6, 3)}-{cpfCnpj.Substring(9, 2)}";
                }
                else
                if (cpfCnpj.Length > 11 && cpfCnpj.Length <= 14)
                {
                    cpfCnpj = cpfCnpj.PadLeft(14, '0');
                    cpfCnpj = $"{cpfCnpj.Substring(0, 2)}.{cpfCnpj.Substring(2, 3)}.{cpfCnpj.Substring(5, 3)}" +
                        $"/{cpfCnpj.Substring(8, 4)}-{cpfCnpj.Substring(12, 2)}";

                }


            }

            return cpfCnpj;
        }

    }
}
