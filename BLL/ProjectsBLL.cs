using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BLL
{
    public class ProjectsBLL
    {
        public DataTable GetAllProjects(ProjectManagementBLL objProjectManagementBLL)
        {
            DataTable dtRecord = new DataTable();
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_GetAllProjects"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    dtRecord = objProjectManagementBLL.GetData(sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while getting All Projects : {0}", ex.Message), ex);
            }
            return dtRecord;
        }

    }
}
