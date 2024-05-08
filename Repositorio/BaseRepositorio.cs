using DataBase;
using DataBase.ADO;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class BaseRepositorio<T> : DataBase<T> where T : BaseEntidade
    {
        public BaseRepositorio() : this(null) { }

        public BaseRepositorio(OnBeforeSaveChances beforeSaveChances)
        {
            this.BeforeSaveChances = beforeSaveChances;
        }

        public delegate void OnBeforeSaveChances(Context context, T t);

        public event OnBeforeSaveChances BeforeSaveChances;

        //public virtual T GetById(int id)
        //{
        //    return GetById(id, new string[] { });
        //}

        public virtual T GetById(int id, params string[] includes)
        {
            using (var context = new Context())
            {
                IQueryable<T> query = context.Set<T>();
                query = InsertIncludes(includes.ToList(), query);
                return query.SingleOrDefault(x => x.Id == id);
            }
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where, params string[] includes)
        {
            using (var context = new Context())
            {
                IQueryable<T> query = context.Set<T>();
                query = InsertIncludes(includes.ToList(), query);
                return query.SingleOrDefault(where);
            }
        }

        public virtual List<T> GetAll(params string[] includes)
        {
            return Get(null, includes);
        }

        public virtual List<T> Get(Expression<Func<T, bool>> where)
        {
            return Get(where, new string[] { });
        }

        public virtual List<T> Get(Expression<Func<T, bool>> where, params string[] includes)
        {
            int count;
            return Get(where, null, false, null, null, out count, includes);
        }

        public virtual List<T> Get(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, bool orderByDescending, int? pageIndex, int? take, out int count, params string[] includes)
        {
            using (var context = new Context())
            {
                count = 0;

                IQueryable<T> query = context.Set<T>();

                if (includes != null && includes.Length > 0)
                    query = InsertIncludes(includes.ToList(), query);

                if (where != null)
                    query = query.Where(where);

                if (orderBy != null)
                    query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

                if (take.HasValue && pageIndex.HasValue)
                {
                    count = query.Count();
                    query = query.Skip(pageIndex.Value * take.Value).Take(take.Value);
                }

                return query.ToList();
            }
        }

        private static IQueryable<T> InsertIncludes(List<string> includes, IQueryable<T> query)
        {
            if (includes != null)
            {
                foreach (var item in includes)
                    query = query.Include(item);
            }
            return query;
        }

        public virtual T Save(T t)
        {
            return Save(t, new List<string>());
        }

        public virtual T Save(T t, List<string> notModifiedFields)
        {
            return Save(t, notModifiedFields, true);
        }

        public virtual T Save(T t, List<string> notModifiedFields, bool commit)
        {
            using (var context = new Context())
            {
                if (t.Id == 0)
                {
                    t.DataCadastro = DateTime.Now;
                    context.Set<T>().Add(t);
                }
                else
                {
                    context.Entry<T>(t).State = EntityState.Modified;
                    context.Entry(t).Property(x => x.DataCadastro).IsModified = false;

                    if (notModifiedFields != null)
                    {
                        foreach (var item in notModifiedFields)
                        {
                            if (!string.IsNullOrEmpty(item))
                                context.Entry<T>(t).Property(item).IsModified = false;
                        }
                    }
                }

                if (BeforeSaveChances != null)
                    BeforeSaveChances(context, t);

                if (commit)
                {
                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();
                }

                return t;
            }
        }

        #region Salvar Com Transação

        public virtual T Save(Context context, T t)
        {
            return Save(context, t, new List<string>());
        }

        public virtual T Save(Context context, T t, List<string> notModifiedFields)
        {
            return Save(context, t, notModifiedFields, true);
        }

        public virtual T Save(Context context, T t, List<string> notModifiedFields, bool commit)
        {
            if (t.Id == 0)
            {
                t.DataCadastro = DateTime.Now;
                context.Set<T>().Add(t);
            }
            else
            {
                context.Entry<T>(t).State = EntityState.Modified;
                context.Entry(t).Property(x => x.DataCadastro).IsModified = false;

                if (notModifiedFields != null)
                {
                    foreach (var item in notModifiedFields)
                    {
                        if (!string.IsNullOrEmpty(item))
                            context.Entry<T>(t).Property(item).IsModified = false;
                    }
                }
            }

            if (BeforeSaveChances != null)
                BeforeSaveChances(context, t);

            if (commit)
            {
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
            }

            return t;
        }

        #endregion

        public virtual void Remove(int id)
        {
            using (var context = new Context())
            {
                IQueryable<T> query = context.Set<T>();
                var entity = query.Where(w => w.Id == id).FirstOrDefault();
                if (entity != null)
                    Remove(entity);
            }
        }

        public virtual void Remove(T t)
        {
            using (var context = new Context())
            {
                context.Entry<T>(t).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
