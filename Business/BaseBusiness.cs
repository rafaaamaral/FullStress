using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BaseBusiness<DTO> where DTO : BaseDTO
    {
        #region Protected

        protected Type EntityType;
        protected Type RepositoryType;
        protected string[] Includes { get; set; }

        protected virtual object RepositoryInstance
        {
            get
            {
                //Cria o Type do BaseRepository<Usuário>, por exemplo
                //Type typeRepository = RepositoryType.MakeGenericType(this.EntityType);
                Type typeRepository = RepositoryType;

                //Cria a instância do objecto BaseRepository<T>
                var repository = Activator.CreateInstance(typeRepository);

                return repository;
            }
        }

        #endregion Protected

        public virtual Retorno<DTO> GetById(int id)
        {
            var Retorno = new Retorno<DTO>();

            try
            {
                //Lista de tipos dos parametros do método a ser executado
                Type[] tiposParams = new Type[] { typeof(int), typeof(string[]) };

                MethodInfo method = RepositoryInstance.GetType().GetMethod("GetById");

                BaseEntidade EntidadeBase = (BaseEntidade)method.Invoke(RepositoryInstance, new object[] { id, this.Includes });

                var childEntity = Convert.ChangeType(EntidadeBase, this.EntityType);

                Retorno.Data = Mapper.DynamicMap<DTO>(childEntity);

                if (Retorno.Data == null)
                    Retorno.Sucesso = false;

            }
            catch (Exception exception)
            {
                Retorno = TratarRetorno.LogError<DTO>(exception);
            }

            return Retorno;
        }

        public virtual Retorno<DTO> Save(DTO dto)
        {
            Retorno<DTO> Retorno = new Retorno<DTO>();

            try
            {
                //Lista de tipos dos parametros do método a ser executado
                Type[] tiposParams = new Type[] { this.EntityType };

                var objEntity = Activator.CreateInstance(this.EntityType);
                Mapper.DynamicMap(dto, objEntity, typeof(DTO), this.EntityType);

                MethodInfo method = RepositoryInstance.GetType().GetMethod("Save", tiposParams);
                objEntity = method.Invoke(RepositoryInstance, new object[] { objEntity });

                var childEntity = Convert.ChangeType(objEntity, this.EntityType);

                Retorno.Data = Mapper.DynamicMap<DTO>(childEntity);
                //Retorno.Data = id;
            }
            catch (Exception ex)
            {
                Retorno = TratarRetorno.LogError<DTO>(ex);

            }

            return Retorno;
        }

        public virtual Retorno<List<DTO>> GetAll()
        {
            var Retorno = new Retorno<List<DTO>>();

            try
            {
                //Lista de tipos dos parametros do método a ser executado
                //Type[] tiposParams = new Type[] { typeof(List<string>) };
                //Type[] tiposParams = new Type[] { typeof(int), typeof(string[]) };

                MethodInfo method = RepositoryInstance.GetType().GetMethod("GetAll");

                var list = method.Invoke(RepositoryInstance, new object[] { this.Includes });

                List<DTO> listDTO = new List<DTO>();

                foreach (var item in (IEnumerable)list)
                {
                    DTO objDTO = (DTO)Activator.CreateInstance(typeof(DTO));

                    Mapper.DynamicMap(item, objDTO, EntityType, typeof(DTO));
                    listDTO.Add(objDTO);

                }


                Retorno.Data = listDTO;

            }
            catch (Exception exception)
            {
                Retorno = TratarRetorno.LogError<List<DTO>>(exception);
            }

            return Retorno;
        }

        public virtual Retorno Remove(int id)
        {
            var Retorno = new Retorno();

            try
            {
                //Lista de tipos dos parametros do método a ser executado
                Type[] tiposParams = new Type[] { typeof(int) };

                MethodInfo method = RepositoryInstance.GetType().GetMethod("Remove", tiposParams);

                method.Invoke(RepositoryInstance, new object[] { id });
            }
            catch (Exception exception)
            {
                Retorno = TratarRetorno.LogError(exception);
            }

            return Retorno;
        }

        public virtual string GeraSenha(int caracteres)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(caracteres, 18);

            string senha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return senha;
        }
    }
}
