using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BLL
{
    public class ProjectManagementBLL : IDisposable
    {
        #region Variables

        DAL.DAL objDAL = new DAL.DAL();

        #endregion

        #region Methods

        public ProjectManagementBLL()
        {
            objDAL.Open();
        }


        public int ExecuteStatement(SqlCommand pObjCommand)
        {
            try
            {
                return objDAL.ExecuteStatement(pObjCommand);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetData(SqlCommand pObjCommand)
        {
            try
            {
                return objDAL.GetData(pObjCommand);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDataSet(SqlCommand pObjCommand)
        {
            try
            {
                return objDAL.GetDataSet(pObjCommand);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Rollback()
        {
            objDAL.Rollback();
        }

        void IDisposable.Dispose()
        {
            objDAL.Close();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

