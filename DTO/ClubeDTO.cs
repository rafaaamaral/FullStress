using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClubeDTO : BaseDTO
    {
        public string Nome { get; set; }
        public int IdUsuario { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public int IdVersao { get; set; }
        public VersaoDTO Versao { get; set; }
    }
}
