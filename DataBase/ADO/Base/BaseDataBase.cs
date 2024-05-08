using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.ADO.Base
{
    public class BaseDataBase
    {
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlComand;
        private SqlDataAdapter _sqlDataAdapter;
        private SqlDataReader _sqlDataReader;

        private string ConectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Context"].ToString();
            }
        }

        protected SqlConnection SqlConnection
        {
            get
            {
                if (_sqlConnection == null)
                    _sqlConnection = new SqlConnection(this.ConectionString);

                return _sqlConnection;
            }
        }

        protected SqlDataAdapter SqlAdapter
        {
            get
            {
                if (_sqlDataAdapter == null)
                    _sqlDataAdapter = new SqlDataAdapter(this._sqlComand);

                return _sqlDataAdapter;
            }
        }

        protected SqlCommand SqlCommandSTP
        {
            get
            {
                if (_sqlComand == null)
                    _sqlComand = new SqlCommand() { Connection = this.SqlConnection, CommandType = System.Data.CommandType.StoredProcedure };

                _sqlComand.CommandTimeout = 0;

                return _sqlComand;
            }
        }

        protected SqlCommand SqlCommandQuerry
        {
            get
            {
                if (_sqlComand == null)
                    _sqlComand = new SqlCommand() { Connection = this.SqlConnection, CommandType = System.Data.CommandType.StoredProcedure };

                _sqlComand.CommandTimeout = 0;

                return _sqlComand;
            }
        }

        protected SqlDataReader SqlDataReaderSTP
        {
            get { return _sqlDataReader ?? (_sqlDataReader = SqlCommandSTP.ExecuteReader()); }
            set { _sqlDataReader = value; }

        }

        protected SqlDataReader SqlDataReader
        {
            get { return _sqlDataReader ?? (_sqlDataReader = SqlCommandQuerry.ExecuteReader()); }
            set { _sqlDataReader = value; }

        }


        protected SqlTransaction OpenTrasacao()
        {
            var transacao = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            SqlCommandQuerry.Transaction = transacao;
            return transacao;
        }

        protected void OpenConnection()
        {
            if (SqlConnection.State != ConnectionState.Open)
            {
                SqlConnection.Open();
            }
        }

        protected void CloseConnection()
        {
            if (SqlConnection.State != ConnectionState.Closed)
            {
                SqlConnection.Close();
            }
        }

    }
}
