using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class CustomerTest
    {
        CustomerController controller = new CustomerController();

        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }


        [TestMethod]
        public void CustomerCreate()
        {
            //Setup
            int preCount = TestHelper.CountTable("Customers");
            preCount = preCount + 1;
            //act
            Customer customer = new Customer("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            controller.Create(customer, "TestPassword");

            int postCount = TestHelper.CountTable("Customers");
            //Assert
            Assert.AreEqual(preCount, postCount, "Customer was not Created Successfully");
        }
        [TestMethod]
        public void CustomerFindByID()
        {
            //setup
            Customer customer = new Customer("TestNameFindID", "TestPhonenumberFindID", "TestEmailFindID", "TestUsernameFindID", null);
            Customer c = controller.Create(customer, "TestPassword");
            //act
            Customer customerFound = controller.Find(c.Id);
            //Assert
            Assert.AreEqual(c, customerFound, "Customer wasn't found");
        }

        [TestMethod]
        public void CustomerFindByColumnAndSearch()
        {
            //setup
            Customer c = controller.Create(new Customer("TestNameFind", "TestPhonenumberFind", "TestEmailFind", "TestUsernameFind", null), "TestPassword");
            Customer c2 = controller.Create(new Customer("TestNameFind", "TestPhonenumberFind1", "TestEmailFind1", "TestUsernameFind1", null), "TestPassword1");
            List<Customer> expected = new List<Customer>() { c, c2 };
            //act
            List<Customer> actual = controller.Find(CustomerColumn.name, "TestNameFind");
            //Assert
            Assert.AreEqual(actual.All(expected.Contains), true, "Customers was incorrectly retrieved from the database");
        }

        [TestMethod]
        public void CustomerUpdate()
        {
            //Setup
            Customer expected = controller.Create(new Customer("TestNameUpdate", "TestPhonenumberUpdate", "TestEmailUpdate", "TestUsernameUpdate", null), "TestPassword");
            expected.Name = "TestNameUpdated";
            //Act
            controller.Update(expected);
            //Assert
            Customer actual = controller.Find(expected.Id);
            Assert.AreEqual(expected, actual, "Customer blec ikke opdateret korrekt");
        }


        [TestMethod]
        public void CustomerDelete()
        {
            //Setup
            Customer c = controller.Create(new Customer("TestNameDelete", "TestPhonenumberDelete", "TestEmailDelete", "TestUsernameDelete", null), "TestPassword");
            //Act
            bool isSuccess = controller.Delete(c.Id);

            //Assert 1
            Customer notFound = controller.Find(c.Id);
            Assert.IsNull(notFound);

            //Assert 2
            Assert.AreEqual(isSuccess, true, "Metoden returner forkert");

        }

        [TestMethod]
        public void CustomerLogin()
        {
            //Setup
            Customer c = controller.Create(new Customer("TestNameLogin", "TestPhonenumberLogin", "TestEmailLogin", "TestUsernameLogin", null), "TestPassword");
            //act
            Customer found = controller.Login("TestUsernameLogin", "TestPassword");
            //assert
            Assert.IsNotNull(found);
        }

        [TestCleanup]
        public void CleanUp() => TestHelper.DeleteAllInDB();

    }
}
