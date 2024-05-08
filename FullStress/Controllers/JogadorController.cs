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
    public class JogadorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new JogadorBusiness().ObterJogadorPorVersaoAtiva(UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstJogador = retorno.Data;
            }
            else
            {
                ViewBag.lstJogador = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                var retornoTipoJogador = new TipoJogadorBusiness().GetAll();

                if (retornoTipoJogador.Sucesso)
                {
                    ViewBag.lstTipoJogador = retornoTipoJogador.Data;
                }
                else
                {
                    ViewBag.lstTipoJogador = null;
                }

                var retorno = new JogadorBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idJogador = retorno.Data.Id;
                    ViewBag.nome = retorno.Data.Nome;
                    ViewBag.overral = retorno.Data.Overral;
                    ViewBag.link = retorno.Data.Link;
                    ViewBag.idTipoJogador = retorno.Data.IdTipoJogador;
                }
                else
                {
                    ViewBag.idJogador = 0;
                    ViewBag.nome = null;
                    ViewBag.overral = null;
                    ViewBag.link = null;
                    ViewBag.idTipoJogador = null;
                }
            }
            catch (Exception e)
            {
                ViewBag.idJogador = 0;
                ViewBag.nome = null;
                ViewBag.overral = null;
                ViewBag.link = null;
                ViewBag.idTipoJogador = null;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarJogador(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<JogadorDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                if (objDados.Id == 0)
                {
                    retorno = new JogadorBusiness().IncluirJogador(objDados, UsuarioLogado.Usuario.Id);
                }
                else
                {
                    retorno = new JogadorBusiness().AlterarJogador(objDados, UsuarioLogado.Usuario.Id);
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