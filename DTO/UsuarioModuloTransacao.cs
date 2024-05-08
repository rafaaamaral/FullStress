using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioModuloTransacao : BaseDTO
    {
        public int IdUsuario { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public int IdModuloTransacao { get; set; }
        public virtual ModuloTransacaoDTO ModuloTransacao { get; set; }
    }
}
