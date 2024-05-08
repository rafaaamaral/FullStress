using Business;
using DTO;
using FullStress.Util;
using Global.Retornos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    [AuthorizationUsuario()]
    public class MeusDadosController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SalvarMeusDados(string strDados)
        {
            var retorno = new Retorno();

            var objDados = JsonConvert.DeserializeObject<UsuarioDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new UsuarioBusiness().AlterarMeusDados(objDados);

                if (retorno.Sucesso)
                {
                    UsuarioLogado.Usuario.Nome = objDados.Nome;
                    UsuarioLogado.Usuario.FotoPerfil = objDados.FotoPerfil;
                }
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }


        public ActionResult EnviaImagemUsuario()
        {

            var isSavedSuccessfully = true;
            var path = "";
            var nomeArquivo = "";

            try
            {
                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];

                    if (!(file?.ContentLength > 0)) continue;
                    var originalDirectory = new DirectoryInfo($"{Server.MapPath("~/Imagens/")}\\Usuario");

                    var pathString = Path.Combine(originalDirectory.ToString(), "");

                    var moment = DateTime.Now;

                    var extension = Path.GetExtension(file.FileName);

                    if (extension != null)
                        nomeArquivo = moment.Hour.ToString() + "_" + moment.Minute.ToString() + "_" + moment.Second.ToString() + moment.Millisecond.ToString() + extension.ToLower();

                    var isExists = Directory.Exists(pathString);

                    if (!isExists)
                        Directory.CreateDirectory(pathString);

                    path = $"{originalDirectory}\\{nomeArquivo}";
                    file.SaveAs(path);

                    //try
                    //{
                    //    var objNegocio = new BllUsuario();
                    //    var intRetorno = objNegocio.Avatar(objSessPesq, nomeArquivo);

                    //    if (intRetorno > 0)
                    //    {
                    //        UContext objContext = Security.UserContext(Request);
                    //        //HttpContext.Response.Cookies.Add(Security.GravaUContext(objContext.Nome, objContext.Perfil.ToString(), nomeArquivo, objContext.Cargo.ToString(),  Request));
                    //        HttpContext.Response.Cookies.Add(Security.GravaUContext(objContext.Nome, objContext.Perfil.ToString(), nomeArquivo, objContext.Cargo.ToString(), "0", Request));

                    //    }
                    //}
                    //catch
                    //{
                    //    System.IO.File.Delete(path);
                    //}
                }
            }
            catch
            {
                System.IO.File.Delete(path);
                isSavedSuccessfully = false;
            }

            return Json(isSavedSuccessfully ? new { Message = nomeArquivo } : new { Message = "Error" });
        }
    }
}