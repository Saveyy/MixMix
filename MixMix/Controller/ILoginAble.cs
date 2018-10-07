using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public interface ILoginAble<T>
    {
        T Login(string username, string password);
    }
}
