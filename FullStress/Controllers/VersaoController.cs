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
    public class VersaoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new VersaoBusiness().GetAll();

            if (retorno.Sucesso)
            {
                ViewBag.lstVersao = retorno.Data;
            }
            else
            {
                ViewBag.lstVersao = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                var retorno = new VersaoBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idVersao = retorno.Data.Id;
                    ViewBag.nome = retorno.Data.Nome;
                    ViewBag.atualProducao = retorno.Data.AtualProducao;

                }
                else
                {
                    ViewBag.idVersao = 0;
                    ViewBag.nome = null;
                    ViewBag.atualProducao = false;
                }
            }
            catch (Exception e)
            {
                ViewBag.idVersao = 0;
                ViewBag.nome = null;
                ViewBag.atualProducao = false;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarVersao(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<VersaoDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                if (objDados.Id == 0)
                {
                    retorno = new VersaoBusiness().IncluirVersao(objDados, UsuarioLogado.Usuario.Id);
                }
                else
                {
                    retorno = new VersaoBusiness().AlterarVersao(objDados, UsuarioLogado.Usuario.Id);
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