using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ErroDTO : BaseDTO
    {
        public string Rastro { get; set; }
        public string Mensagem { get; set; }
        public int IdUsuario { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
    }
}
