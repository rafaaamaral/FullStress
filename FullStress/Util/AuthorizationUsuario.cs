using Business;
using Global.Enum;
using Global.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Util
{
    public class AuthorizationUsuario : AuthorizeAttribute
    {
        public Modulo Modulo { get; set; }
        public Transacao Transacao { get; set; }
        public Erro Erro { get; set; }

        public int MyProperty { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var sessaoCookie = httpContext.Request.Cookies[Security.SessaoSeguranca];

            if (sessaoCookie != null)
            {
                if (!string.IsNullOrEmpty(sessaoCookie.Value.ToString()))
                {
                    var strValor = sessaoCookie.Value;

                    var usuario = new UsuarioBusiness().BuscarUsuarioSessao(strValor);

                    if(!usuario.Sucesso)
                    {
                        Erro = Erro.SessaoExpirada;
                        return false;
                    }
                    else
                    {
                        UsuarioLogado.Usuario = usuario.Data;
                    }
                }
            }
            else
            {
                Erro = Erro.SessaoExpirada;
                return false;
            }

            var retorno = new object();

            if (Modulo != 0 && Transacao != 0)
            {
                if (UsuarioLogado.Usuario.Perfil == null)
                    return false;

                if (UsuarioLogado.Usuario.Perfil != null)
                {
                    retorno =
                    UsuarioLogado.Usuario.PerfilModuloTransacao
                                 .FirstOrDefault(f => f.ModuloTransacao.IdModulo == (int)Modulo && f.ModuloTransacao.IdTransacao == (int)Transacao && f.Ativo);
                }

                if (retorno == null)
                    return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            switch (Erro)
            {
                case Erro.SessaoExpirada:
                    filterContext.Result = new RedirectResult("/Erro/Erro401");
                    break;
                default:
                    filterContext.Result = new RedirectResult("/Erro/Erro403");
                    break;
            }

        }
    }
}