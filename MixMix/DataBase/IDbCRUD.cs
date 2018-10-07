using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public interface IDbCRUD<T>
    {
        T Insert(T obj, string password);
        T Find(int id);
        bool Update(T obj);
        bool Delete(int id);
    }
}
