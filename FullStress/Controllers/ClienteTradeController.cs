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
    public class ClienteTradeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new ClienteTradeBusiness().ObterClienteTradePorUsuario(UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstCliente = retorno.Data;
                ViewBag.UsuarioAtivo = retorno.Data.Count(c => c.Ativo);
                ViewBag.UsuarioInativo = retorno.Data.Count(c => !c.Ativo);
            }
            else
            {
                ViewBag.lstCliente = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                var retorno = new ClienteTradeBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idClienteTrade = retorno.Data.Id;
                    ViewBag.nome = retorno.Data.Nome;
                    ViewBag.telefone = retorno.Data.Telefone;
                    ViewBag.plano = retorno.Data.Plano;
                    ViewBag.ativo = retorno.Data.Ativo;
                }
                else
                {
                    ViewBag.idClienteTrade = 0;
                    ViewBag.nome = null;
                    ViewBag.telefone = null;
                    ViewBag.plano = null;
                    ViewBag.ativo = false;
                }
            }
            catch (Exception e)
            {
                ViewBag.idClienteTrade = 0;
                ViewBag.nome = null;
                ViewBag.telefone = null;
                ViewBag.plano = null;
                ViewBag.ativo = false;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarClienteTrade(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<ClienteTradeDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                if (objDados.Id == 0)
                {
                    retorno = new ClienteTradeBusiness().IncluirClienteTrade(objDados, UsuarioLogado.Usuario.Id);
                }
                else
                {
                    retorno = new ClienteTradeBusiness().AlterarClienteTrade(objDados, UsuarioLogado.Usuario.Id);
                }
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        #region Cliente Trade Faturamento

        public PartialViewResult ListaFaturamento(string strDados)
        {
            var objDados = JsonConvert.DeserializeObject<ClienteTradeDTO>(strDados);

            var retorno = new ClienteTradeFaturamentoBusiness().ObterFaturamentoPorCliente(objDados.Id, UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstClienteFaturamento = retorno.Data;
            }
            else
            {
                ViewBag.lstClienteFaturamento = null;
            }

            return PartialView();
        }

        [HttpPost]
        public JsonResult SalvarClienteTradeFaturamento(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<ClienteTradeFaturamentoDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new ClienteTradeFaturamentoBusiness().IncluirClienteTradeFaturamento(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        [HttpPost]
        public PartialViewResult ObterFaturamento(string strDados)
        {
            var retorno = new Retorno<ClienteTradeFaturamentoDTO>();

            var objDados = JsonConvert.DeserializeObject<ClienteTradeFaturamentoDTO>(strDados);

            try
            {
                retorno = new ClienteTradeFaturamentoBusiness().GetById(objDados.Id);
                if (retorno.Data.Id > 1)
                {
                    ViewBag.idClienteTradeFaturamento = retorno.Data.Id;
                    ViewBag.Inicio = retorno.Data.Inicio;
                    ViewBag.Termino = retorno.Data.Termino;
                    ViewBag.Valor = retorno.Data.Valor;
                }
                else
                {
                    ViewBag.idWeekendLeagueJogo = 0;
                    ViewBag.Inicio = null;
                    ViewBag.Termino = null;
                    ViewBag.Valor = null;
                }

            }
            catch (Exception ex)
            {
                ViewBag.idWeekendLeagueJogo = 0;
                ViewBag.Inicio = null;
                ViewBag.Termino = null;
                ViewBag.Valor = null;
            }

            return PartialView();
        }

        #endregion
    }
}