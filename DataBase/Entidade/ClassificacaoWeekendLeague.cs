using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class ClassificacaoWeekendLeague : BaseEntidade
    {
        public int IdVersao { get; set; }

        [ForeignKey("IdVersao")]
        public virtual Versao Versao { get; set; }

        public string Nome { get; set; }

        public int NumeroVitoria { get; set; }

        public string Foto { get; set; }
    }
}
