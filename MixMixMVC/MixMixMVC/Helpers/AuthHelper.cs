using MixMixMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixMixMVC.Helpers
{
    public static class AuthHelper
    {
        //com
        private static readonly string LoginSessionName = "LoggedInUser";
        private static LoginViewModel currentUser;

        public static LoginViewModel CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[LoginSessionName] as LoginViewModel;
            }
            private set
            {
                currentUser = value;
            }
        }


        public static bool IsLoggedIn()
        {
            return HttpContext.Current.Session[LoginSessionName] != null;
        }
        public static void Login(LoginViewModel lvm)
        {
            HttpContext.Current.Session[LoginSessionName] = lvm;
        }
        public static void Logout()
        {
            HttpContext.Current.Session[LoginSessionName] = null;
        }
    }
}