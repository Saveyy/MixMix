using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    interface IDbCrud<T>
    {
        bool Create(T obj);
        T Find(int id);
        bool Update(T obj);
        bool Delete(int id);
    }
}
