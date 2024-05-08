using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class UsuarioMaquina
    {
        public string Maquina { get; set; }
        public string Browser { get; set; }
        public bool Mobile { get; set; }
        public string Dispositivo { get; set; }
        public string UserAgent { get; set; }
    }

    public class Sessao
    {
        public string StrSessao { get; set; }
        public string NmVarSessao { get; set; }
        public int CdSite { get; set; }
        public int CdUsuario { get; set; }
        public string DsValorVarSessao { get; set; }
    }

    public class SessaoPesq
    {
        public string StrSessao { get; set; }
        public int CdSite { get; set; }
    }
}
