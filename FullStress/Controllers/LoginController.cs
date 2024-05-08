using Business;
using FullStress.Models;
using FullStress.Util;
using Global.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            TempData["msgContent"] = null;

            return View();
        }

        public ActionResult Logar(LoginModel model)
        {

            //if (!ModelState.IsValid)
            //if ((string.IsNullOrEmpty(model.Login.ToString()) && string.IsNullOrEmpty(model.Senha.ToString())))
            //    return View("Index", model);

            var usuario = model.ObterUsuario(model.Login, model.Senha);

            if (!usuario.Sucesso)
            {
                UsuarioLogado.Usuario = null;

                SetMensagem(usuario.Mensagem);
                return View("Index");
            }

            UsuarioLogado.Usuario = model.Usuario;

            var codigoSessao = new SessaoBusiness().IncluirSessao(model.Usuario, Security.RecuperaDadosMaquina(Request));

            if (!string.IsNullOrEmpty(codigoSessao.Data))
            {
                HttpContext.Response.Cookies.Add(Security.GravaCSess(codigoSessao.Data));
                HttpContext.Response.Cookies.Add(Security.GravaUContext(model.Usuario.Nome, model.Usuario.IdPerfil.ToString(), model.Usuario.FotoPerfil, Request));
            }
            else
            {
                UsuarioLogado.Usuario = null;

                SetMensagem(usuario.Mensagem);
                return View("Index");
            }

            if (UsuarioLogado.Usuario != null)
            {
                if (UsuarioLogado.Usuario.Ativo)
                {
                    Session["Usuario"] = model.Usuario;
                }
                else
                {
                    SetMensagem("Usuário Inativo!");

                    return View("Index");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public void SetMensagem(string mensagem)
        {
            TempData["msgContent"] = mensagem;
            //TempData["msgType"] = tipo;
        }

        //public ActionResult RecuperarSenha(LoginModel model)
        //{

        //    if (string.IsNullOrEmpty(model.Login))
        //        return View("Index", model);

        //    var senhaRecuperada = model.RecuperarSenha(model.CPF);
        //    string tipo = senhaRecuperada.Mensagem;

        //    SetMensagem(tipo);
        //    return View("Index");

        //}
    }
}