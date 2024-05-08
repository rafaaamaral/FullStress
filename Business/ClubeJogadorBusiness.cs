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
    public class ClubeJogadorBusiness: BaseBusiness<ClubeJogadorDTO>
    {
        private readonly ClubeJogadorRepositorio _clubeJogadorRepositorio;

        public ClubeJogadorBusiness()
        {
            base.EntityType = typeof(ClubeJogador);
            base.RepositoryType = typeof(ClubeJogadorRepositorio);
            base.Includes = new[] { "Clube", "Jogador", "Jogador.TipoJogador" };
            _clubeJogadorRepositorio = new ClubeJogadorRepositorio();
        }

        public Retorno<List<ClubeJogadorDTO>> ObterJogadorPorClube(int idClube, int idUsuario)
        {
            var retorno = new Retorno<List<ClubeJogadorDTO>>();

            try
            {
                var clube = _clubeJogadorRepositorio.ObterJogadorPorClube(idClube);
                retorno.Data = Mapper.DynamicMap<List<ClubeJogadorDTO>>(clube);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Jogador não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirClubeJogador(ClubeJogadorDTO clube, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var jogadorVinculado = _clubeJogadorRepositorio.Get(w => w.IdClube == clube.IdClube && w.IdJogador == clube.IdJogador);

                if(jogadorVinculado.Count() > 0)
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Jogador já vinculado ao clube";
                    return retorno;
                }

                var clubeDTO = ObterDTO(clube.IdClube, clube.IdJogador);

                var entidade = _clubeJogadorRepositorio.Save(Mapper.DynamicMap<ClubeJogador>(clubeDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Jogador incluido no clube com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao vincular jogador ao clube";
            }

            return retorno;
        }

        public Retorno<int> AtivarInativarClubeJogador(ClubeJogadorDTO clube, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _clubeJogadorRepositorio.AtivarInativarClubeJogador(clube.Id, clube.Ativo);

                retorno.Data = 1;
                retorno.Mensagem = "Jogador ativado/Inativado com Sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao inativar Jogador";
            }

            return retorno;
        }

        private ClubeJogadorDTO ObterDTO(int idClube, int idJogador)
        {
            return new ClubeJogadorDTO()
            {
                IdClube = idClube,
                IdJogador = idJogador,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
