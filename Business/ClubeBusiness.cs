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
    public class ClubeBusiness : BaseBusiness<ClubeDTO>
    {
        private readonly ClubeRepositorio _clubeRepositorio;

        public ClubeBusiness()
        {
            base.EntityType = typeof(Clube);
            base.RepositoryType = typeof(ClubeRepositorio);
            base.Includes = new[] { "Usuario" };
            _clubeRepositorio = new ClubeRepositorio();
        }

        public Retorno<List<ClubeDTO>> ObterClubePorUsuarioVersao(int idUsuario)
        {
            var retorno = new Retorno<List<ClubeDTO>>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var clube = _clubeRepositorio.ObterClubePorUsuario(idUsuario, versao.Id);
                retorno.Data = Mapper.DynamicMap<List<ClubeDTO>>(clube);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirClube(ClubeDTO clube, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var clubeDTO = ObterDTO(clube.Nome, idUsuario);

                var entidade = _clubeRepositorio.Save(Mapper.DynamicMap<Clube>(clubeDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Clube incluido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir Clube";
            }

            return retorno;
        }

        public Retorno<int> AlterarClube(ClubeDTO clube, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _clubeRepositorio.AlterarClube(clube.Id, clube.Nome);

                retorno.Data = 1;
                retorno.Mensagem = "Clube alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar clube";
            }

            return retorno;
        }

        private ClubeDTO ObterDTO(string nome, int idUsuario)
        {
            return new ClubeDTO()
            {
                Nome = nome,
                IdUsuario = idUsuario,
                IdVersao = new VersaoBusiness().ObterVersaoAtiva(idUsuario).Data.Id,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
