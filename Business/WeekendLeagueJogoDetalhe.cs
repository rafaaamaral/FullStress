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
    public class WeekendLeagueJogoDetalheBusiness : BaseBusiness<WeekendLeagueJogoDetalheDTO>
    {
        private readonly WeekendLeagueJogoDetalheRepositorio _weekendLeagueJogoDetalheRepositorio;

        public WeekendLeagueJogoDetalheBusiness()
        {
            base.EntityType = typeof(WeekendLeagueJogoDetalhe);
            base.RepositoryType = typeof(WeekendLeagueJogoDetalheRepositorio);
            base.Includes = new[] { "WeekendLeagueJogo", "JogadorGol", "JogadorAssistencia" };
            _weekendLeagueJogoDetalheRepositorio = new WeekendLeagueJogoDetalheRepositorio();
        }

        public Retorno<int> IncluirWeekendLeagueJogoDetalhe(WeekendLeagueJogoDetalheDTO objDados, int idUsuario, int idWeekendLeagueJogo)
        {
            var retorno = new Retorno<int>();

            try
            {
                var JogadorDTO = ObterDTO(objDados.IdJogadorGol, objDados.QuantidadeGol, objDados.IdJogadorAssistencia, objDados.QuantidadeAssistencia, idUsuario, idWeekendLeagueJogo);

                var entidade = _weekendLeagueJogoDetalheRepositorio.Save(Mapper.DynamicMap<WeekendLeagueJogoDetalhe>(JogadorDTO));

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

        public Retorno<List<WeekendLeagueJogoDetalheDTO>> ObterJogoDetalhePorJogo(int idJogo, int idUsuario)
        {
            var retorno = new Retorno<List<WeekendLeagueJogoDetalheDTO>>();

            try
            {
                var detalhe = _weekendLeagueJogoDetalheRepositorio.ObterJogoDetalhePorJogo(idJogo);
                retorno.Data = Mapper.DynamicMap<List<WeekendLeagueJogoDetalheDTO>>(detalhe);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de jogos!";
            }

            return retorno;
        }

        private WeekendLeagueJogoDetalheDTO ObterDTO(int? idJogadorGol, int? quantidadeGol, int? idJogadorAssistencia, int? quantidadeAssistencia, int idUsuario, int idWeekendLeagueJogo)
        {
            return new WeekendLeagueJogoDetalheDTO
            {
                IdJogadorAssistencia = idJogadorAssistencia,
                IdJogadorGol = idJogadorGol,
                IdWeekenLeagueJogo = idWeekendLeagueJogo,
                QuantidadeAssistencia = quantidadeAssistencia,
                QuantidadeGol = quantidadeGol,
                Ativo = true,
                DataCadastro = DateTime.Now
            };
        }
    }
}
