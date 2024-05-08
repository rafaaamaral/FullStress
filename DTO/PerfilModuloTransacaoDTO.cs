using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PerfilModuloTransacaoDTO : BaseDTO
    {
        public int IdPerfil { get; set; }
        public virtual PerfilDTO Perfil { get; set; }
        public int IdModuloTransacao { get; set; }
        public virtual ModuloTransacaoDTO ModuloTransacao { get; set; }
    }
}
