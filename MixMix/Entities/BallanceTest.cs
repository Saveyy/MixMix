using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BallanceTest
    {
        /* This is a comment for the sake of commit changes - Can be removed */
        public double Ballancee { get; private set; }

        public void IncreaseBallance(double amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Der er skdsau ballade");
            }
            else
            {
                Ballancee += amount;
            }
        }
    }
}
