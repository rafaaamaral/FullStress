using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioDTO : BaseDTO
    {
        public string Nome { get; set; }
        public string PsnID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string FotoPerfil { get; set; }
        public int IdPerfil { get; set; }
        public virtual PerfilDTO Perfil { get; set; }

        public List<PerfilModuloTransacaoDTO> PerfilModuloTransacao { get; set; }
    }
}
