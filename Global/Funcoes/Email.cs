using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Global.Funcoes
{
    public class Email
    {

        #region Configuração

        protected SmtpClient GetSmptClient
        {
            get
            {
                var host = ConfigurationManager.AppSettings["SmtpServer"];

                int? porta = null;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpPort"]))
                    porta = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

                var usuarioSMTP = ConfigurationManager.AppSettings["SmtpUser"];
                var senhaSMTP = ConfigurationManager.AppSettings["SmtpPassword"];

                var client = new SmtpClient(host);

                if (porta.HasValue)
                    client.Port = porta.Value;

                var credential = new NetworkCredential(usuarioSMTP, senhaSMTP);
                client.Credentials = credential;

                return client;
            }
        }

        #endregion

        #region Envio de Email

        public void EnviarEmailEsqueciMinhaSenha(string corpoEmail, string email)
        {
            string emailTo = email;

            string emailFrom = ConfigurationManager.AppSettings["EmailSystem"];

            MailMessage mail = new MailMessage(emailFrom, emailTo);

            mail.Subject = "Recuperação de Senha";

            mail.Body = corpoEmail;

            mail.IsBodyHtml = true;
            GetSmptClient.Send(mail);
        }

        public void EnviarEmailChamadoUsuario(string corpoEmail, int idEmpresa)
        {
            var emailAtendimento = ObterEmailAtendimento(idEmpresa);

            string emailFrom = ConfigurationManager.AppSettings["EmailSystem"];

            MailMessage mail = new MailMessage(emailFrom, emailAtendimento);

            mail.Subject = "Chamado Trade App";

            mail.Body = corpoEmail;

            mail.IsBodyHtml = true;
            GetSmptClient.Send(mail);
        }

        public void EnviarEmailRespostaChamado(string corpoEmail, string email)
        {
            string emailFrom = ConfigurationManager.AppSettings["EmailSystem"];

            MailMessage mail = new MailMessage(emailFrom, email);

            mail.Subject = "Chamado Trade App";

            mail.Body = corpoEmail;

            mail.IsBodyHtml = true;
            GetSmptClient.Send(mail);
        }

        public void EnviarEmailUsuarioCadastro(string corpoEmail, string email)
        {
            string emailFrom = ConfigurationManager.AppSettings["EmailSystem"];

            MailMessage mail = new MailMessage(emailFrom, email);

            mail.Subject = "Bem Vindo ao TRADE APPS";

            mail.Body = corpoEmail;

            mail.IsBodyHtml = true;
            GetSmptClient.Send(mail);
        }

        public void EnviarEmailNotificacaoCompra(string corpoEmail, string email)
        {
            string emailFrom = ConfigurationManager.AppSettings["EmailSystem"];

            MailMessage mail = new MailMessage(emailFrom, email);

            mail.Subject = "Especificações de Pagamento";

            mail.Body = corpoEmail;

            mail.IsBodyHtml = true;
            GetSmptClient.Send(mail);
        }

        #endregion

        #region Obter Corpo de Email

        public string ObterCorpoEmailEsqueciMinhaSenha(string nome, string email, string login, string senha)
        {
            StringBuilder body = new StringBuilder();

            body.Append("Nome: " + nome);
            body.Append("<br />");
            body.Append("Email: " + email);
            body.Append("<br />");
            body.Append("Login: " + login);
            body.Append("<br />");
            body.Append("Senha: " + senha);
            body.Append("<br />");

            return body.ToString();
        }

        public string ObterCorpoEmailNovoCadastro(string nome, string login, string Senha)
        {
            StringBuilder body = new StringBuilder();
            body.Append("Olá, ");
            body.Append("<br />");
            body.Append("Um novo Usuario foi criado!");
            body.Append("<br />");
            body.Append("Por favor, acesse o Portal para Acessar!");
            body.Append("<br />");
            body.Append("Dados do Cadastro:");
            body.Append("<br />");
            body.Append("Nome: " + nome);
            body.Append("<br />");
            body.Append("Login: " + login);
            body.Append("<br />");
            body.Append("Senha: " + Senha);
            body.Append("<br />");
            body.Append("<br />");

            return body.ToString();
        }

        public string ObterCorpoEmailNotificacaoCompra(string Status)
        {
            StringBuilder body = new StringBuilder();
            body.Append("Olá, ");
            body.Append("<br />");
            body.Append("A Solicitação de Pagamento!");
            body.Append("<br />");
            body.Append("Por favor, acesse o Portal para Acessar!");
            body.Append("<br />");
            body.Append("Dados do Cadastro:");
            body.Append("<br />");
            body.Append("Status Pagamento: " + Status);
            body.Append("<br />");
            body.Append("<br />");

            return body.ToString();
        }

        public string ObterCorpoEmailNovoChamado(string nome, string email, string cpf, string descricao)
        {
            StringBuilder body = new StringBuilder();
            body.Append("Olá, ");
            body.Append("<br />");
            body.Append("Um novo chamado foi criado!");
            body.Append("<br />");
            body.Append("Por favor, acesse o Portal para visualizá-lo!");
            body.Append("<br />");
            body.Append("Dados do chamado:");
            body.Append("<br />");
            body.Append("Nome: " + nome);
            body.Append("<br />");
            body.Append("Email: " + email);
            body.Append("<br />");
            body.Append("CPF: " + Geral.FormatarCPF(cpf));
            body.Append("<br />");
            body.Append("Descrição: " + descricao);
            body.Append("<br />");

            return body.ToString();
        }

        public string ObterCorpoEmailRespostaChamadoUsuario(string nome, string email)
        {
            StringBuilder body = new StringBuilder();

            body.Append("Olá, ");
            body.Append("<br />");
            body.Append("O Chamado do usuário: " + nome + " Email: " + email + " foi atualizado!");
            body.Append("<br />");
            body.Append("Por favor, acesse o Portal para visualizá-lo!");

            return body.ToString();
        }

        #endregion

        #region Auxiliar

        private string ObterEmailAtendimento(int idEmpresa)
        {
            switch (idEmpresa)
            {
                default:
                    return "atendimentojbs@digipronto.com.br";
            }
        }

        #endregion
    }
}
