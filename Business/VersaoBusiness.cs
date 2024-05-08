using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class VersaoBusiness: BaseBusiness<VersaoDTO>
    {
        private readonly VersaoRepositorio _versaoRepositorio;

        public VersaoBusiness()
        {
            base.EntityType = typeof(Versao);
            base.RepositoryType = typeof(VersaoRepositorio);
            base.Includes = new string[] { };
            _versaoRepositorio = new VersaoRepositorio();
        }

        public Retorno<VersaoDTO> ObterVersaoAtiva(int idUsuario)
        {
            var retorno = new Retorno<VersaoDTO>();

            try
            {
                var usuario = _versaoRepositorio.ObterVersaoAtiva();
                retorno.Data = Mapper.DynamicMap<VersaoDTO>(usuario);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Versão não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirVersao(VersaoDTO versao, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var dtoVersao = ObterDTO(versao.Nome, versao.AtualProducao);
                var entidade = _versaoRepositorio.Save(Mapper.DynamicMap<Versao>(dtoVersao));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Versão incluida com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir Versão";
            }

            return retorno;
        }

        public Retorno<int> AlterarVersao(VersaoDTO versao, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _versaoRepositorio.AlterarVersao(versao.Id, versao.Nome, versao.AtualProducao);

                retorno.Data = 1;
                retorno.Mensagem = "Versão alterada com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar versão";
            }

            return retorno;
        }

        private VersaoDTO ObterDTO(string nome, bool atualProducao)
        {
            return new VersaoDTO()
            {
                Nome = nome,
                AtualProducao = atualProducao,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
