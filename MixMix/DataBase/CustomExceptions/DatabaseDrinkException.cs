using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.CustomExceptions
{
    public class DatabaseDrinkException : Exception
    {
        public DatabaseDrinkException()
        {
        }

        public DatabaseDrinkException(string message) : base(message)
        {
        }

        public DatabaseDrinkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseDrinkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
