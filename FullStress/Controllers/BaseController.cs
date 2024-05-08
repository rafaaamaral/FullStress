using Business;
using FullStress.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    public class BaseController : Controller
    {
        [AllowAnonymous]
        protected override void OnException(ExceptionContext exceptionContext)
        {
            var idUsuario = 0;

            if (UsuarioLogado.Usuario != null)
            {
                idUsuario = UsuarioLogado.Usuario.Id;
            }

            new ErroBusiness().IncluirErro(idUsuario, exceptionContext.Exception.StackTrace, exceptionContext.Exception.Message);


            Response.Redirect("/Erro");
        }

        public void SetMensagem(string mensagem)
        {
            TempData["msgContent"] = mensagem;
        }

        public void ObterDescricaoTela(string mensagem)
        {
            ViewBag.DescricaoTela = mensagem;
        }

        public string SalvarDocumento(HttpPostedFileBase file, string pasta)
        {
            var retorno = "";

            try
            {
                var originalDirectory = new DirectoryInfo($"{Server.MapPath("~/Arquivos/")}\\" + pasta);

                var pathString = Path.Combine(originalDirectory.ToString(), "");

                var moment = DateTime.Now;

                var extension = Path.GetExtension(file.FileName);

                if (extension != null)
                    retorno = moment.Hour.ToString() + "_" + moment.Minute.ToString() + "_" + moment.Second.ToString() + moment.Millisecond.ToString() + "_" + UsuarioLogado.Token + extension.ToLower();

                var isExists = Directory.Exists(pathString);

                if (!isExists)
                    Directory.CreateDirectory(pathString);

                var path = $"{originalDirectory}\\{retorno}";
                file.SaveAs(path);
            }
            catch
            {

            }

            return retorno;
        }
    }
}