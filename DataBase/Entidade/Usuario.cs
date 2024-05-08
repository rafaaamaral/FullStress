using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Usuario : BaseEntidade
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        public string FotoPerfil { get; set; }

        public int IdPerfil { get; set; }

        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }
    }
}
