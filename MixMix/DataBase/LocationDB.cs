using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Entities;

namespace DataBase
{
    public class LocationDB
    {
        private static LocationDB instance = new LocationDB();
        private LocationDB() { }

        public static LocationDB Instance
        {
            get
            {
                return instance;
            }
        }

        //Denne metode modtager en adresse som er skrevet ind af brugeren
        //og et zipcodeId som også er valgt af brugeren
        //Vi sørger for at Countries og Zipcodes er genereret på forhånd af os
        //public Address CreateAddress(Address address, int? zipcodeId)
        //{
        //    using (SqlConnection connection = DBConnection.GetSqlConnection())
        //    {
        //        using (SqlCommand cmd = connection.CreateCommand())
        //        {
        //            cmd.CommandText = $"insert into Addresses (name, zipcode_ID) output INSERTED.ID values(@addressName, @zipcodeId);";
        //            cmd.Parameters.AddWithValue("@addressName", address.AddressName);
        //            cmd.Parameters.AddWithValue("@zipcodeId", zipcodeId ?? address.Zipcode.Id);
        //            address.Id = (int)cmd.ExecuteScalar();
        //        }
        //    }
        //    return address;
        //}

        public Address CreateAddress(Address address, int zipcode)
        {
            using(SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    int zipcodeId = FindZipcodeIdByZipcode(zipcode);
                    cmd.CommandText = $"insert into Addresses (name, zipcode_ID) output INSERTED.ID values(@addressName, @zipcodeId);";
                    cmd.Parameters.AddWithValue("@addressName", address.AddressName);
                    cmd.Parameters.AddWithValue("@zipcodeId", zipcodeId);
                    address.Id = (int)cmd.ExecuteScalar();
                }
            }
            return address;
        }

