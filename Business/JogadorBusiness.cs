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
    public class JogadorBusiness : BaseBusiness<JogadorDTO>
    {
        private readonly JogadorRepositorio _jogadorRepositorio;

        public JogadorBusiness()
        {
            base.EntityType = typeof(Jogador);
            base.RepositoryType = typeof(JogadorRepositorio);
            base.Includes = new string[] { "TipoJogador" };
            _jogadorRepositorio = new JogadorRepositorio();
        }

        public Retorno<List<JogadorDTO>> ObterJogadorPorVersaoAtiva(int idUsuario)
        {
            var retorno = new Retorno<List<JogadorDTO>>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var jogador = _jogadorRepositorio.ObterJogadorPorVersaoAtiva(versao.Id);
                retorno.Data = Mapper.DynamicMap<List<JogadorDTO>>(jogador);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de jogador!";
            }

            return retorno;
        }

        public Retorno<int> IncluirJogador(JogadorDTO jogador, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var JogadorDTO = ObterDTO(jogador.Nome, jogador.Overral, jogador.Link, jogador.IdTipoJogador, idUsuario);

                var entidade = _jogadorRepositorio.Save(Mapper.DynamicMap<Jogador>(JogadorDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Jogador incluido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir Jogador";
            }

            return retorno;
        }

        public Retorno<int> AlterarJogador(JogadorDTO jogador, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _jogadorRepositorio.AlterarJogador(jogador.Id, jogador.Nome, jogador.Overral, jogador.Link, jogador.IdTipoJogador);

                retorno.Data = 1;
                retorno.Mensagem = "Jogador alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar Jogador";
            }

            return retorno;
        }

        private JogadorDTO ObterDTO(string nome, int overral, string link, int idTipoJogador, int idUsuario)
        {
            return new JogadorDTO()
            {
                Nome = nome.ToUpper(),
                Overral = overral,
                Link = link,
                IdTipoJogador = idTipoJogador,
                IdVersao = new VersaoBusiness().ObterVersaoAtiva(idUsuario).Data.Id,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
