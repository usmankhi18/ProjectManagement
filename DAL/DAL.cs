using ProjectManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DAL
    {
        SqlConnection sqlConnection = null;
        System.Data.SqlClient.SqlTransaction sqlTransaction = null;

        public void Open()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(CommonObjects.GetConnectionString());
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
            }
        }

        public void Close()
        {
            if (sqlConnection != null)
            {
                if (sqlTransaction != null)
                {
                    try
                    {
                        sqlTransaction.Commit();
                    }
                    catch (Exception)
                    {
                    }
                    sqlTransaction = null;
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }

        public void Rollback()
        {
            if (sqlConnection != null)
            {
                if (sqlTransaction != null)
                {
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception)
                    {
                    }
                    sqlTransaction = null;
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }

        public int ExecuteStatement(SqlCommand pObjCommand)
        {
            try
            {
                pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                return pObjCommand.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public DataTable GetData(SqlCommand pObjCommand)
        {
            try
            {
                pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                DataTable dataTable = new DataTable();
                SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
                objAdapter.Fill(dataTable);
                objAdapter.Dispose();
                return dataTable;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public DataSet GetDataSet(SqlCommand pObjCommand)
        {
            try
            {
                pObjCommand.Transaction = sqlTransaction;
                pObjCommand.Connection = sqlConnection;
                DataSet dataSet = new DataSet();
                SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
                objAdapter.Fill(dataSet);
                objAdapter.Dispose();
                return dataSet;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

