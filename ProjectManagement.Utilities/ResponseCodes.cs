using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Utilities
{
    public class ResponseCodes
    {
        public static string Success = "00";
        public static string Failed = "01";
    }

    public class ResponseMessages
    {
        public static string Success = "Success";
        public static string InvalidCredentials = "Invalid Credentials";
        public static string InvalidData = "Invalid Data";
        public static string Failed = "Failed";
        public static string NoData = "No Data Found";
    }
}
