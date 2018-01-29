using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
//using RestSharp;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using RestSharp;
using siteSmartOrder.Models;
//using SiteCobranza.Models;


    public class MyMembershipProvider : MembershipProvider
    {

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(ConfigurationManager.AppSettings["PortalServer"]);
                var request = new RestRequest("UserPortal/Login", Method.POST);
                request.RequestFormat = DataFormat.Json;
              
                request.AddBody(new { 
                    nickname = username,
                    password = password
                });
                
                var response = client.Execute(request);
                string content = response.Content;
                var usuarioPortal = JsonConvert.DeserializeObject<Response<UserPortal>>(content);
                if (usuarioPortal.IsSuccess)
                {
                    HttpContext.Current.Session.Remove("UserPortal");
                    HttpContext.Current.Session.Add("UserPortal", usuarioPortal.Data);
                    //HttpContext.Current.Session.Timeout = 2;
                    return true;
                }
                int errorCode = usuarioPortal.ErrorCode;
                if(errorCode==501)
                    HttpContext.Current.Items["ValidateUserResult"] = "Usuario o password incorrectos";

                HttpContext.Current.Items["ValidateUserResult"] = usuarioPortal.Message;

                return false;

            }
            catch (Exception e)
            {
                HttpContext.Current.Items["ValidateUserResult"] = "Error no especificado: " + e.Message;
                return false;
            }
        }
    }
