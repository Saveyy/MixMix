using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixMixMVC.Exceptions
{
    [Serializable]
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException()
            :base(String.Format("Invalid login"))
        {

        }
    }
}