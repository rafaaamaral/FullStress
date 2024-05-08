using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Global.Funcoes
{
    public static class Geral
    {
        #region Constantes

        public static readonly IFormatProvider FormatoPTBR = new System.Globalization.CultureInfo("pt-br", true);
        public static readonly IFormatProvider FormatoENUS = new System.Globalization.CultureInfo("en-US", true);
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        #endregion

        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static string ConvertStringArrayToString(string[] array)
        {
            if (array == null)
                return string.Empty;
            else
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (string value in array)
                {
                    if (i != 0)
                        builder.Append(',');

                    builder.Append(value);
                    i++;
                }
                return builder.ToString();
            }
        }

        public static string ConvertIntArrayToString(int[] array)
        {
            if (array == null)
                return string.Empty;
            else
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (int value in array)
                {
                    if (i != 0)
                        builder.Append(',');

                    builder.Append(value);
                    i++;
                }
                return builder.ToString();
            }
        }

        public static bool ValidaCNPJ(string cnpj)
        {

            string CNPJ = cnpj.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                    CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                        nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                        nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }

        }

        public static bool ValidarEmail(string email)
        {
            try
            {
                bool isValid = Regex.Match(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z").Success;
                return isValid;

                //var mail = new System.Net.Mail.MailAddress(email);
                //return true;
            }
            catch
            {
                return false;
            }
        }

        public static string AppSetting(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
                throw new Exception("AppSetting não informado");

            return ConfigurationManager.AppSettings[key];
        }

        public static string ConverterParaXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = new XmlTextWriter(sw);

            serializer.Serialize(tw, obj);
            return sw.ToString();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            return ToDataTable<T>(items, new List<string>());

        }

        public static DataTable ToDataTable<T>(List<T> items, List<string> ignorarColunas)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            ignorarColunas.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x) && dataTable.Columns.Contains(x))
                    dataTable.Columns.Remove(x);
            });
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string RandomString(int length)
        {
            const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var result = new StringBuilder(length);
            for (var i = 0; i < length; i++)
                result.Append(characters[random.Next(characters.Length)]);

            return result.ToString();
        }

        public static string FormatarCPF(string cpf)
        {
            if (cpf != null && cpf.Length == 11)
                return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");

            return cpf;
        }

        public static string FormatarTelefone(string telefone)
        {
            if (telefone != null && telefone.Length >= 10)
                return telefone.Insert(0, "(").Insert(3, ")").Insert(8, "-");

            return telefone;
        }

        public static string FormatarCelular(string celular)
        {
            if (celular != null && celular.Length >= 10)
                return celular.Insert(0, "(").Insert(3, ")").Insert(9, "-");

            return celular;
        }

        public static string FormatarCNPJ(string cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj) && cnpj.Length == 14)
                return cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            return cnpj;
        }

        public static string FormatarCEP(string cep)
        {
            if (!string.IsNullOrEmpty(cep) && cep.Length == 8)
                return cep.Insert(5, "-");
            return cep;
        }

        public static bool DataValida(string data)
        {
            DateTime outDate;
            return DateTime.TryParseExact(data.Replace("/", ""), "ddMMyyyy", FormatoPTBR, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out outDate);
        }

        public static string DataToString(DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        public static string SubstringReticencias(this string value, int qtdeCaracteres)
        {
            if (value.Length > qtdeCaracteres)
                return value.Substring(0, qtdeCaracteres) + "...";

            return value;
        }

        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToInt32(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, dataRow[dtField.Name].ToString(), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static DateTime ObterPrimeiroDiaMes(DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1);
        }

        public static DateTime ObterUltimoDiaMes(DateTime data)
        {
            return new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
        }

        public static string RetornaSomenteNumeros(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return null;

            return Regex.Replace(texto, "[^0-9]+", string.Empty);
        }
    }
}
