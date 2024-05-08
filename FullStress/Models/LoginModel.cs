using Business;
using DTO;
using Global;
using Global.Retornos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FullStress.Models
{
    public class LoginModel
    {
        [DisplayName("Login")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        public string Senha { get; set; }

        [DisplayName("NovaSenha")]
        public string NovaSenha { get; set; }

        [DisplayName("ConfirmaSenha")]
        public string ConfirmaSenha { get; set; }

        [DisplayName("CPF")]
        public string CPF { get; set; }

        public UsuarioDTO Usuario { get; set; }

        //public UsuarioPlanoDto UsuarioPlano { get; set; }

        //public Retorno<UsuarioDTO> RecuperarSenha(string cpf)
        //{
        //    using (var cliente = new UsuarioService.UsuarioWebServiceClient())
        //    {
        //        var retorno = cliente.RecuperarSenha(cpf);
        //        return retorno;
        //    }
        //}       

        public Retorno<UsuarioDTO> ObterUsuario(string login, string senha)
        {
            var retorno = new Retorno<UsuarioDTO>();

            retorno = new UsuarioBusiness().Logar(login, senha);

            if (retorno.Sucesso)
                Usuario = retorno.Data;

            return retorno;

        }
    }
}