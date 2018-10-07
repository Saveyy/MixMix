using System;
using System.Data.SqlClient;
using Controller;
using DataBase;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class LocationTest
    {
        private LocationController controller = new LocationController();

        [TestInitialize]
        public void TestInitialize()
        {
            CleanUp();
        }

        [TestMethod]
        public void AddressCreate()
        {
            //setup
            Address a = CreateNewAddress();

            int preCount = TestHelper.CountTable("addresses");
            preCount += 1;
            //act
            Address made = controller.CreateAddress(a);
            int postCount = TestHelper.CountTable("addresses");

            //arrange
            Assert.AreEqual(preCount, postCount);

        }

        [TestMethod]
        public void AddressDelete()
        {
            //setup
            Address a = CreateNewAddress();
            a = controller.CreateAddress(a);
            int preCount = TestHelper.CountTable("addresses");
            preCount -= 1;
            
            //act
            bool result = controller.Delete(a.Id);
            int postCount = TestHelper.CountTable("addresses");

            //arrange
            Assert.AreEqual(preCount, postCount);
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void AddressUpdateName()
        {
            //setup
            Address expected = CreateNewAddress();
            expected = controller.CreateAddress(expected);
            expected.AddressName = "NameUpdatedTest";
            //act
            bool result = controller.Update(expected);
            string actualName = "";
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select name from addresses where id = @id";
                    cmd.Parameters.AddWithValue("@id", expected.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            actualName = reader.GetString("name");
                        }
                    }
                }
            }

            //arrange
            Assert.AreEqual(expected.AddressName, actualName);
            Assert.AreEqual(true, result);
        }




        private static Address CreateNewAddress()
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
            return a;
        }


        [TestCleanup]
        public void CleanUp()
        {
            TestHelper.DeleteAllInDB();

        }

    }
}