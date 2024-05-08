using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sessao = DataBase.Entidade.Sessao;

namespace Business
{
    public class SessaoBusiness : BaseBusiness<SessaoDTO>
    {
        private readonly SessaoRepositorio _sessaoRepositorio;

        public SessaoBusiness()
        {
            base.EntityType = typeof(Sessao);
            base.RepositoryType = typeof(SessaoRepositorio);
            base.Includes = new[] { "Usuario" };
            _sessaoRepositorio = new SessaoRepositorio();
        }

        public Retorno<string> IncluirSessao(UsuarioDTO usuario, UsuarioMaquina usuarioMaquina)
        {
            var retorno = new Retorno<string>();

            try
            {
                InativarSessao(usuario);


                var codigoSessao = Guid.NewGuid().ToString();

                var dtoSessao = ObterDTO(usuario.Id, usuarioMaquina.Maquina, usuarioMaquina.Mobile, usuarioMaquina.Browser, 
                    usuarioMaquina.Dispositivo, usuarioMaquina.UserAgent, codigoSessao);
                var entidade = _sessaoRepositorio.Save(Mapper.DynamicMap<Sessao>(dtoSessao));

                retorno.Data = entidade.Codigo;
                retorno.Mensagem = "Sessão incluida com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(usuario.Id, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir sessão";
            }

            return retorno;
        }

        public void InativarSessao(UsuarioDTO usuario)
        {
            _sessaoRepositorio.InativarSessaoAntiga(usuario.Id);
        }

        private SessaoDTO ObterDTO(int idUsuario, string ip, bool mobile, string browser, string dispositivo, string userAgent, string codigoSessao)
        {
            return new SessaoDTO
            {
                Ativo = true,
                Browser = browser,
                Codigo = codigoSessao,
                DataCadastro = DateTime.Now,
                Dispositivo = dispositivo,
                IdUsuario = idUsuario,
                InicioSessao = DateTime.Now,
                IP = ip,
                Mobile = mobile,
                SessaoAtiva = true,
                UserAgent = userAgent
            };
        }
    }
}
