using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.ServiceInterfaces
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        bool Login(string username, string password);
    }
}
