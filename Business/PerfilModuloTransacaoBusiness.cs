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
    public class PerfilModuloTransacaoBusiness: BaseBusiness<PerfilModuloTransacaoDTO>
    {
        private readonly PerfilModuloTransacaoRepositorio _perfilModuloTransacaoRepositorio;

        public PerfilModuloTransacaoBusiness()
        {
            base.EntityType = typeof(PerfilModuloTransacao);
            base.RepositoryType = typeof(PerfilModuloTransacaoRepositorio);
            _perfilModuloTransacaoRepositorio = new PerfilModuloTransacaoRepositorio();
        }

        public Retorno<List<PerfilModuloTransacaoDTO>> BuscarTransacaoPorPerfil(int idPerfil, int idUsuario)
        {
            var retorno = new Retorno<List<PerfilModuloTransacaoDTO>>();

            try
            {
                var retornoRepositorio = _perfilModuloTransacaoRepositorio.BuscarTransacaoPorPerfil(idPerfil);
                retorno.Data = Mapper.DynamicMap<List<PerfilModuloTransacaoDTO>>(retornoRepositorio);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Transacao não encontrada!";
            }

            return retorno;
        }

        public Retorno IncluirPerfilModuloTransacao(int idPerfil, int idModuloTransacao, int idUsuario)
        {
            var retorno = new Retorno();

            try
            {
                var dtoPerfilModuloTransacao = ObterDTO(idPerfil, idModuloTransacao);
                _perfilModuloTransacaoRepositorio.Save(Mapper.DynamicMap<PerfilModuloTransacao>(dtoPerfilModuloTransacao));

                retorno.Mensagem = "Transacao incluida com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir transacao";
            }

            return retorno;
        }

        private PerfilModuloTransacaoDTO ObterDTO(int idPerfil, int idModuloTransacao)
        {
            return new PerfilModuloTransacaoDTO()
            {
                IdPerfil = idPerfil,
                IdModuloTransacao = idModuloTransacao,
                Ativo = true,
                DataCadastro = DateTime.Now,

            };
        }
    }
}
