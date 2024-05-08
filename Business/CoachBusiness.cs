using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Enum;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CoachBusiness : BaseBusiness<CoachDTO>
    {
        private readonly CoachRepositorio _coachRepositorio;

        public CoachBusiness()
        {
            base.EntityType = typeof(Coach);
            base.RepositoryType = typeof(CoachRepositorio);
            base.Includes = new[] { "Player" };
            _coachRepositorio = new CoachRepositorio();
        }

        public Retorno<List<CoachDTO>> ObterCoachPorUsuario(int idUsuario)
        {
            var retorno = new Retorno<List<CoachDTO>>();

            try
            {
                var coach = _coachRepositorio.ObterListaCoachPorUsuario(idUsuario);
                retorno.Data = Mapper.DynamicMap<List<CoachDTO>>(coach);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Coach não encontrado!";
            }

            return retorno;
        }

        public Retorno<List<CoachDTO>> ObterCoachPorStatus(int? idStatus, int idUsuario)
        {
            var retorno = new Retorno<List<CoachDTO>>();

            try
            {
                var coach = _coachRepositorio.ObterCoachPorStatus(idStatus);
                retorno.Data = Mapper.DynamicMap<List<CoachDTO>>(coach);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Coach não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirCoach(CoachDTO coach, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var clubeDTO = ObterDTO(coach.Titulo, coach.ObservacaoPlayer, coach.LinkVideo , idUsuario);

                var entidade = _coachRepositorio.Save(Mapper.DynamicMap<Coach>(clubeDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Coach incluído com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir Coach";
            }

            return retorno;
        }

        public Retorno<int> ResponderCoach(CoachDTO coach, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _coachRepositorio.ResponderCoach(coach.Id, coach.FeedbackCoach);

                retorno.Data = 1;
                retorno.Mensagem = "Coach respondido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao responder coach";
            }

            return retorno;
        }

        private CoachDTO ObterDTO(string titulo, string observacao, string linkVideo, int idUsuario)
        {
            return new CoachDTO()
            {
                IdPlayer = idUsuario,
                ObservacaoPlayer = observacao,
                LinkVideo = linkVideo,
                Status = (int)StatusCoach.Solicitado,
                Titulo = titulo,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
