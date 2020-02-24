using BLL;
using ProjectManagement.Utilities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Services.Controllers
{
    public class ActivityController : ApiController
    {
        [HttpPost]
        [Route("api/Activity/LoadActivityTypes")]
        public ResponseDTO LoadActivityTypes(APICredentials request)
        {
            ResponseDTO resp = new ResponseDTO();
            using (ProjectManagementBLL objProjectManagementBLL = new ProjectManagementBLL())
            {
                try
                {
                    APICredentialsBLL credentialsBLL = new APICredentialsBLL();
                    credentialsBLL.UserName = request.APIUserName;
                    credentialsBLL.Password = request.APIPassword;
                    if (!new CommonMethods().ValidateRequest(credentialsBLL))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidCredentials;
                    }
                    else
                    {
                        DataTable dtRecord = new ActivityBLL().GetAllActivityTypes(objProjectManagementBLL);
                        List<ActivityTypes> acttypes = new List<ActivityTypes>();
                        if (dtRecord.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtRecord.Rows)
                            {
                                ActivityTypes acttyp = new ActivityTypes();
                                acttyp.ActTypeID = int.Parse(row["ActTypeID"].ToString());
                                acttyp.ActivityType = row["ActivityType"].ToString();
                                acttypes.Add(acttyp);
                            }
                        }
                        resp.ResponseCode = ResponseCodes.Success;
                        resp.ResponseMessage = ResponseMessages.Success;
                        resp.ResponseData = new ResponseData();
                        resp.ResponseData.activitytypes = acttypes;
                    }
                }
                catch (Exception ex)
                {
                    resp.ResponseCode = ResponseCodes.Failed;
                    resp.ResponseMessage = ex.Message;
                }
            }
            return resp;
        }

        [HttpPost]
        [Route("api/Activity/InsertActivity")]
        public ResponseDTO InsertActivity(RequestInsertActivity request)
        {
            ResponseDTO resp = new ResponseDTO();
            using (ProjectManagementBLL objProjectManagementBLL = new ProjectManagementBLL())
            {
                try
                {
                    APICredentialsBLL credentialsBLL = new APICredentialsBLL();
                    credentialsBLL.UserName = request.APICredentials.APIUserName;
                    credentialsBLL.Password = request.APICredentials.APIPassword;
                    if (!new CommonMethods().ValidateRequest(credentialsBLL))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidCredentials;
                    }
                    else if (request.UserID == 0 || string.IsNullOrEmpty(request.ActivityName) || request.ProjectID == 0 || request.ActivityTypeID == 0)
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidData;
                    }
                    else
                    {
                        ActivityProperties propActivities = new ActivityProperties();
                        propActivities.UserID = request.UserID;
                        propActivities.ActivityName = request.ActivityName;
                        propActivities.Description = request.Description;
                        propActivities.ProjectID = request.ProjectID;
                        propActivities.IsInScope = request.IsInScope;
                        propActivities.ActivityTypeID = request.ActivityTypeID;
                        propActivities.StartDate = request.StartDate;
                        propActivities.EndDate = request.EndDate;
                        if (new ActivityBLL().InsertActivity(objProjectManagementBLL, propActivities))
                        {
                            resp.ResponseCode = ResponseCodes.Success;
                            resp.ResponseMessage = ResponseMessages.Success;
                        }
                        else
                        {
                            resp.ResponseCode = ResponseCodes.Failed;
                            resp.ResponseMessage = ResponseMessages.Failed;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resp.ResponseCode = ResponseCodes.Failed;
                    resp.ResponseMessage = ex.Message;
                }
            }
            return resp;
        }

        [HttpPost]
        [Route("api/Activity/NextDayActiviity")]
        public ResponseDTO NextDayActiviity(RequestNextDayActivity request)
        {
            ResponseDTO resp = new ResponseDTO();
            using (ProjectManagementBLL objProjectManagementBLL = new ProjectManagementBLL())
            {
                try
                {
                    APICredentialsBLL credentialsBLL = new APICredentialsBLL();
                    credentialsBLL.UserName = request.APICredentials.APIUserName;
                    credentialsBLL.Password = request.APICredentials.APIPassword;
                    if (!new CommonMethods().ValidateRequest(credentialsBLL))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidCredentials;
                    } else if (request.UserID == 0 || string.IsNullOrEmpty(request.NextDayActivity)) {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidData;
                    }
                    else
                    {
                        if (new ActivityBLL().UpdateNextDayActivity(objProjectManagementBLL, request.UserID, request.NextDayActivity))
                        {
                            resp.ResponseCode = ResponseCodes.Success;
                            resp.ResponseMessage = ResponseMessages.Success;
                        }
                        else {
                            resp.ResponseCode = ResponseCodes.Failed;
                            resp.ResponseMessage = ResponseMessages.Failed;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resp.ResponseCode = ResponseCodes.Failed;
                    resp.ResponseMessage = ex.Message;
                }
            }
            return resp;
        }
    }
}
