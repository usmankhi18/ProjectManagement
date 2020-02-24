using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.DTO
{
    public class LoginRequestDTO
    {
        public APICredentials APICredentials { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailExistRequestDTO
    {
        public APICredentials APICredentials { get; set; }
        public string Email { get; set; }
    }

    public class RequestNextDayActivity {
        public APICredentials APICredentials { get; set; }
        public int UserID { get; set; }
        public string NextDayActivity { get; set; }
    }

    public class RequestInsertActivity {
        public APICredentials APICredentials { get; set; }
        public int UserID { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }
        public bool IsInScope { get; set; }
        public int ActivityTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class APICredentials { 
        public string APIUserName { get; set; }
        public string APIPassword { get; set; }
    } 
}