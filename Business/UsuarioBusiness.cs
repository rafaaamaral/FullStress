using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global;
using Global.Criptografias;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioBusiness : BaseBusiness<UsuarioDTO>
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private const string senhaInicial = "123456";
        public UsuarioBusiness()
        {
            base.EntityType = typeof(Usuario);
            base.RepositoryType = typeof(UsuarioRepositorio);
            base.Includes = new[] { "Perfil" };
            _usuarioRepositorio = new UsuarioRepositorio();
        }

        public Retorno<UsuarioDTO> BuscarUsuarioPorId(int idUsuario)
        {
            var retorno = new Retorno<UsuarioDTO>();

            try
            {
                var usuario = _usuarioRepositorio.BuscarUsuarioPorId(idUsuario);
                retorno.Data = Mapper.DynamicMap<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário não encontrado!";
            }

            return retorno;
        }

        public Retorno<UsuarioDTO> BuscarUsuarioPorLogin(int idUsuario, string login)
        {
            var retorno = new Retorno<UsuarioDTO>();

            try
            {
                var usuario = _usuarioRepositorio.BuscarUsuarioPorLogin(login);
                retorno.Data = Mapper.DynamicMap<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário não encontrado pelo Login!";
            }

            return retorno;
        }

        public Retorno<int> IncluirUsuario(UsuarioDTO usuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var dtoUsuario = ObterDTO(usuario.Nome, usuario.PsnID, usuario.Login, senhaInicial, usuario.IdPerfil, "avatarpadrao.png");
                var entidade = _usuarioRepositorio.Save(Mapper.DynamicMap<Usuario>(dtoUsuario));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Usuario incluido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(usuario.Id, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir usuario";
            }

            return retorno;
        }

        public Retorno<int> AlterarUsuario(UsuarioDTO usuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _usuarioRepositorio.AlterarUsuario(usuario.Id, usuario.Nome, usuario.PsnID, usuario.Login, usuario.IdPerfil);

                retorno.Data = 1;
                retorno.Mensagem = "Usuario alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(usuario.Id, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar usuario";
            }

            return retorno;
        }

        public Retorno AlterarMeusDados(UsuarioDTO usuario)
        {
            var retorno = new Retorno();

            try
            {

                _usuarioRepositorio.AlterarMeusDados(usuario.Id, usuario.Nome, usuario.PsnID, usuario.Login, usuario.FotoPerfil, usuario.Senha);

                retorno.Mensagem = "Meus Dados alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(usuario.Id, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar Meus Dados!";
            }

            return retorno;
        }

        public Retorno AtivarDesativarUsuario(int idUsuario)
        {
            var retorno = new Retorno();

            try
            {
                _usuarioRepositorio.AtivarDesativarUsuario(idUsuario);
                retorno.Mensagem = "Status do usuario alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar status do Usuario";
            }

            return retorno;
        }

        public Retorno<UsuarioDTO> Logar(string usuario, string senha)
        {
            var retorno = new Retorno<UsuarioDTO>();

            try
            {
                var usuarioLogado = _usuarioRepositorio.Logar(usuario, senha);
                retorno.Data = Mapper.DynamicMap<UsuarioDTO>(usuarioLogado);

                if (retorno.Data != null)
                {
                    ObterPerfilModuloTransacao(retorno);
                }
                else
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Usuário e senha não correspondem às informações em nossos registros. Tente Novamente";
                }
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário e senha não correspondem às informações em nossos registros. Tente Novamente";
            }

            return retorno;
        }

        public Retorno<UsuarioDTO> BuscarUsuarioSessao(string sessao)
        {
            var retorno = new Retorno<UsuarioDTO>();

            try
            {
                var usuarioLogado = _usuarioRepositorio.ObterSessaoAtiva(sessao);
                retorno.Data = Mapper.DynamicMap<UsuarioDTO>(usuarioLogado);

                if (retorno.Data != null)
                {
                    ObterPerfilModuloTransacao(retorno);
                }
                else
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Usuário e senha não correspondem às informações em nossos registros. Tente Novamente";
                }
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário e senha não correspondem às informações em nossos registros. Tente Novamente";
            }

            return retorno;
        }

        private string CriarSessao(UsuarioDTO usuario, UsuarioMaquina usuarioMaquina)
        {
            var retorno = new SessaoBusiness().IncluirSessao(usuario, usuarioMaquina);

            if (retorno.Sucesso)
                return retorno.Data;

            return string.Empty;
        }

        private void ObterPerfilModuloTransacao(Retorno<UsuarioDTO> retorno)
        {
            var retornoPerfilModuloTransacao = new PerfilModuloTransacaoBusiness().BuscarTransacaoPorPerfil(retorno.Data.IdPerfil, retorno.Data.Id);

            if (retornoPerfilModuloTransacao.Sucesso)
                retorno.Data.PerfilModuloTransacao = retornoPerfilModuloTransacao.Data;
        }

        private UsuarioDTO ObterDTO(string nome, string psnID, string login, string senha, int idPerfil, string fotoPerfil)
        {
            return new UsuarioDTO()
            {
                Nome = nome,
                PsnID = psnID,
                Login = login,
                Senha = Criptografia.Criptografar(senha),
                IdPerfil = idPerfil,
                FotoPerfil = fotoPerfil,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
