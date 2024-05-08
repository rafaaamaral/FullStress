using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ClienteTradeBusiness : BaseBusiness<ClienteTradeDTO>
    {
        private readonly ClienteTradeRepositorio _clienteTradeRepositorio;

        public ClienteTradeBusiness()
        {
            base.EntityType = typeof(ClienteTrade);
            base.RepositoryType = typeof(ClienteTradeRepositorio);
            base.Includes = new[] { "Usuario" };
            _clienteTradeRepositorio = new ClienteTradeRepositorio();
        }

        public Retorno<List<ClienteTradeDTO>> ObterClienteTradePorUsuario(int idUsuario)
        {
            var retorno = new Retorno<List<ClienteTradeDTO>>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var clube = _clienteTradeRepositorio.ObterClienteTradePorUsuario(idUsuario);
                retorno.Data = Mapper.DynamicMap<List<ClienteTradeDTO>>(clube);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Cliente não encontrado!";
            }

            return retorno;
        }

        public Retorno<int> IncluirClienteTrade(ClienteTradeDTO cliente, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var clienteTradeDTO = ObterDTO(cliente.Nome, cliente.Telefone, cliente.Plano, idUsuario);

                var entidade = _clienteTradeRepositorio.Save(Mapper.DynamicMap<ClienteTrade>(clienteTradeDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "Cliente Trade incluido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir Cliente Trade";
            }

            return retorno;
        }

        public Retorno<int> AlterarClienteTrade(ClienteTradeDTO cliente, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _clienteTradeRepositorio.AlterarClienteTrade(cliente.Id, cliente.Nome, cliente.Telefone, cliente.Plano);

                retorno.Data = 1;
                retorno.Mensagem = "Cliente Trade alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar cliente";
            }

            return retorno;
        }

        private ClienteTradeDTO ObterDTO(string nome, string telefone, int plano, int idUsuario)
        {
            return new ClienteTradeDTO()
            {
                Nome = nome,
                Telefone = telefone,
                IdUsuario = idUsuario,
                Plano = plano,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
