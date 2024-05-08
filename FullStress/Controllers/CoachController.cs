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
    [AuthorizationUsuario]
    public class CoachController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var objDados = JsonConvert.DeserializeObject<CoachDTO>(strDados);

            var retorno = new CoachBusiness().ObterCoachPorStatus(objDados.Status, UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstCoach = retorno.Data;
            }
            else
            {
                ViewBag.lstCoach = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                var retorno = new CoachBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idCoach = retorno.Data.Id;
                    ViewBag.titulo = retorno.Data.Titulo;
                    ViewBag.observacaoPlayer = retorno.Data.ObservacaoPlayer;
                    ViewBag.linkVideo = retorno.Data.LinkVideo;
                    ViewBag.feedBackCoach = retorno.Data.FeedbackCoach;
                    ViewBag.idStatus = retorno.Data.Status;
                }
                else
                {
                    ViewBag.idCoach = 0;
                    ViewBag.titulo = null;
                    ViewBag.observacaoPlayer = null;
                    ViewBag.linkVideo = null;
                    ViewBag.feedBackCoach = null;
                    ViewBag.idStatus = null;
                }
            }
            catch (Exception e)
            {
                ViewBag.idCoach = 0;
                ViewBag.titulo = null;
                ViewBag.observacaoPlayer = null;
                ViewBag.linkVideo = null;
                ViewBag.feedBackCoach = null;
                ViewBag.idStatus = null;
            }

            return View();
        }

        [HttpPost]
        public JsonResult ResponderCoach(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<CoachDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new CoachBusiness().ResponderCoach(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }
    }
}