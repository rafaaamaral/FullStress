using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class UsuarioModuloTransacao : BaseEntidade
    {
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public int IdModuloTransacao { get; set; }

        [ForeignKey("IdModuloTransacao")]
        public virtual ModuloTransacao ModuloTransacao { get; set; }
    }
}
