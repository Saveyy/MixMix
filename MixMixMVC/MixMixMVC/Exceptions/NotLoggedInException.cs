using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixMixMVC.Exceptions
{
    [Serializable]
    public class NotLoggedInException : Exception
    {
        public NotLoggedInException()
            :base(String.Format("Not Logged in Exception"))
        {

        }
    }
}