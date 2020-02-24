using ProjectManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class APICredentialsBLL
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CommonMethods
    {
        public bool ValidateRequest(APICredentialsBLL objAPICredentials)
        {
            bool IsValidate = false;
            try
            {
                if (objAPICredentials.UserName == CommonObjects.GetCongifValue(ConfigKeys.UserName) && objAPICredentials.Password == CommonObjects.GetCongifValue(ConfigKeys.Password))
                    IsValidate = true;
            }
            catch (Exception)
            {
                throw;
            }
            return IsValidate;
        }

    }
}
