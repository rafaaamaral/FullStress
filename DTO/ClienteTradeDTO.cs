using Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClienteTradeDTO : BaseDTO
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Plano { get; set; }
        public int IdUsuario { get; set; }
        public UsuarioDTO Usuario { get; set; }

        public string DescricaoPlano
        {
            get
            {
                switch (Plano)
                {
                    case (int)PlanoTrade.Mensal:
                        return "Mensal";
                    case (int)PlanoTrade.Temporada:
                        return "Temporada";
                    default:
                        return "";
                }
            }
            set { }

        }
    }
}
