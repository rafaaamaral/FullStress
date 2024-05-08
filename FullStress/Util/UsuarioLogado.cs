using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStress.Util
{
    public class UsuarioLogado
    {
        public UsuarioLogado()
        {
            Token = new Guid();
        }

        public static UsuarioDTO Usuario
        {
            get
            {
                if (HttpContext.Current.Session["UsuarioLogadoLogged"] == null)
                    return null;

                HttpContext.Current.Session.Timeout = 300;

                return HttpContext.Current.Session["UsuarioLogadoLogged"] as UsuarioDTO;
            }

            set { HttpContext.Current.Session["UsuarioLogadoLogged"] = value; }
        }

        public static Guid Token { get; set; }

        public static string ImagemClassificacao { get; set; }
    }
}