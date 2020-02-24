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
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/GetLogin")]
        public ResponseDTO GetLogin(LoginRequestDTO request) {
            ResponseDTO resp = new ResponseDTO();
            using (ProjectManagementBLL objProjectManagementBLL = new ProjectManagementBLL())
            {
                try {
                    APICredentialsBLL credentialsBLL = new APICredentialsBLL();
                    credentialsBLL.UserName = request.APICredentials.APIUserName;
                    credentialsBLL.Password = request.APICredentials.APIPassword;
                    if (!new CommonMethods().ValidateRequest(credentialsBLL))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidCredentials;
                    }
                    else if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidData;
                    }
                    else {
                        APICredentialsBLL credentials = new APICredentialsBLL();
                        credentials.UserName = request.Email;
                        credentials.Password = request.Password;
                        DataTable dtRecord = new UserMasterBLL().GetLogin(objProjectManagementBLL, credentials);
                        LoginResponse login = new LoginResponse();
                        if (dtRecord.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtRecord.Rows)
                            {
                                login.UserID = int.Parse(row["UserID"].ToString());
                                login.RoleID = int.Parse(row["RoleID"].ToString());
                                login.FullName = row["FullName"].ToString();
                                login.RoleName = row["RoleName"].ToString();
                            }
                        }
                        resp.ResponseCode = ResponseCodes.Success;
                        resp.ResponseMessage = ResponseMessages.Success;
                        resp.ResponseData = new ResponseData();
                        resp.ResponseData.LoginResp = login;
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
        [Route("api/User/UserExist")]
        public ResponseDTO UserExist(EmailExistRequestDTO request)
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
                    else if (string.IsNullOrWhiteSpace(request.Email))
                    {
                        resp.ResponseCode = ResponseCodes.Failed;
                        resp.ResponseMessage = ResponseMessages.InvalidData;
                    }
                    else
                    {
                        DataTable dtRecord = new UserMasterBLL().AuthenticateEmail(objProjectManagementBLL, request.Email);
                        if (dtRecord.Rows.Count > 0)
                        {
                            resp.ResponseCode = ResponseCodes.Success;
                            resp.ResponseMessage = ResponseMessages.Success;
                        }
                        else {
                            resp.ResponseCode = ResponseCodes.Success;
                            resp.ResponseMessage = ResponseMessages.NoData;
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
