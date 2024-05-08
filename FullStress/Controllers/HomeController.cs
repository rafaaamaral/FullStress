using Business;
using FullStress.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    [AuthorizationUsuario()]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            new SessaoBusiness().InativarSessao(UsuarioLogado.Usuario);

            UsuarioLogado.Usuario = null;

            return RedirectToAction("Index", "Login");
        }
    }
}