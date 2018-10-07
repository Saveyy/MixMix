using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Controller;
using DataBase;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class BarTest
    {
        BarController controller = new BarController();

        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }


        [TestMethod]
        public void BarCreate()
        {
            //Setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };

            int preCount = TestHelper.CountTable("Bars");
            preCount = preCount + 1;
            //act
            Bar Foundbar = controller.Create(bar, m.Id, "TestPassword");

            int postCount = TestHelper.CountTable("Bars");
            //Assert
            Assert.AreEqual(preCount, postCount, "Bar was not Created Successfully");

            //setup2
            Bar bar2 = new Bar
            {
                Address = a,
                Email = "TestBarEmail2",
                Name = "TestBarName2",
                PhoneNumber = "TestBarP2",
                Username = "TestBarUsername2"
            };
            //act2
            int preCount2 = TestHelper.CountTable("Bars");
            Bar createdBar = controller.Create(m.Id, bar2.Name, a, bar2.PhoneNumber, bar2.Email, bar2.Username, "TestPassword2");
            int postCount2 = TestHelper.CountTable("Bars");
            //assert2
            Assert.AreEqual(preCount,postCount,0, "Bar did not get created successfully");

        }

        [TestMethod]
        public void barCreateStock()
        {
            IngredientController ingredientController = new IngredientController();
            DrinkController drinkController = new DrinkController();
            //setup
            Ingredient vodka = new Ingredient
            {
                Name = "TestVodka",
                Measurable = true,
                Alch = 37.5
            };

            Ingredient juice = new Ingredient
            {
                Name = "TestJuice",
                Measurable = false,
                Alch = 0
            };

            vodka = ingredientController.Create(vodka);
            juice = ingredientController.Create(juice);
            
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;
            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            Bar Createdbar = controller.Create(bar, m.Id, "TestPassword");

            ICollection<Measurement> measurements = new DrinkController().FindAllMeasurements();
            var allStocks = controller.GetAllStocks(bar.ID);
            int precount = allStocks.Count;
            int preVodkaStockCount = allStocks.Where(e => e.Ingredient.Equals(vodka)).Count();
            int preJuiceStockCount = allStocks.Where(e => e.Ingredient.Equals(juice)).Count();

            //act
            controller.CreateStock(Createdbar.ID, 20, vodka, measurements.FirstOrDefault().Id);

            preVodkaStockCount++;   //Just creted one vodka. so we increment
            precount++;             // Just created one more stock in the DB so we increment

            controller.CreateStock(Createdbar.ID, 20, juice, null);
            precount++; //Just created one juice, so we increment
            preJuiceStockCount++; // Just craeted one juice so we increment

            List<Stock> stocksFound = controller.GetAllStocks(bar.ID);
            //assert
            bool success = (stocksFound.Where(e => e.Ingredient.Equals(vodka))).Count() == preVodkaStockCount &&
                            stocksFound.Count() == precount;
            

            Assert.IsTrue(success,"Stock measurable was not created successfully");
            int actual = stocksFound.Where(e => e.Ingredient.Equals(juice)).Count();


            success = (actual == preJuiceStockCount && stocksFound.Count() == precount);

            Assert.IsTrue(success, "Stock not mreasurable was not created successfully");
        }


        [TestMethod]
        public void BarFindByID()
        {
            //setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            Bar barCreated = controller.Create(bar, m.Id, "TestPassword");
            //act
            Bar barFound = controller.Find(barCreated.ID);
            //Assert
            Assert.AreEqual(barCreated, barFound, "Bar wasn't found");
        }

       

        [TestMethod]
        public void BarrUpdateName()
        {
            //Setup
            //setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            Bar expected = controller.Create(bar, m.Id, "TestPassword");
            expected.Name = "TestNameUpdated";
            //Act
            controller.Update(expected.ID, expected.Name, a, expected.PhoneNumber, expected.Email, expected.Username);
            Bar actual = controller.Find(expected.ID);
            //Assert
            Assert.AreEqual(expected, actual, "Bar name was not updated correctly");
        }

        public void BarUpdatePhone()
        {
            //setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            Bar expected = controller.Create(bar, m.Id, "TestPassword");
            expected.PhoneNumber = "TestphUp";
            //Act
            controller.Update(expected.ID, expected.Name, a, expected.PhoneNumber, expected.Email, expected.Username);
            Bar actual = controller.Find(expected.ID);
            //Assert
            Assert.AreEqual(expected, actual, "Bar name was not updated correctly");
        }

        [TestMethod]
        public void BarDelete()
        {
            //Setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };

            Bar expected = controller.Create(bar, m.Id, "TestPassword");
            //Act
            bool isSuccess = controller.Delete(expected.ID);

            //Assert 1 
            Bar notFound = controller.Find(expected.ID);
            Assert.IsNull(notFound);

            //Assert 2
            Assert.AreEqual(isSuccess, true, "The method returns an unexpected value");
        }

        [TestMethod]
        public void BarLogin()
        {
            //Setup
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;

            Bar bar = new Bar
            {
                Address = a,
                Email = "TestBarEmail",
                Name = "TestBarName",
                PhoneNumber = "TestBarP",
                Username = "TestBarUsername"
            };
            Bar expected = controller.Create(bar, m.Id, "TestPassword");

            //act
            Bar found = controller.Login("TestBarUsername", "TestPassword");

            //assert
            Assert.IsNotNull(found);
        }

        [TestMethod]
        public void BarFindAll()
        {
           
            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                Assert.Fail("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;
            TestHelper.MakeTestBars(zip.Id, 30);
            int precount = TestHelper.CountTableBarsWithZipID(zip.Id);


            //setup
            Zipcode z = LocationDB.Instance.findZipById(zip.Id);
            if (z == null)
            {
                Assert.Fail("findZip failed");
            }

            //act
            IList<Bar> bars = controller.FindAll(z.Zip);
            //arrange
            Assert.AreEqual(bars.Count, precount);
            
        }

        /* NIKO FÅR IKKE LOV TIL AT LAVE TEST IGEN
        [TestMethod]
        public void TestDecrementStock()
        {
            //bool firstCheck = controller.DecrementStock(1, 1);
            //Assert.IsTrue(firstCheck);
            bool secondCheck = controller.DecrementStock(1, 1);
            Assert.IsFalse(secondCheck);
        }
        */

        [TestCleanup]
        public void CleanUp()
        {
            TestHelper.DeleteAllInDB();
        }
    }
}
