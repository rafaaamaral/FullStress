using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class VersaoRepositorio : BaseRepositorio<Versao>
    {
        public Versao ObterVersaoAtiva()
        {
            using (var contexto = new Context())
            {
                return contexto.Versao.Where(w => w.AtualProducao).FirstOrDefault();
            }
        }

        public void AlterarVersao(int idVersao, string nome, bool atualProducao)
        {
            using (var contexto = new Context())
            {
                var versao = contexto.Versao.FirstOrDefault(f => f.Id == idVersao);

                versao.Nome = nome;
                versao.AtualProducao = atualProducao;

                contexto.SaveChanges();
            }
        }
    }
}
