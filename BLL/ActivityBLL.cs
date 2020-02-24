using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ActivityProperties {
        public int UserID { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }
        public bool IsInScope { get; set; }
        public int ActivityTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ActivityBLL
    {
        public DataTable GetAllActivityTypes(ProjectManagementBLL objProjectManagementBLL)
        {
            DataTable dtRecord = new DataTable();
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_GetAllActivityTypes"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    dtRecord = objProjectManagementBLL.GetData(sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured while getting All Activity Types : {0}", ex.Message), ex);
            }
            return dtRecord;
        }

        public bool UpdateNextDayActivity(ProjectManagementBLL objProjectManagementBLL, int UserID, string NextDayActivity)
        {
            bool isUpdate = false;
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_UpdateNextDayActivity"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@UserID", UserID);
                    sqlComm.Parameters.AddWithValue("@NextDayActivity", NextDayActivity);
                    objProjectManagementBLL.GetData(sqlComm);
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                isUpdate = false;
                throw new Exception(string.Format("Error occured while update next day activity : {0}", ex.Message), ex);
            }
            return isUpdate;
        }

        public bool InsertActivity(ProjectManagementBLL objProjectManagementBLL, ActivityProperties propActivities)
        {
            bool isInsert = false;
            try
            {
                using (SqlCommand sqlComm = new SqlCommand("proc_InsertActivity"))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@UserID", propActivities.UserID);
                    sqlComm.Parameters.AddWithValue("@ActivityName", propActivities.ActivityName);
                    sqlComm.Parameters.AddWithValue("@Description", propActivities.Description);
                    sqlComm.Parameters.AddWithValue("@ProjectID", propActivities.ProjectID);
                    sqlComm.Parameters.AddWithValue("@IsInScope", propActivities.IsInScope);
                    sqlComm.Parameters.AddWithValue("@ActivityTypeID", propActivities.ActivityTypeID);
                    sqlComm.Parameters.AddWithValue("@StartDate", propActivities.StartDate);
                    sqlComm.Parameters.AddWithValue("@EndDate", propActivities.EndDate);
                    objProjectManagementBLL.GetData(sqlComm);
                    isInsert = true;
                }
            }
            catch (Exception ex)
            {
                isInsert = false;
                throw new Exception(string.Format("Error occured while insert activity : {0}", ex.Message), ex);
            }
            return isInsert;
        }
    }
}
