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
    public class TipoJogadorBusiness: BaseBusiness<TipoJogadorDTO>
    {
        private readonly TipoJogadorRepositorio _tipoJogadorRepositorio;

        public TipoJogadorBusiness()
        {
            base.EntityType = typeof(TipoJogador);
            base.RepositoryType = typeof(TipoJogadorRepositorio);
            _tipoJogadorRepositorio = new TipoJogadorRepositorio();
        }

        public Retorno<List<TipoJogadorDTO>> ObterTipoJogadorPorVersaoAtiva(int idUsuario)
        {
            var retorno = new Retorno<List<TipoJogadorDTO>>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var TipoJogador = _tipoJogadorRepositorio.Get(w => w.Versao == versao);
                retorno.Data = Mapper.DynamicMap<List<TipoJogadorDTO>>(TipoJogador);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de Tipo de Jogador!";
            }

            return retorno;
        }
    }
}
