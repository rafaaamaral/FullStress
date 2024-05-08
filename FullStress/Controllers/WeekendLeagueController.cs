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
    public class WeekendLeagueController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new WeekendLeagueBusiness().ObterWeekendLeaguePorUsuarioEVersaoAtiva(UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstWeekendLeague = retorno.Data;
            }
            else
            {
                ViewBag.lstWeekendLeague = null;
            }

            return PartialView();
        }

        public ActionResult Alterar(int? id)
        {
            try
            {
                var _weekendLeagueBusiness = new WeekendLeagueBusiness();

                if (!id.HasValue)
                {
                    id = _weekendLeagueBusiness.IncluirWeekendLeague(UsuarioLogado.Usuario.Id).Data;
                }

                var retorno = new WeekendLeagueBusiness().GetById(id.HasValue ? id.Value : 0);

                if (retorno.Sucesso)
                {
                    ViewBag.idWeekendLeague = retorno.Data.Id;
                    ViewBag.descricao = retorno.Data.Descricao;
                    ViewBag.top100 = retorno.Data.Top100;
                    ViewBag.Status = retorno.Data.Status;
                    ViewBag.descricaoStatus = retorno.Data.DescricaoStatus;
                    ViewBag.descricaoClassificacao = retorno.Data.Classificacao.Nome;

                    var retornoJogo = new WeekendLeagueJogoBusiness().ObterJogoPorWL(retorno.Data.Id, UsuarioLogado.Usuario.Id);

                    if (retornoJogo.Sucesso)
                    {
                        ViewBag.quantidadeJogo = retornoJogo.Data.Count();
                        ViewBag.quantidadeVitoria = retornoJogo.Data.Count(c => c.Vitoria);
                    }
                    else
                    {
                        ViewBag.quantidadeJogo = 0;
                        ViewBag.quantidadeVitoria = 0;
                    }

                }
                else
                {
                    ViewBag.idWeekendLeague = 0;
                    ViewBag.descricao = null;
                    ViewBag.Status = null;
                    ViewBag.top100 = false;
                    ViewBag.descricaoStatus = null;
                    ViewBag.descricaoClassificacao = null;
                    ViewBag.quantidadeJogo = 0;
                    ViewBag.quantidadeVitoria = 0;
                }
            }
            catch (Exception e)
            {
                ViewBag.idWeekendLeague = 0;
                ViewBag.descricao = null;
                ViewBag.Status = null;
                ViewBag.top100 = false;
                ViewBag.descricaoStatus = null;
                ViewBag.descricaoClassificacao = null;
                ViewBag.quantidadeJogo = 0;
                ViewBag.quantidadeVitoria = 0;
            }

            return View();
        }

        [HttpPost]
        public JsonResult SalvarWeekendLeague(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<WeekendLeagueDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new WeekendLeagueBusiness().AlterarWeekendLeague(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        [HttpPost]
        public JsonResult ExcluirWeekendLeague(string strDados)
        {
            var retorno = new Retorno();

            var objDados = JsonConvert.DeserializeObject<WeekendLeagueDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new WeekendLeagueBusiness().Remove(objDados.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        public ActionResult ResumoWeekendLeague(int id)
        {
            return View();
        }

        #region Weekend League Jogo

        public PartialViewResult ListaJogo(string strDados)
        {
            var objDados = JsonConvert.DeserializeObject<WeekendLeagueDTO>(strDados);

            var retorno = new WeekendLeagueJogoBusiness().ObterJogoPorWL(objDados.Id, UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstJogo = retorno.Data;
            }
            else
            {
                ViewBag.lstJogo = null;
            }

            return PartialView();
        }

        public PartialViewResult ListaJogoDetalhe(string strDados)
        {
            var objDados = JsonConvert.DeserializeObject<WeekendLeagueJogoDTO>(strDados);

            var retorno = new WeekendLeagueJogoDetalheBusiness().ObterJogoDetalhePorJogo(objDados.Id, UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstJogo = retorno.Data;
            }
            else
            {
                ViewBag.lstJogo = null;
            }

            return PartialView();
        }

        [HttpPost]
        public JsonResult SalvarWeekendLeagueJogo(string strDados)
        {
            var retorno = new Retorno<int>();

            var objDados = JsonConvert.DeserializeObject<WeekendLeagueJogoDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new WeekendLeagueJogoBusiness().ProcessarWeekendLeagueJogo(objDados, UsuarioLogado.Usuario.Id);
            }
            catch (Exception ex)
            {
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }

        [HttpPost]
        public PartialViewResult ObterJogo(string strDados)
        {
            var retorno = new Retorno<WeekendLeagueJogoDTO>();

            var objDados = JsonConvert.DeserializeObject<WeekendLeagueJogoDTO>(strDados);

            try
            {
                retorno = new WeekendLeagueJogoBusiness().ObterJogoPorId(objDados.Id, UsuarioLogado.Usuario.Id);

                var retornoWL = new WeekendLeagueBusiness().GetById(objDados.IdWeekendLeague);

                var lstRetornoClubeJogador = new ClubeJogadorBusiness().ObterJogadorPorClube(retornoWL.Data.IdClube, UsuarioLogado.Usuario.Id);

                if (lstRetornoClubeJogador.Sucesso)
                {
                    ViewBag.lstJogador = lstRetornoClubeJogador.Data.Where(w => w.Ativo).Select(s => s.Jogador);
                }
                else
                {
                    ViewBag.lstJogador = null;
                }

                if (retorno.Data.Id > 1)
                {
                    ViewBag.idWeekendLeagueJogo = retorno.Data.Id;
                    ViewBag.golsFavor = retorno.Data.GolsFavor;
                    ViewBag.golsContra = retorno.Data.GolsContra;
                    ViewBag.psnAdversario = retorno.Data.PsnAdversario;
                    ViewBag.precisaoPasses = retorno.Data.PrecisaoPasses;
                    ViewBag.possebola = retorno.Data.PosseDeBola;
                    ViewBag.idJogadorMelhorCampo = retorno.Data.IdJogadorMelhorCampo;
                    ViewBag.quitRage = retorno.Data.QuitRage;
                    ViewBag.observacao = retorno.Data.Observacao;
                }
                else
                {
                    ViewBag.idWeekendLeagueJogo = 0;
                    ViewBag.golsFavor = null;
                    ViewBag.golsContra = null;
                    ViewBag.psnAdversario = null;
                    ViewBag.precisaoPasses = null;
                    ViewBag.possebola = null;
                    ViewBag.idJogadorMelhorCampo = null;
                    ViewBag.quitRage = false;
                    ViewBag.observacao = null;
                }

            }
            catch (Exception ex)
            {
                ViewBag.idWeekendLeagueJogo = 0;
                ViewBag.golsFavor = null;
                ViewBag.golsContra = null;
                ViewBag.psnAdversario = null;
                ViewBag.precisaoPasses = null;
                ViewBag.possebola = null;
                ViewBag.idJogadorMelhorCampo = null;
                ViewBag.quitRage = false;
                ViewBag.observacao = null;
            }

            return PartialView();
        }

        [HttpPost]
        public JsonResult ExcluirWeekendLeagueJogo(string strDados)
        {
            var retorno = new Retorno();

            var objDados = JsonConvert.DeserializeObject<WeekendLeagueJogoDTO>(strDados);

            var jsonResult = new JsonResult();
            jsonResult.Data = false;

            try
            {
                retorno = new WeekendLeagueJogoBusiness().ExcluirWeekendLeagueJogo(objDados, UsuarioLogado.Usuario.Id);
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