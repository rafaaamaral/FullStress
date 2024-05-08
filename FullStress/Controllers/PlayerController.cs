using Business;
using DTO;
using FullStress.Util;
using Global.Retornos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStress.Controllers
{
    [AuthorizationUsuario()]
    public class PlayerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new UsuarioBusiness().GetAll();

            if (retorno.Sucesso)
            {
                ViewBag.lstUsuario = retorno.Data;
            }
            else
            {
                ViewBag.lstUsuario = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {

                var retornoPerfil = new PerfilBusiness().GetAll();

                if (retornoPerfil.Sucesso)
                {
                    ViewBag.lstPerfil = retornoPerfil.Data;
                }
                else
                {
                    ViewBag.lstPerfil = null;
                }

                var retorno = new UsuarioBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idUsuario = retorno.Data.Id;
                    ViewBag.nome = retorno.Data.Nome;
                    ViewBag.psnID = retorno.Data.PsnID;
                    ViewBag.login = retorno.Data.Login;
                    ViewBag.idPerfil = retorno.Data.IdPerfil;
                    ViewBag.senha = retorno.Data.Senha;
                }
                else
                {
                    ViewBag.idUsuario = 0;
                    ViewBag.nome = null;
                    ViewBag.psnID = null;
                    ViewBag.login = null;
                    ViewBag.idPerfil = null;
                    ViewBag.senha = null;
                }
            }
            catch(Exception e)
            {
                ViewBag.lstPerfil = null;
                ViewBag.idUsuario = 0;
                ViewBag.nome = null;
                ViewBag.psnID = null;
                ViewBag.login = null;
                ViewBag.idPerfil = null;
                ViewBag.senha = null;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarUsuario(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<UsuarioDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                if(objDados.Id == 0)
                {
                    retorno = new UsuarioBusiness().IncluirUsuario(objDados);
                }
                else
                {
                    retorno = new UsuarioBusiness().AlterarUsuario(objDados);
                }
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }
    }
}