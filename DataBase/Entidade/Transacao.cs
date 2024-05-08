using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Transacao : BaseEntidade
    {
        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }

        public string URL { get; set; }

        public int? ParametroURL { get; set; }
    }
}
