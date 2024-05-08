using Business;
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
    [AuthorizationUsuario]
    public class InscritoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Lista(string strDados)
        {
            var retorno = new InscritosStreamerBusiness().ObterInscritosPorUsuario(UsuarioLogado.Usuario.Id);

            if (retorno.Sucesso)
            {
                ViewBag.lstInscrito = retorno.Data;
                ViewBag.UsuarioAtivo = retorno.Data.Count(c => c.Ativo);
                ViewBag.UsuarioInativo = retorno.Data.Count(c => !c.Ativo);
            }
            else
            {
                ViewBag.lstInscrito = null;
            }

            return PartialView();
        }

        public JsonResult Importar(string strDados)
        {
            var jsonResult = new JsonResult();
            var nomeDocumento = string.Empty;
            var retorno = new Retorno();

            try
            {
                foreach (string fileName in Request.Files)
                {
                    nomeDocumento = SalvarDocumento(Request.Files[fileName], "Inscritos/Importado");
                }

                if (!string.IsNullOrEmpty(nomeDocumento))
                {

                    var originalDirectory = new DirectoryInfo($"{Server.MapPath("~/Arquivos/")}\\" + "Inscritos/Importado");

                    var pathString = Path.Combine(originalDirectory.ToString(), nomeDocumento);

                    new InscritosStreamerBusiness().ProcessarInscritos(pathString, UsuarioLogado.Usuario.Id);

                    //new BllBudgetPlanejamento().SalvarPlanejamentoDadosImportados(Security.ValidaSessao(Request), objDados);

                    retorno.Mensagem = "Arquivo Importado com sucesso";
                    retorno.Sucesso = true;
                }
                else
                {
                    retorno.Mensagem = "Erro ao importar arquivo";
                    retorno.Sucesso = false;
                }

            }
            catch (Exception e)
            {
                retorno.Mensagem = "Erro ao importar arquivo";
                retorno.Sucesso = false;
            }

            jsonResult.Data = retorno;

            return jsonResult;
        }
    }
}