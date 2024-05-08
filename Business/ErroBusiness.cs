using AutoMapper;
using DataBase.Entidade;
using DTO;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ErroBusiness : BaseBusiness<ErroDTO>
    {
        private readonly ErroRepositorio _erroRepositorio;

        public ErroBusiness()
        {
            EntityType = typeof(Erro);
            base.RepositoryType = typeof(ErroRepositorio);
            _erroRepositorio = new ErroRepositorio();
        }

        public void IncluirErro(int idUsuario, string rastro, string mensagem)
        {
            var dtoErro = ObterDTO(idUsuario, rastro, mensagem);
            var entidade = _erroRepositorio.Save(Mapper.DynamicMap<Erro>(dtoErro));
        }

        private ErroDTO ObterDTO(int idUsuario, string rastro, string mensagem)
        {
            return new ErroDTO()
            {
                IdUsuario = idUsuario,
                Rastro = rastro,
                Mensagem = mensagem,
                Ativo = true,
                DataCadastro = DateTime.Now,

            };
        }
    }
}
