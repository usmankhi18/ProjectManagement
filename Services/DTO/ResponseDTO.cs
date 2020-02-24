using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.DTO
{
    public class ResponseDTO
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public ResponseData ResponseData { get; set; }
    }

    public class ResponseData { 
        public LoginResponse LoginResp { get; set; }
        public List<Projects> projects { get; set; }
        public List<ActivityTypes> activitytypes { get; set; }
    }

    public class LoginResponse { 
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
    }

    public class Projects { 
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }

    public class ActivityTypes
    {
        public int ActTypeID { get; set; }
        public string ActivityType { get; set; }
    }
}