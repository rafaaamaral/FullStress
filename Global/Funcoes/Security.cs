using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Global.Funcoes
{
    public class Security
    {
        public const string SessaoSeguranca = "sessFullStress"; //"sessBolacha"
        public const string SessaoGateway = "sessFullStressGAT";
        public const string SessaoContextoUsuario = "uContextFullStress";
        private const string SessaoRememberMe = "rmbmeFullStress";

        public enum TipoCookie
        {
            Seguranca = 0,
            Contexto = 1,
            RememberMe = 2
        }

        public static HttpCookie GravaCSess(string pstrSessao)
        {

            var sessCookie = new HttpCookie(SessaoSeguranca) //sessConnectAlbaugh
            {
                Value = pstrSessao,
                Expires = DateTime.Now.AddDays(1)
            };

            return sessCookie;
        }

        public static HttpCookie LogoffCookie(TipoCookie enmTipo)
        {
            if (TipoCookie.Seguranca.Equals(enmTipo))
            {
                var sessCookie = new HttpCookie(SessaoSeguranca) //sessConnectAlbaugh
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                return sessCookie;
            }
            else if (TipoCookie.Contexto.Equals(enmTipo))
            {
                var sessCookie = new HttpCookie(SessaoContextoUsuario) //sessConnectAlbaugh
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                return sessCookie;
            }
            else
            {
                var sessCookie = new HttpCookie(SessaoRememberMe) //sessConnectAlbaugh
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                return sessCookie;
            }
        }

        public static HttpCookie GravaUContext(string nome, string perfil, string foto, HttpRequestBase pRequest)
        {

            try
            {
                HttpCookie uCookie = new HttpCookie(SessaoContextoUsuario); //uContextConnectAlbaugh

                uCookie.Value = nome + "/" + perfil + "/" + foto;
                uCookie.Expires = DateTime.Now.AddDays(1);
                return uCookie;
            }
            catch
            {
                HttpCookie uCookie = new HttpCookie(SessaoContextoUsuario);

                uCookie.Value = "Erro" + "/" + "0" + "/" + "false" + "/" + "0" + "/" + "0" + "/" + "false";
                uCookie.Expires = DateTime.Now.AddDays(1);
                return uCookie;
            }
        }

        public static SessaoPesq ValidaSessao(HttpRequestBase pRequest)
        {
            try
            {
                var pComunicacao = pRequest.Cookies[SessaoSeguranca];

                if (pComunicacao != null)
                {
                    SessaoPesq objReturn = new SessaoPesq();
                    objReturn.StrSessao = "";
                    objReturn.CdSite = 0;

                    if (!string.IsNullOrEmpty(pComunicacao.Value.ToString()))
                    {

                        string[] _strValor = pComunicacao.Value.Split('/');
                        objReturn.StrSessao = _strValor[0];
                        objReturn.CdSite = Convert.ToInt32(_strValor[1]);
                    }

                    return objReturn;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static UsuarioMaquina RecuperaDadosMaquina(HttpRequestBase pRequest)
        {
            UsuarioMaquina objMaquina = new UsuarioMaquina();
            objMaquina.Maquina = pegaIPUsuario(pRequest);
            objMaquina.Browser = qualBrowser(pRequest);
            objMaquina.Mobile = estaNoCelular(pRequest);
            objMaquina.Dispositivo = qualDispositivo(pRequest);
            objMaquina.UserAgent = pRequest.UserAgent.ToLowerInvariant();

            return objMaquina;
        }

        private static string pegaIPUsuario(HttpRequestBase pRequest)
        {

            // Conexão utilizando proxy
            //string strIPUsuario = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string strIPUsuario = pRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (strIPUsuario == null)
            {
                // Conexão sem utilizar proxy
                strIPUsuario = pRequest.ServerVariables["REMOTE_ADDR"];
            }

            return strIPUsuario;
        }

        private static bool estaNoCelular(HttpRequestBase request)
        {
            bool isMobile = request.Browser.IsMobileDevice;
            string userAgent = request.UserAgent.ToLowerInvariant();
            Regex rgxAgentes = null;
            StringBuilder sbAgentes = null;

            try
            {
                sbAgentes = new StringBuilder();

                sbAgentes.Append(@"(iphone)");
                sbAgentes.Append(@"|(ipod)");
                sbAgentes.Append(@"|(ipad)");
                sbAgentes.Append(@"|(windows ce)");
                sbAgentes.Append(@"|(windows phone)");
                sbAgentes.Append(@"|(samsung)");
                sbAgentes.Append(@"|(lg)");
                sbAgentes.Append(@"|(nokia)");
                sbAgentes.Append(@"|(blackberry)");
                sbAgentes.Append(@"|(mobile)");
                sbAgentes.Append(@"|(palm)");
                sbAgentes.Append(@"|(fennec)");
                sbAgentes.Append(@"|(adobeair)");
                sbAgentes.Append(@"|(ripple)");
                sbAgentes.Append(@"|(android)");
                sbAgentes.Append(@"|(tablet)");

                rgxAgentes = new Regex(sbAgentes.ToString(), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

                if (rgxAgentes.IsMatch(userAgent))
                {
                    isMobile = true;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                userAgent = null;
                rgxAgentes = null;
                sbAgentes = null;

            }

            return isMobile;
        }

        private static string qualDispositivo(HttpRequestBase request)
        {
            string userAgent = request.UserAgent.ToLowerInvariant();
            string strDispositivo = "desconhecido";

            try
            {

                string[] arrDados = new string[16];
                arrDados[0] = "android";
                arrDados[1] = "iphone";
                arrDados[2] = "ipad";
                arrDados[3] = "samsung";
                arrDados[4] = "lg";
                arrDados[5] = "windows phone";
                arrDados[6] = "windows ce";
                arrDados[7] = "nokia";
                arrDados[8] = "blackberry";
                arrDados[9] = "palm";
                arrDados[10] = "fennec";
                arrDados[11] = "adobeair";
                arrDados[12] = "ripple";
                arrDados[13] = "ipod";
                arrDados[14] = "tablet";

                for (int intIndex = 0; intIndex < arrDados.Length; intIndex++)
                {
                    if (userAgent.ToLower().IndexOf(arrDados[intIndex]) > 0)
                    {
                        strDispositivo = arrDados[intIndex];
                        break;
                    }
                }

            }
            catch
            {
                strDispositivo = "desconhecido";
            }

            return strDispositivo;
        }

        private static string qualBrowser(HttpRequestBase request)
        {
            string strBrowser = "";

            switch (request.Browser.Browser.ToLower())
            {
                case "ie":
                    strBrowser = "ie";
                    break;
                case "firefox":
                    strBrowser = "firefox";
                    break;
                case "chrome":
                    strBrowser = "chrome";
                    break;
                case "safari":
                    strBrowser = "safari";
                    break;
                case "unknown":
                    strBrowser = request.Browser.Browser.ToLower().PadLeft(10);
                    break;
            }

            return strBrowser;
        }
    }
}