        public int FindZipcodeIdByZipcode(int zipcode)
        {
            int zipcodeId = 0;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id from Zipcodes where zipcode = @zipcode;";
                    cmd.Parameters.AddWithValue("@zipcode", zipcode);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zipcodeId = reader.GetInt32("id");
                        }
                    }
                }
            }
            return zipcodeId;
        }

        public Zipcode findZipById(int id)
        {
            Zipcode zipcode = null;
            List<Country> cList = LocationDB.instance.getCountries();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select zipcode, name, country_ID from zipcodes where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zipcode = new Zipcode
                            {
                                Id = id,
                                CityName = reader.GetString("name"),
                                Zip = reader.GetString("zipcode")
                            };
                            int cid = reader.GetInt32("country_ID");
                            zipcode.Country = cList.Where(e => e.Id == cid).FirstOrDefault();
                            if (zipcode.Country == null)
                            {
                                return null;
                            }
                        }
                    }

                    return zipcode;
                }
            }
        }

        public Address FindAddress(int id)
        {
            Address a = new Address();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select (name, zipcode_Id) from Addresses where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a.AddressName = reader.GetString("name");
                            a.Zipcode = LocationDB.instance.findZipById(reader.GetInt32("zipcode_ID"));
                        }
                    }
                    return a;
                }
            }
        }

        public Address CreateAddress(Address address)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (SqlConnection connection = DBConnection.GetSqlConnection())
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = $"insert into Addresses (name, zipcode_ID) output INSERTED.ID values(@addressName, @zipcodeId);";
                        cmd.Parameters.AddWithValue("@addressName", address.AddressName);
                        cmd.Parameters.AddWithValue("@zipcodeId", address.Zipcode.Id);
                        address.Id = (int)cmd.ExecuteScalar();
                    }
                    scope.Complete();
                }
            }
            return address;
        }

        public bool Delete(int id)
        {
            bool success = false;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE from Addresses WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    success = true;
                }
            }
            return success;
        }


        public bool UpdateAddress(Address address)
        {
            int rowsAffacted = 0;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE Addresses SET name = @name WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@name", address.AddressName);
                    cmd.Parameters.AddWithValue("@id", address.Id);
                    rowsAffacted = cmd.ExecuteNonQuery();
                }
                
            }

            return rowsAffacted == 1;
        }




        #region old method
        public Address UpdateAddress(Address address, int zipcodeId)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE Addresses SET addressName = @address, zipcode_ID = @zipcodeId WHERE id = @addressId;";
                    cmd.Parameters.AddWithValue("@address", address.AddressName);
                    cmd.Parameters.AddWithValue("@zipcodeId", zipcodeId);
                    cmd.Parameters.AddWithValue("@addressId", address.Id);
                    address.Id = (int)cmd.ExecuteScalar();
                }
            }
            return address;
        }
        #endregion

        public List<Country> getCountries()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, name from Countries;";
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            int id = resultSet.GetInt32(resultSet.GetOrdinal("id"));
                            string name = resultSet.GetString(resultSet.GetOrdinal("name"));
                            Country country = new Country(id, name);
                            countries.Add(country);

                        }
                    }
                }
            }
            return countries;
        }

        public Country getCountryById(int countryId)
        {
            Country country = null;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, name from Countries where id = @id;";
                    cmd.Parameters.AddWithValue("@id", countryId);
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            int id = resultSet.GetInt32(resultSet.GetOrdinal("id"));
                            string name = resultSet.GetString(resultSet.GetOrdinal("name"));
                            country = new Country(id, name);

                        }
                    }
                }
            }
            return country;
        }

        public List<Zipcode> getZipcodesByCountryId(int countryId)
        {
            Country country = getCountryById(countryId);
            List<Zipcode> zipcodes = new List<Zipcode>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {

                using (SqlCommand cmd = connection.CreateCommand())
                {

                    cmd.CommandText = $"SELECT id, zipcode, name from Zipcodes where country_ID = @id;";
                    cmd.Parameters.AddWithValue("@id", countryId);
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            int zipcodeId = resultSet.GetInt32(resultSet.GetOrdinal("id"));
                            string zipcodeZip = resultSet.GetString(resultSet.GetOrdinal("zipcode"));
                            string zipcodeCity = resultSet.GetString(resultSet.GetOrdinal("name"));
                            Zipcode zipcode = new Zipcode(zipcodeId, zipcodeZip, zipcodeCity, country);
                            zipcodes.Add(zipcode);

                        }
                    }
                }
            }
            return zipcodes;
        }


        //public List<Zipcode> getZipcodes()
        //{

        //    List<Zipcode> zipcodes = null;
        //    using (SqlConnection connection = DBConnection.GetSqlConnection())
        //    {

        //        using (SqlCommand cmd = connection.CreateCommand())
        //        {
        //            //We want this method to get all zipcodes and their associated country, but we don't
        //            cmd.CommandText = $"SELECT z.id as zipcodeId, z.zipcode as zipcodeZip, z.name as zipcodeCity, c.id as countryId, c.name as countryName from Zipcodes z, Countries c);";
        //            using (SqlDataReader resultSet = cmd.ExecuteReader())
        //            {
        //                while (resultSet.Read())
        //                {
        //                    int countryId = resultSet.GetInt32(resultSet.GetOrdinal("countryId"));
        //                    string countryName = resultSet.GetString(resultSet.GetOrdinal("countryName"));
        //                    Country country = new Country(countryId, countryName);

        //                    int zipcodeId = resultSet.GetInt32(resultSet.GetOrdinal("zipcodeId"));
        //                    string zipcodeZip = resultSet.GetString(resultSet.GetOrdinal("zipcodeZip"));
        //                    string zipcodeCity = resultSet.GetString(resultSet.GetOrdinal("zipcodeCity"));
        //                    Zipcode zipcode = new Zipcode(zipcodeId, zipcodeZip, zipcodeCity, country);
        //                    zipcodes.Add(zipcode);

        //                }
        //            }
        //        }
        //    }
        //    return zipcodes;
        //}

    }
}
