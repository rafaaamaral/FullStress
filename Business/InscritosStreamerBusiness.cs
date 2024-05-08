using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class InscritosStreamerBusiness  :BaseBusiness<InscritosStreamerDTO>
    {
        private readonly InscritosStreamerRepositorio _inscritosStreamerRepositorio;

        public InscritosStreamerBusiness()
        {
            base.EntityType = typeof(InscritosStreamer);
            base.RepositoryType = typeof(InscritosStreamerRepositorio);
            base.Includes = new[] { "Streamer" };
            _inscritosStreamerRepositorio = new InscritosStreamerRepositorio();
        }

        public Retorno<List<InscritosStreamerDTO>> ObterInscritosPorUsuario(int idUsuario)
        {
            var retorno = new Retorno<List<InscritosStreamerDTO>>();

            try
            {
                var isncritos = _inscritosStreamerRepositorio.ObterIncritosPorUsuario(idUsuario);
                retorno.Data = Mapper.DynamicMap<List<InscritosStreamerDTO>>(isncritos);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário não encontrado!";
            }

            return retorno;
        }

        public Retorno ProcessarInscritos(string arquivo, int idUsuario)
        {
            var retorno = new Retorno();

            try
            {
                _inscritosStreamerRepositorio.InativarTodosIncritos();

                using(var reader = new StreamReader(arquivo))
                {
                    List<string> listA = new List<string>();
                    List<string> listB = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values[0].ToUpper() == "USERNAME")
                            continue;

                        var inscrito = _inscritosStreamerRepositorio.ObterInscritoPorNome(values[0]);

                        if(inscrito == null)
                        {
                            var inscritoDTO = ObterDTO(values[0], values[1], values[2], values[3], values[4], values[5], idUsuario);
                            var entidade = _inscritosStreamerRepositorio.Save(Mapper.DynamicMap<InscritosStreamer>(inscritoDTO));
                        }
                        else
                        {
                            _inscritosStreamerRepositorio.AlterarInscrito(values[0], values[1]);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Usuário não encontrado!";
            }

            return retorno;
        }

        public InscritosStreamerDTO ObterDTO(string nome, string dataInscricao, string currentTier, string tempoInscricao, string sequencial, string tipoInscricao, int idUsuario)
        {
            return new InscritosStreamerDTO()
            {
                Ativo = true,
                CurrentTier = currentTier,
                DataCadastro = DateTime.Now,
                IdStreamer = idUsuario,
                DataInscricao = Convert.ToDateTime(dataInscricao),
                NomeUsuario = nome,
                Sequencial = Convert.ToInt32(sequencial),
                TempoInscricao = Convert.ToInt32(tempoInscricao),
                TipoInscricao = tipoInscricao
            };
        }

    }
}
