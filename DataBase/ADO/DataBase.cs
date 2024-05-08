using DataBase.ADO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataBase.ADO
{
    public class DataBase<T> : BaseDataBase
    {
        public delegate List<T> DelegateExecutarRead(SqlDataReader dataReader);

        public List<T> ExecuteProcReader(string nomeProcedure, List<SqlParameter> parametros, DelegateExecutarRead delegateMetodoReader)
        {
            try
            {
                SqlCommandSTP.CommandText = nomeProcedure;
                SetParameters(parametros);
                OpenConnection();
                var dr = SqlDataReaderSTP;
                return delegateMetodoReader(dr);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
            finally
            {
                CloseConnection();
            }

        }

        public DataTable ExecuteDataTable(string nomeProcedure, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommandSTP.CommandText = nomeProcedure;
                SetParameters(parametros);
                OpenConnection();

                DataTable dt = new DataTable();
                SqlAdapter.Fill(dt);


                return dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                CloseConnection();
            }

        }

        public List<T> ExecuteQueryReader(string query, List<SqlParameter> parametros,
                                            DelegateExecutarRead delegateMetodoReader)
        {
            try
            {
                SqlCommandQuerry.CommandText = query;
                SetParameters(parametros);
                OpenConnection();
                var dr = SqlDataReader;
                return delegateMetodoReader(dr);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(string procedureName, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommandSTP.CommandText = procedureName;
                SetParameters(parametros);

                OpenConnection();


                return SqlCommandSTP.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SetParameters(List<SqlParameter> parametros)
        {
            SqlCommandSTP.Parameters.Clear();
            if (parametros == null) return;
            foreach (var t in parametros)
            {
                SqlCommandSTP.Parameters.Add(t);
            }
        }

        public List<SqlParameter> ListaParametros { get; set; }

        public DataSet ExecuteDataSet(string _procname, params DbParameter[] parameters)
        {

            DatabaseFactory.ClearDatabaseProviderFactory();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            Database dbTrade = new DatabaseProviderFactory().Create("Context");
            DbCommand cmd = dbTrade.GetStoredProcCommand(_procname);

            for (int i = 0; i < parameters.Length; i++)
            {
                dbTrade.AddParameter(cmd, parameters[i].ParameterName, parameters[i].DbType, parameters[i].Direction, parameters[i].SourceColumn, parameters[i].SourceVersion, parameters[i].Value);
            }

            return dbTrade.ExecuteDataSet(cmd);
        }

    }
}
