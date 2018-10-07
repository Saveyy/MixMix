using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Entities.Manager;

namespace test
{
    [TestClass]
    public class ManagerTest
    {
        private ManagerController controller = new ManagerController();

        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }

        [TestMethod]
        public void ManagerCreate()
        {
            //Setup
            int preCount = TestHelper.CountTable("Managers");

            //act
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            controller.CreateManager(manager, "TestPassword");

            int postCount = TestHelper.CountTable("Managers");
            //Assert
            preCount = preCount + 1;
            Assert.AreEqual(preCount, postCount, "Manager was not Created Successfully");
        }

        [TestMethod]
        public void ManagerFindByID()
        {
            //setup
            Manager manager = new Manager("TestNameFindID", "TestPhonenumberFindID", "TestEmailFindID", "TestUsernameFindID", null);
            Manager m = controller.CreateManager(manager, "TestPassword");
            //act
            Manager customerFound = controller.FindManager(m.Id);
            //Assert
            Assert.AreEqual(m, customerFound, "Manager wasn't found");
        }


        // TEST INSERT
        [TestMethod]
        public void ManagerFindByColumnAndSearch()
        {
            //setup
            Manager c = controller.CreateManager(new Manager("TestNameFind", "TestPhonenumberFind", "TestEmailFind", "TestUsernameFind", null), "TestPassword");
            Manager c2 = controller.CreateManager(new Manager("TestNameFind", "TestPhonenumberFind1", "TestEmailFind1", "TestUsernameFind1", null), "TestPassword1");
            List<Manager> expected = new List<Manager>() { c, c2 };
            //act
            List<Manager> actual = controller.FindManager(ManagerColumn.name, "TestNameFind");
            //Assert
            Assert.AreEqual(actual.All(expected.Contains), true, "Managers was incorrectly retrieved from the database");
        }

        [TestMethod]
        public void ManagerUpdate()
        {
            //Setup
            Manager expected = controller.CreateManager(new Manager("TestNameUpdate", "TestPhonenumberUpdate", "TestEmailUpdate", "TestUsernameUpdate", null), "TestPassword");
            expected.Name = "TestNameUpdated";
            //Act
            controller.UpdateManager(expected);
            //Assert
            Manager actual = controller.FindManager(expected.Id);
            Assert.AreEqual(expected, actual, "Manager was incorrectly updated");
        }
        [TestMethod]
        public void ManagerDelete()
        {
            //Setup
            Manager c = controller.CreateManager(new Manager("TestNameDelete", "TestPhonenumberDelete", "TestEmailDelete", "TestUsernameDelete", null), "TestPassword");
            //Act
            bool isSuccess = controller.DeleteManager(c.Id);

            //Assert 1
            Manager notFound = controller.FindManager(c.Id);
            Assert.IsNull(notFound);

            //Assert 2
            Assert.AreEqual(isSuccess, true, "Metoden returner forkert");

        }


        [TestMethod]
        public void ManagerLogin()
        {
            string password = "TestPassword";
            string username = "TestUsernameLogin";
            //Setup
            Manager m = controller.CreateManager(new Manager("TestNameLogin", "TestPhonenumberLogin", "TestEmailLogin", username, null), password);
            //act
            Manager found = controller.Login(username, password);
            //assert
            Assert.IsNotNull(found);
        }


        [TestCleanup]
        public void CleanUp()
        {
            TestHelper.DeleteAllInDB();

        }
}
}
