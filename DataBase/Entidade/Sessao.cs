using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Sessao : BaseEntidade
    {
        [Required]
        public string Codigo { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [Required]
        public DateTime InicioSessao { get; set; }

        public DateTime? TerminoSessao { get; set; }

        [Required]
        public bool SessaoAtiva { get; set; }

        public DateTime? UltimaMovimentacao { get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        public bool Mobile { get; set; }

        [Required]
        public string Browser { get; set; }

        [Required]
        public string Dispositivo { get; set; }

        [Required]
        public string UserAgent { get; set; }
    }
}
