using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;

namespace test
{
    [TestClass]
    public class OrderTest
    {
        OrderController orderController = new OrderController();

        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }
        


        [TestCleanup]
        public void CleanUp()
        {
            TestHelper.DeleteAllInDB();
        }
    }
}
