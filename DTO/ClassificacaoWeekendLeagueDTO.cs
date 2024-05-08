using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClassificacaoWeekendLeagueDTO : BaseDTO
    {
        public int IdVersao { get; set; }
        public virtual VersaoDTO Versao { get; set; }
        public string Nome { get; set; }
        public int NumeroVitoria { get; set; }
        public string Foto { get; set; }
    }
}
