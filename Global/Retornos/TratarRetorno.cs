using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Global.Retornos
{
    public class TratarRetorno
    {
        private const string Message = "Erro ao processar a requisição.";

        public static Retorno LogError(Exception exception)
        {
            var retorno = new Retorno();
            var message = CheckErrorMessage(exception);
            retorno.Sucesso = false;
            retorno.Mensagem = message;

            return retorno;
        }

        public static Retorno<T> LogError<T>(Exception exception)
        {
            var retorno = new Retorno<T>();
            var message = CheckErrorMessage(exception);
            retorno.Sucesso = false;
            retorno.Mensagem = message;

            return retorno;
        }

        public static string CheckErrorMessage(Exception exception)
        {
            if (exception.GetType() == typeof(DbEntityValidationException))
                return CheckDbEntityValidationException(exception);

            if (exception.GetType() == typeof(DbUpdateException))
                return CheckDbUpdateException(exception);

            if (exception is TargetInvocationException && exception.InnerException != null)
            {
                if (exception.InnerException.GetType() == typeof(DbEntityValidationException))
                    return CheckDbEntityValidationException(exception.InnerException);

                if (exception.InnerException.GetType() == typeof(DbUpdateException))
                    return CheckDbUpdateException(exception.InnerException);
            }

            return Message;
        }

        private static string CheckDbUpdateException(Exception exception)
        {
            var dbUpdateException = exception as DbUpdateException;

            if (dbUpdateException != null && dbUpdateException.InnerException != null && dbUpdateException.InnerException.InnerException != null)
            {
                if (dbUpdateException.InnerException.InnerException.ToString().ToUpper().Contains("IX_EMAIL"))
                    return "E-mail já cadastrado";

                if (dbUpdateException.InnerException.InnerException.ToString().ToUpper().Contains("IX_CPF"))
                    return "CPF já cadastrado";
            }

            return Message;
        }

        private static string CheckDbEntityValidationException(Exception exception)
        {
            var dbEntityValidationException = exception as DbEntityValidationException;
            var message = dbEntityValidationException != null ? string.Join("<br />", dbEntityValidationException.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage)) : Message;

            return message;
        }
    }
}
