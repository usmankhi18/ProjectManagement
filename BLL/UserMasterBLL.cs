using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BLL
{
    public class UserMasterBLL
    {
        public DataTable GetLogin(ProjectManagementBLL objProjectManagementBLL, APICredentialsBLL credentials)
        {
            DataTable dtRecord = new DataTable();
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_GetLogin"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Email", credentials.UserName);
                    sqlComm.Parameters.AddWithValue("@Password", credentials.Password);
                    dtRecord = objProjectManagementBLL.GetData(sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while Login : {0}", ex.Message), ex);
            }
            return dtRecord;
        }

        public DataTable AuthenticateEmail(ProjectManagementBLL objProjectManagementBLL, string Email)
        {
            DataTable dtRecord = new DataTable();
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_AuthenticateEmail"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Email", Email);
                    dtRecord = objProjectManagementBLL.GetData(sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while Authenticate Email : {0}", ex.Message), ex);
            }
            return dtRecord;
        }
    }
}
