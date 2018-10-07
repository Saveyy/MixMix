using Controller;
using DataBase;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public static class TestHelper
    {
        /// <summary>
        /// Counts the number of rows in Specified table
        /// </summary>
        /// <param name="input">Name of the given Table</param>
        /// <returns></returns>
        public static int CountTable(string input)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select count(*) from {input};";
                    return (Int32)cmd.ExecuteScalar();
                }
            }
        }

        public static int CountTableBarsWithZipID(int zipID)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select COUNT(*) from Addresses a, Zipcodes z, Bars b where a.zipcode_ID = z.id and b.address_ID = a.id and zipcode_ID = @p1";
                    cmd.Parameters.AddWithValue("@p1", zipID);
                    return (Int32)cmd.ExecuteScalar();
                }
            }
        }

        public static void DeleteTest(string tableName, string columnName)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"Delete from {tableName} where {columnName} like 'test%';";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static BarController barController = new BarController();
        
        public static void MakeTestBars()
        {
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                throw new ArgumentNullException("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(1);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;
            for (int i = 0; i < 30; i++)
            {
                Bar bar = new Bar
                {
                    Address = a,
                    Email = "TestBarEmail" + i,
                    Name = "TestBarName" + i,
                    PhoneNumber = "TestBarP" + i,
                    Username = "TestBarUsername" + i
                };

                Bar expected = barController.Create(bar, m.Id, "TestPassword");
            }
        }

        public static void MakeTestBars(int ZipID, int amountOfBarsToCreate)
        {
            ManagerController mc = new ManagerController();
            Manager manager = new Manager("TestName", "TestPhonenumber", "TestEmail", "TestUsername", null);
            Manager m = mc.CreateManager(manager, "TestPassword");

            Country country = LocationDB.Instance.getCountryById(1);
            if (country is null)
            {
                throw new ArgumentNullException("Country is null");
            }
            Zipcode zip = LocationDB.Instance.findZipById(ZipID);
            Address a = new Address("TestVej", zip);
            zip.Country = country;
            a.Zipcode = zip;
            for (int i = 0; i < amountOfBarsToCreate; i++)
            {
                Bar bar = new Bar
                {
                    Address = a,
                    Email = "TestBarEmail" + i,
                    Name = "TestBarName" + i,
                    PhoneNumber = "TestBarP" + i,
                    Username = "TestBarUsername" + i
                };

                barController.Create(bar, m.Id, "TestPassword");
            }
        }

        public static int CountAllIngredients()
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Select COUNT(*) from ingredients";
                    return (Int32)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EqualsAll<T>(this IList<T> a, IList<T> b)
        {
            if (a == null || b == null)
                return false; // null == null reutrn false

            return (a.Count != b.Count) ? false : a.SequenceEqual(b);
                
        }

        internal static object FindAllMeasurements()
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Select COUNT(*) from measurements";
                    return (Int32)cmd.ExecuteScalar();
                }
            }
        }

        public static void DeleteAllInDB()
        {
            TestHelper.DeleteTest("Bars", "username");
            TestHelper.DeleteTest("addresses", "name");
            TestHelper.DeleteTest("Ingredients", "name");
            TestHelper.DeleteTest("Drinks", "recipe");
            TestHelper.DeleteTest("DrinkNames", "name");
            TestHelper.DeleteTest("Managers", "username");
            TestHelper.DeleteTest("customers", "username");
        }
    }
}
