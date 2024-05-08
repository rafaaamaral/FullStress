using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ClienteTradeFaturamentoBusiness : BaseBusiness<ClienteTradeFaturamentoDTO>
    {
        private readonly ClienteTradeFaturamentoRepositorio _clienteTradeFaturamentoRepositorio;

        public ClienteTradeFaturamentoBusiness()
        {
            base.EntityType = typeof(ClienteTradeFaturamento);
            base.RepositoryType = typeof(ClienteTradeFaturamento);
            base.Includes = new[] { "ClienteFaturamento" };
            _clienteTradeFaturamentoRepositorio = new ClienteTradeFaturamentoRepositorio();
        }

        public Retorno<List<ClienteTradeFaturamentoDTO>> ObterFaturamentoPorCliente(int idCliente, int idUsuario)
        {
            var retorno = new Retorno<List<ClienteTradeFaturamentoDTO>>();

            try
            {
                var clube = _clienteTradeFaturamentoRepositorio.ObterFaturamentoPorCliente(idCliente);
                retorno.Data = Mapper.DynamicMap<List<ClienteTradeFaturamentoDTO>>(clube);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Faturamento não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirClienteTradeFaturamento(ClienteTradeFaturamentoDTO clienteFaturamento, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var clienteFaturamentoDTO = ObterDTO(clienteFaturamento.IdClienteTrade, clienteFaturamento.DtInicio, clienteFaturamento.DtTermino, clienteFaturamento.VlValor);

                var entidade = _clienteTradeFaturamentoRepositorio.Save(Mapper.DynamicMap<ClienteTradeFaturamento>(clienteFaturamentoDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Jogador incluido no clube com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao faturar cliente";
            }

            return retorno;
        }

        private ClienteTradeFaturamentoDTO ObterDTO(int idClienteTrade, string inicio, string termino, string valor)
        {
            return new ClienteTradeFaturamentoDTO()
            {
                IdClienteTrade = idClienteTrade,
                Inicio = Convert.ToDateTime(inicio, new CultureInfo("pt-BR")),
                Termino = Convert.ToDateTime(termino, new CultureInfo("pt-BR")),
                Valor = Convert.ToDecimal(valor, new CultureInfo("pt-BR")),
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
