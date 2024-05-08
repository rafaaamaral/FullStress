using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SessaoDTO : BaseDTO
    {
        public string Codigo { get; set; }
        public int IdUsuario { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public DateTime InicioSessao { get; set; }
        public DateTime? TerminoSessao { get; set; }
        public bool SessaoAtiva { get; set; }
        public DateTime? UltimaMovimentacao { get; set; }
        public string IP { get; set; }
        public bool Mobile { get; set; }
        public string Browser { get; set; }
        public string Dispositivo { get; set; }
        public string UserAgent { get; set; }
    }
}
