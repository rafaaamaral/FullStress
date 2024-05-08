using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Perfil : BaseEntidade
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
