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
    public class ProjectsController : ApiController
    {
        [HttpPost]
        [Route("api/Projects/LoadProjects")]
        public ResponseDTO LoadProjects(APICredentials request)
        {
            ResponseDTO resp = new ResponseDTO();
            using (ProjectManagementBLL objProjectManagementBLL = new ProjectManagementBLL())
            {
                try {
                    APICredentialsBLL credentialsBLL = new APICredentialsBLL();
                    credentialsBLL.UserName = request.APIUserName;
                    credentialsBLL.Password = request.APIPassword;
                    if (!new CommonMethods().ValidateRequest(credentialsBLL))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidCredentials;
                    }
                    else {
                        DataTable dtRecord = new ProjectsBLL().GetAllProjects(objProjectManagementBLL);
                        List<Projects> projects = new List<Projects>();
                        if (dtRecord.Rows.Count > 0) {
                            foreach (DataRow row in dtRecord.Rows) {
                                Projects proj = new Projects();
                                proj.ProjectID = int.Parse(row["ProjectID"].ToString());
                                proj.ProjectName = row["ProjectName"].ToString();
                                projects.Add(proj);
                            }
                        }
                        resp.ResponseCode = ResponseCodes.Success;
                        resp.ResponseMessage = ResponseMessages.Success;
                        resp.ResponseData = new ResponseData();
                        resp.ResponseData.projects = projects;
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
