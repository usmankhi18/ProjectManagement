using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Controllers;
using Services;
using Services.DTO;
using System.Configuration;
using System.Security.Principal;
using ProjectManagement.Models;
using System.Web.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ProjectManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var login = new Login();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();


                return View(login);
            }
            catch
            {
                throw;
            }
        }
        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }
        //GET: SignInAsync
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login entity)
        {
            UserController user = new UserController();
            LoginRequestDTO login = new LoginRequestDTO();
            ResponseDTO Response = new ResponseDTO();
            try
            {
                if (!ModelState.IsValid)
                    return View(entity);

                bool isLogin = false;
                login = setlogin(entity.Email, entity.Password);

                Response = GetLoginFromService(login);

                //Response = user.GetLogin(login);
                if (Response.ResponseCode == "00")
                {
                    if (Response.ResponseData.LoginResp.UserID != 0)
                    {
                        isLogin = true;
                    }
                    else
                    {
                        isLogin = false;
                        TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                        return View(entity);
                    }

                }
                else
                {
                    isLogin = false;
                    TempData["ErrorMSG"] = "Access Denied! Access to Service Failed";
                    return View(entity);
                }
                if (isLogin)
                {
                    //Login Success
                    //For Set Authentication in Cookie (Remeber ME Option)
                    SignInRemember(entity.Email, entity.isRemember);

                    //Set A Unique ID in session
                    Session["UserID"] = Response.ResponseData.LoginResp.UserID;
                    Session["LoginCredentials"] = Response.ResponseData.LoginResp;

                    return RedirectToAction("Index","Home" );
                }
                else
                {
                    //Login Fail
                    if (TempData["ErrorMSG"].ToString()=="")
                    {
                        TempData["ErrorMSG"] = "Error";
                        return View(entity);
                    }
                    else
                    return View(entity);
                }
            }
            catch
            {
                throw;
            }

        }
        // Haris
        [HttpPost]
        private ResponseDTO GetLoginFromService(LoginRequestDTO login)
        {
            using (var client = new HttpClient())
            {
                ResponseDTO res = new ResponseDTO();
                //client.BaseAddress = new Uri("http://localhost/PMServices/");
                var response =  client.PostAsJsonAsync("http://localhost/PMServices/api/User/GetLogin", login).Result;
                var result =  response.Content.ReadAsStringAsync().Result;
                res= JsonConvert.DeserializeObject<ResponseDTO>(result);
                return res;
            }
        }

        private LoginRequestDTO setlogin(string username, string password)
        {
            LoginRequestDTO login = new LoginRequestDTO();
            login.UserName = username;
            login.Password = password;
            APICredentials api = new APICredentials()
            {
                APIPassword = ConfigurationManager.AppSettings["ApiPass"],
                APIUserName = ConfigurationManager.AppSettings["ApiUserName"]
            };
            login.APICredentials = api;
            return login;
        }
    }
}