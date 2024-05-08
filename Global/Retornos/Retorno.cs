using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Retornos
{
    public class Retorno<T>
    {
        public bool Sucesso { get; set; }

        public List<string> _errosValidacao { get; set; } = new List<string>();

        public List<string> ErrosValidacao
        {
            get
            {
                return _errosValidacao;
            }
            set
            {
                if (value.Any())
                {
                    Sucesso = false;
                    _errosValidacao.AddRange(value);
                }
            }
        }

        public T Data { get; set; }
        public string Mensagem { get; set; } = string.Empty;

        public Retorno()
        {
            this.Sucesso = true;
            this.ErrosValidacao = new List<string>();
        }

        public Retorno(T data) : this()
        {
            this.Data = data;
        }
    }

    public class Retorno
    {
        public Retorno()
        {
            this.Sucesso = true;
            this.ErrosValidacao = new List<string>();
        }

        public bool Sucesso { get; set; }
        public object Data { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public List<string> ErrosValidacao { get; set; } = new List<string>();
    }
}
