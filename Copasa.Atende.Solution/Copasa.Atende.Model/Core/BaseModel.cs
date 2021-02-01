using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using Copasa.Util;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Classe Base das Entidades 
    /// </summary>
    [Serializable()]
    public class BaseModel
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public BaseModel()
        {
            Type objtype = GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                if ("String".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(this, "");
                }
                else if ("Int32".Equals(p.PropertyType.Name) || "Int64".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(this, 0);
                }
                else if ("List`1".Equals(p.PropertyType.Name))
                {
                    object objectModelo = Activator.CreateInstance(p.PropertyType);
                    objtype.GetProperty(p.Name).SetValue(this, objectModelo);
                }
            }

        }

        /*
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descricao 
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data Registro
        /// </summary>
        public DateTime? DtRegistro { get; set; }

        /// <summary>
        /// Num MatriculaEmpregado
        /// </summary>
        public int? NuMatriculaEmpregado { get; set; }
        */

        /// <summary>
        /// Transforma json em BaseModel
        /// </summary>
        public static BaseModel jsonToBaseModel(string json, BaseModel obj)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(obj.GetType());
                BaseModel baseModel = (BaseModel)deserializer.ReadObject(ms);
                return baseModel;
            }

        }

        /// <summary>
        /// Retorna retorna valores de um baseModel
        /// </summary>
        public List<string> getValores()
        {
            List<string> retorno = new List<string>();
            return getValores(this, retorno);
        }

        /// <summary>
        /// Retorna nomes dos campos
        /// </summary>
        public List<string> getNomeCampos()
        {
            Type _type = GetType();
            List<string> nomesCampos = new List<string>();
            foreach (PropertyInfo p in _type.GetProperties())
            {
                nomesCampos.Add(p.PropertyType.Name);
            }
            return nomesCampos;
        }

        /// <summary>
        /// Retorna retorna valores de um baseModel
        /// </summary>
        public string getIdDyn365Name()
        {
            Type objtype = GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                        return p.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// Retorna o  nome do identificador 
        /// </summary>
        public string getDyn365IdCopasaName()
        {
            Type objtype = GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                        return p.Name;
                }
            }
            return null;
        }

        /// <summary>
        /// Retorna o nome do identificador no D365
        /// </summary>
        public string getDyn365IdCopasaNameDynamics()
        {
            Type objtype = GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                        return ((Dyn365IdCopasaAttribute)at).Name;
                }
            }
            return null;
        }

        /// <summary>
        /// Validar se os dados contidos no baseModel estão corretos
        /// </summary>
        public bool validar(out string mensagemErro)
        {
            //string mensagemErro;
            return validar(this,out mensagemErro);
        }

        /// <summary>
        /// Validar se os dados contidos no baseModel estão corretos
        /// </summary>
        private bool validar(BaseModel baseModel,out string mensagemErro)
        {
            bool retorno = true;
            mensagemErro = "";
            Type tipoBaseModel = baseModel.GetType();            
            foreach (PropertyInfo p in tipoBaseModel.GetProperties())
            {
                ISAttribute isAttribute = null;
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("ISAttribute".Equals(at.GetType().Name))
                        isAttribute = (ISAttribute)at;
                }

                var tipo = p.PropertyType;
                if (tipo.FullName.Contains("[]"))
                {                    
                    object[] array = (object[])p.GetValue(this);
                    if (array != null && array.Length > 0)
                    {
                        tipo = array[0].GetType();
                        for (int i = 0; i < array.Length; i++)
                        {
                            var valor = array[i];
                            if (valor == null)
                            {
                                if (tipo == typeof(string))
                                    array[i] = "";
                                else if (tipo == typeof(int) || tipo == typeof(long))
                                    array[i] = 0;
                            }
                            else
                            {
                                if (tipo == typeof(string))
                                {
                                    if ("STRING".Equals(((string)valor).ToUpper()))
                                    {
                                        array[i] = "";
                                    }
                                    else
                                    {
                                        if (isAttribute != null)
                                        {
                                            string texto = trataTextoIBM(((string)valor).Trim());
                                            if ("N".Equals(isAttribute.Tipo.ToUpper()))
                                            {
                                                texto = retiraCaracteresMascara(texto);
                                                long num = 0;
                                                if (!"".Equals(texto) && !long.TryParse(texto, out num))
                                                {
                                                    mensagemErro = "O valor informado '" + valor + "' deve conter apenas números";
                                                    retorno = false;
                                                }
                                                else
                                                    texto = num.ToString();
                                                if (texto.Length > isAttribute.Tamanho)
                                                {
                                                    mensagemErro = "O valor informado '" + valor + "' excede o tamanho máximo permitido";
                                                    retorno = false;
                                                }
                                            }
                                            array[i] = texto;
                                        }
                                        else
                                        {
                                            string texto = trataTextoIBM(((string)valor).Trim());
                                            array[i] = texto;
                                        }
                                    }
                                }
                                else if (tipo.FullName.Contains("Model"))
                                {
                                    retorno = validar((BaseModel)valor, out mensagemErro);
                                }
                            }
                        }
                        tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, array);
                    }
                    
                }
                else
                {
                    var valor = p.GetValue(this);
                    if (valor == null)
                    {
                        if (tipo == typeof(string))
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, "");
                        else if (tipo == typeof(int) || tipo == typeof(long))
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, 0);
                    }
                    else
                    {
                        if (tipo == typeof(string))
                        {
                            if ("STRING".Equals(((string)valor).ToUpper()))
                            {
                                tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, "");
                            }
                            else
                            {
                                if (isAttribute != null)
                                {
                                    string texto = trataTextoIBM(((string)valor).Trim());
                                    if ("N".Equals(isAttribute.Tipo.ToUpper()))
                                    {
                                        texto = retiraCaracteresMascara(texto);
                                        long num = 0;
                                        if (!"".Equals(texto) && !long.TryParse(texto, out num))
                                        {
                                            mensagemErro = "O valor informado '" + valor + "' deve conter apenas números";
                                            retorno = false;
                                        }
                                        else
                                            texto = num.ToString();
                                        if (texto.Length > isAttribute.Tamanho)
                                        {
                                            mensagemErro = "O valor informado '" + valor + "' excede o tamanho máximo permitido";
                                            retorno = false;
                                        }
                                    }
                                    tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, texto);
                                }
                                else
                                {
                                    string texto = trataTextoIBM(((string)valor).Trim());
                                    tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, texto);
                                }
                            }
                        }
                        else if (tipo.FullName.Contains("Model"))
                        {
                            retorno = validar((BaseModel)valor, out mensagemErro);
                        }
                    }
                }
            }
            return retorno;
        }


        /// <summary>
        /// Substitui campos com valores null por um válido
        /// </summary>
        private void retirarNull(BaseModel baseModel)
        {
            Type tipoBaseModel = baseModel.GetType();
            foreach (PropertyInfo p in tipoBaseModel.GetProperties())
            {
                var tipo = p.PropertyType;
                if (tipo.FullName.Contains("[]"))
                {
                    /*
                    object[] array = (object[])p.GetValue(this);
                    foreach (object valor in array)
                    {
                        tipo = valor.GetType();
                      
                    }
                    */
                }
                else
                {
                    var valor = p.GetValue(this);
                    if (valor == null)
                    {
                        if (tipo == typeof(string))
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, "");
                        else if (tipo == typeof(int) || tipo == typeof(long))
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, 0);
                        else if (tipo.FullName.Contains("Model"))
                        {
                            retirarNull((BaseModel)valor);
                        }

                    }
                    else
                    {
                        if (tipo == typeof(string) && "STRING".Equals(((string)valor).ToUpper()))
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, valor);
                        else if (tipo == typeof(string))
                        {
                            tipoBaseModel.GetProperty(p.Name).SetValue(baseModel, trataTextoIBM(((string)valor).Trim()));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Retorna o json do Model
        /// </summary>
        public string getJson()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Retorna retorna valores de um baseModel
        /// </summary>
        public List<string> getValores(BaseModel baseModel, List<string> retorno)
        {
            Type _type = baseModel.GetType();
            string virgula = "";
            var prop = baseModel.GetType();
            foreach (PropertyInfo p in _type.GetProperties())
            {
                try
                {
                    string type = p.PropertyType.Name;
                    string value = null;
                    if ("String".Equals(type))
                        value = (string)_type.GetProperty(p.Name).GetGetMethod().Invoke(this, new object[] { });
                    else if ("String[]".Equals(type))
                    {
                        string[] values = (string[])_type.GetProperty(p.Name).GetGetMethod().Invoke(this, new object[] { });
                        value = "";
                        foreach (string valueTemp in values)
                        {
                            if (valueTemp != null && !"".Equals(valueTemp))
                                value = value + valueTemp + ",";
                        }
                        if (!"".Equals(value))
                        {
                            value = "[" + value + "]";
                        }
                    }
                    else if ("List`1".Equals(p.PropertyType.Name))
                    {
                        try
                        {
                            dynamic arrayModels = prop.GetProperty(p.Name).GetGetMethod().Invoke(baseModel, new object[] { });
                            foreach (Object objeto in arrayModels)
                            {
                                if (objeto != null)
                                {
                                    retorno = getValores((BaseModel)objeto, retorno);
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            string erro = e.Message;
                        }
                    }

                    //if (value != null && !"".Equals(value.Trim()))
                    retorno.Add(virgula + p.Name + ": " + value);
                    virgula = "; ";
                }
                catch (Exception) { }
            }
            return retorno;
        }

        /// <summary>
        /// Atribui valor a um campo 
        /// </summary>
        public void setValue(string nomeCampo,string valor)
        {
            try
            {
                Type typeThis = GetType();
                typeThis.GetProperty(nomeCampo).SetValue(this, valor);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Atribui valor a campos de um BaseModel com nomes semelhantes
        /// </summary>
        public bool setValues(BaseModel baseModel)
        {
            bool retorno = true;
            Type typeThis = GetType();
            IEnumerable<string> propertiesNames = typeThis.GetProperties().Select(p => p.Name);            
            //Type typeParm = baseModel.GetType();
            foreach (string name in propertiesNames)
            {

                try
                {
                    dynamic value = baseModel.getValorAsObject(name);
                    if (baseModel.GetType().GetProperty(name) != null)
                    {
                        var typeParm = baseModel.GetType().GetProperty(name).PropertyType;
                        var typeLocal = GetType().GetProperty(name).PropertyType;

                        //var value = typeParm.GetProperty(name).GetGetMethod().Invoke(baseModel, new object[] { });
                        if (typeParm != typeLocal)
                        {
                            if (typeLocal == typeof(string) && typeParm == typeof(Int32))
                                value = ((Int32)value).ToString();
                            else if (typeLocal == typeof(Int32) && typeParm == typeof(string))
                            {
                                string valueString = (string)value;
                                if (valueString != null && !"".Equals(valueString))
                                {
                                    valueString = valueString.Replace('.', ' ');
                                    valueString = valueString.Replace(',', ' ');
                                    valueString = valueString.RemoveEspacos();
                                    value = Int32.Parse(valueString);
                                }
                                else
                                    value = null;
                            }
                            else if (typeLocal == typeof(string) && (typeParm == typeof(Nullable<DateTime>) || typeParm == typeof(DateTime)))
                                value = ((DateTime)value).ToString("MM/dd/yyyy HH:mm:ss");
                            else if ((typeLocal == typeof(DateTime) && typeParm == typeof(Nullable<DateTime>)) || (typeLocal == typeof(Nullable<DateTime>) && typeParm == typeof(DateTime)))
                                value = (DateTime)value;
                            else
                            {
                                value = null;
                                if (typeLocal == typeof(string))
                                    value = "";
                            }
                        }
                        if (value != null && ((typeLocal == typeof(string) && !"".Equals((string)value)) || typeLocal != typeof(string)))
                            typeThis.GetProperty(name).SetValue(this, value);
                    }
                }
                catch (Exception)
                {
                    retorno = false;
                }
            }
            return retorno;
        }

        /// <summary>
        /// Substitui algum texto por vazio 
        /// </summary>
        public void retiraTexto(string texto)
        {
            retiraTexto(texto, this);
        }
        /// <summary>
        /// Substitui algum texto por vazio 
        /// </summary>
        public void retiraTexto(string texto, BaseModel baseModel)
        {
            Type _type = baseModel.GetType();
            var prop = baseModel.GetType();
            foreach (PropertyInfo p in _type.GetProperties())
            {
                try
                {
                    string type = p.PropertyType.Name;
                    string value = null;
                    if ("String".Equals(type))
                    {
                        value = (string)_type.GetProperty(p.Name).GetGetMethod().Invoke(this, new object[] { });
                        if (texto.Equals(value.ToLower()))
                        {
                            _type.GetProperty(p.Name).SetValue(baseModel, "");
                        }

                    }
                    else if ("String[]".Equals(type))
                    {
                        String[] values = (string[])_type.GetProperty(p.Name).GetGetMethod().Invoke(this, new object[] { });
                        Type typeArray = _type.GetProperty(p.Name).GetType();
                        bool achou = false;
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (texto.Equals(values[i].ToLower()))
                            {
                                values[i] = "";
                                achou = true;
                            }
                        }
                        if (achou)
                        {
                            _type.GetProperty(p.Name).SetValue(p, values);

                        }
                    }
                    else if ("List`1".Equals(p.PropertyType.Name))
                    {
                        try
                        {
                            dynamic arrayModels = prop.GetProperty(p.Name).GetGetMethod().Invoke(baseModel, new object[] { });
                            foreach (Object objeto in arrayModels)
                            {
                                if (objeto != null)
                                {
                                    retiraTexto(texto, (BaseModel)objeto);
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            string erro = e.Message;
                        }
                    }
                }
                catch (Exception e)
                {
                    string erro = e.Message;
                }
            }
        }

        /// <summary>
        /// Retorna o valor de um determinado  campo
        /// </summary>
        public string getValor(string nomeCampo)
        {
            try
            {
                string retorno = "";
                Type _type = GetType();
                IEnumerable<string> propertiesNames = _type.GetProperties().Select(p => p.Name);
                foreach (PropertyInfo p in _type.GetProperties())
                {
                    try
                    {
                        string tipo = p.PropertyType.ToString();
                        if (nomeCampo.Equals(p.Name))
                        {
                            if (tipo != null && tipo.Length > 19 && "Copasa.Atende.Model".Equals(tipo.Substring(0, 19)))
                            {
                                if ("[]".Equals(tipo.Substring(tipo.Length - 2, 2)))
                                {
                                    BaseModel[] bms = (BaseModel[])_type.GetProperty(p.Name).GetValue(this);
                                    foreach (BaseModel bm in bms)
                                    {
                                        retorno = bm.getValor(nomeCampo);
                                        if (!"".Equals(retorno))
                                        {
                                            return retorno;
                                        }
                                    }
                                }
                                else
                                {
                                    BaseModel bm = (BaseModel)_type.GetProperty(p.Name).GetValue(this);
                                    retorno = bm.getValor(nomeCampo);
                                    if (!"".Equals(retorno))
                                    {
                                        return retorno;
                                    }
                                }
                            }
                            else
                            {
                                return (string)_type.GetProperty(nomeCampo).GetValue(this);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string erro = e.Message;
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Retorna o valor de um determinado  campo
        /// </summary>
        public dynamic getValorAsObject(string nomeCampo)
        {
            try
            {
                object retorno = null;
                Type _type = GetType();
                IEnumerable<string> propertiesNames = _type.GetProperties().Select(p => p.Name);
                foreach (PropertyInfo p in _type.GetProperties())
                {
                    try
                    {
                        string tipo = p.PropertyType.ToString();
                        if (nomeCampo.Equals(p.Name))
                        {
                            if (tipo != null && tipo.Length > 19 && "Copasa.Atende.Model".Equals(tipo.Substring(0, 19)))
                            {
                                if ("[]".Equals(tipo.Substring(tipo.Length - 2, 2)))
                                {
                                    BaseModel[] bms = (BaseModel[])_type.GetProperty(p.Name).GetValue(this);
                                    foreach (BaseModel bm in bms)
                                    {
                                        retorno = bm.getValor(nomeCampo);
                                        return retorno;
                                    }
                                }
                                else
                                {
                                    BaseModel bm = (BaseModel)_type.GetProperty(p.Name).GetValue(this);
                                    //retorno = bm.getValorAsObject(nomeCampo);
                                    return bm;
                                }
                            }
                            else
                            {
                                return _type.GetProperty(nomeCampo).GetValue(this);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string erro = e.Message;
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Retorna se o campo existe
        /// </summary>
        public bool existe(string nomeCampo)
        {
            try
            {
                Type objtype = GetType();
                string retorno = (string)objtype.GetProperty(nomeCampo).GetValue(this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Retorna se o campo existe
        /// </summary>
        public override string ToString()
        {
            StringBuilder retorno = new StringBuilder();
            foreach (string valor in getValores())
            {
                retorno.Append(valor);
            }
            return retorno.ToString();
        }

        /// <summary>
        /// Retorna apenas os números do texto
        /// </summary>
        private string getNumeros(string cpfCnpj)
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

        /// <summary>
        /// Retira caracteres de mascaras em numeros
        /// </summary>
        private string retiraCaracteresMascara(string cpfCnpj)
        {
            char[] chars = cpfCnpj.ToCharArray();
            List<char> retorno = new List<char>();
            foreach (char cr in chars)
            {
                if (cr!='.' && cr != ',' && cr != '-' && cr != '/' && cr != '_' && cr != ':' && cr != ';')
                    retorno.Add(cr);
            }
            return new string(retorno.ToArray());
        }


        /// <summary>
        /// Retira caracteres especiais
        /// </summary>
        private string trataTextoIBM(string texto)
        {
            try
            {
                if (texto != null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < texto.Trim().Length; i++)
                    {
                        char caracter = trataCaracter(texto.Substring(i, 1));
                        sb.Append(caracter);
                    }
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

        private char trataCaracter(string texto)
        {
            char caracter = texto.ToUpper()[0];
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
            }
            int codigo = Convert.ToInt32(caracter);

            if (codigo > 126 || codigo < 35)
                return ' ';
            else
                return caracter;
        }
    }
}
