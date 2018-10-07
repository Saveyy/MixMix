using System;
using Controller;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            CustomerController controller = new CustomerController();
            Customer customer = new Customer("TestName", "TestPhonenumber", "TestEmail", "m1", null);
            controller.Create(customer, "m1");
        }
    }
}
