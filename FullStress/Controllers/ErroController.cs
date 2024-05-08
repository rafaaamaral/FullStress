using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    public class ErroController : BaseController
    {
        public ActionResult Erro401()
        {
            return View();
        }

        public ActionResult Erro403()
        {
            return View();
        }

        public ActionResult Erro404()
        {
            return View();
        }

        public ActionResult Erro()
        {
            return View();
        }
    }
}