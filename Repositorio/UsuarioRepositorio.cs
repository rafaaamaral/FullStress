using DataBase;
using DataBase.Entidade;
using Global.Criptografias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>
    {
        public Usuario BuscarUsuarioPorId(int idUsuario)
        {
            using (var contexto = new Context())
            {
                return contexto.Usuario.FirstOrDefault(f => f.Id == idUsuario);
            }
        }

        public Usuario BuscarUsuarioPorLogin(string login)
        {
            using (var contexto = new Context())
            {
                return contexto.Usuario.FirstOrDefault(f => f.Login.ToUpper() == login.ToUpper());
            }
        }

        public void AlterarUsuario(int idUsuario, string nome, string psnID, string login, int idPerfil)
        {
            using (var contexto = new Context())
            {
                var usuario = contexto.Usuario.FirstOrDefault(f => f.Id == idUsuario);

                usuario.Nome = nome;
                usuario.Login = login;
                usuario.IdPerfil = idPerfil;

                contexto.SaveChanges();
            }
        }

        public void AlterarMeusDados(int idUsuario, string nome, string psnID, string login, string fotoPerfil, string senha)
        {
            using (var contexto = new Context())
            {
                var usuario = contexto.Usuario.FirstOrDefault(f => f.Id == idUsuario);

                usuario.Nome = nome;
                usuario.Login = login;
                usuario.FotoPerfil = fotoPerfil;
                usuario.Senha = string.IsNullOrEmpty(senha) ? usuario.Senha : Criptografia.Criptografar(senha);

                contexto.SaveChanges();
            }
        }

        public void AtivarDesativarUsuario(int idUsuario)
        {
            using (var contexto = new Context())
            {
                var usuario = contexto.Usuario.FirstOrDefault(f => f.Id == idUsuario);

                usuario.Ativo = !usuario.Ativo;
                usuario.DataDesativado = usuario.Ativo ? new Nullable<DateTime>() : DateTime.Now;

                contexto.SaveChanges();
            }
        }

        public Usuario Logar(string login, string senha)
        {
            using (var contexto = new Context())
            {
                var senhaCripto = Criptografia.Criptografar(senha);
                var usuario = contexto.Usuario.Include("Perfil").FirstOrDefault(f => f.Login == login && f.Senha == senhaCripto);

                return usuario;
            }
        }

        public Usuario ObterSessaoAtiva(string sessao)
        {
            using (var contexto = new Context())
            {
                var entidade = contexto.Sessao.FirstOrDefault(f => f.Codigo == sessao);

                entidade.UltimaMovimentacao = DateTime.Now;

                contexto.SaveChanges();

                var usuario = contexto.Sessao.Include("Usuario").FirstOrDefault(f => f.Codigo == sessao && f.SessaoAtiva)
                    .Usuario;

                return usuario;
            }
        }
    }
}
