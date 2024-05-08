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
    public class ClubeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new ClubeBusiness().ObterClubePorUsuarioVersao(UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstClube = retorno.Data;
            }
            else
            {
                ViewBag.lstClube = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                try
                {
                    var retornoJogador = new JogadorBusiness().ObterJogadorPorVersaoAtiva(UsuarioLogado.Usuario.Id);

                    if (retornoJogador.Sucesso)
                    {
                        ViewBag.lstJogador = retornoJogador.Data;
                    }
                    else
                    {
                        ViewBag.lstJogador = null;
                    }
                }
                catch
                {
                    ViewBag.lstJogador = null;
                }

                var retorno = new ClubeBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idClube = retorno.Data.Id;
                    ViewBag.nome = retorno.Data.Nome;
                }
                else
                {
                    ViewBag.idClube = 0;
                    ViewBag.nome = null;
                }
            }
            catch (Exception e)
            {
                ViewBag.idClube = 0;
                ViewBag.nome = null;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarClube(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<ClubeDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                if (objDados.Id == 0)
                {
                    retorno = new ClubeBusiness().IncluirClube(objDados, UsuarioLogado.Usuario.Id);
                }
                else
                {
                    retorno = new ClubeBusiness().AlterarClube(objDados, UsuarioLogado.Usuario.Id);
                }
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        #region Clube Jogador

        public PartialViewResult ListaJogador(string strDados)
        {
            var objDados = JsonConvert.DeserializeObject<ClubeDTO>(strDados);

            var retorno = new ClubeJogadorBusiness().ObterJogadorPorClube(objDados.Id, UsuarioLogado.Usuario.Id);

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

        [HttpPost]
        public JsonResult SalvarClubeJogador(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<ClubeJogadorDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new ClubeJogadorBusiness().IncluirClubeJogador(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        [HttpPost]
        public JsonResult AtivarInativarClubeJogador(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<ClubeJogadorDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new ClubeJogadorBusiness().AtivarInativarClubeJogador(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        #endregion
    }
}