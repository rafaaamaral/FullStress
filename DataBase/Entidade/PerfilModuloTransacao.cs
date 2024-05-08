using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class PerfilModuloTransacao : BaseEntidade 
    {
        public int IdPerfil { get; set; }

        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }

        public int IdModuloTransacao { get; set; }

        [ForeignKey("IdModuloTransacao")]
        public virtual ModuloTransacao ModuloTransacao { get; set; }
    }
}
