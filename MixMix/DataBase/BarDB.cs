using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Entities;


namespace DataBase
{
    public class BarDB
    {
        private static readonly BarDB instance = new BarDB();
        private BarDB() { }

        public static BarDB Instance
        {
            get
            {
                return instance;
            }
        }

        public bool DecrementStock(int drinkId, int barId)
        {
            TransactionOptions transactionoption = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionoption))
            {
                using (SqlConnection connection = DBConnection.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("exec DecrementStock @BarId = @inputBarId, @DrinkId = @inputDrinkId", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@inputBarId", barId);
                        cmd.Parameters.AddWithValue("@inputDrinkId", drinkId);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            return false;
                        }
                    }
                }
                scope.Complete();
                return true;
            }
        }


        public Bar Create(Bar bar, int managerId, string password)
        {
            //TODO TransactionScope - Fjernes og timeout fejl kommer ikke ??
            using (TransactionScope rootScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (SqlConnection connection = DBConnection.GetSqlConnection())
                {
                    if (bar.Address.Id == default(int))
                    {
                        bar.Address = LocationDB.Instance.CreateAddress(bar.Address);
                    }
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        string hashedPassword = Security.GenerateHashedPassword(password, out string salt);

                        cmd.CommandText = "insert into Bars (name, phonenumber, email, username, hashedPassword, salt, address_ID, manager_ID) output INSERTED.ID values(@Name, @phonenumber, @email, @username, @hashedpassword, @salt, @addressId, @managerId);";
                        cmd.Parameters.AddWithValue("@name", bar.Name);
                        cmd.Parameters.AddWithValue("@addressId", bar.Address.Id);
                        cmd.Parameters.AddWithValue("@phonenumber", bar.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", bar.Email);
                        cmd.Parameters.AddWithValue("@username", bar.Username);
                        cmd.Parameters.AddWithValue("@managerId", managerId);
                        cmd.Parameters.AddWithValue("@hashedpassword", hashedPassword);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        bar.ID = (int)cmd.ExecuteScalar(); //TODO

                        if (bar.ID == default(int))
                        {
                            return null;
                        }

                    }
                }
                rootScope.Complete();
            }
            return bar;
        }

        public Bar Login(string username, string password)
        {
            string salt = "";
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select salt from Bars where username = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salt = reader.GetString("salt");
                        }
                    }
                }
            }
            if (salt == "")
            {
                return null;
            }
            string hashedPassword = Security.GenerateHashedPassword(password, salt);
            int id = default(int);
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id from Bars where username = @username AND HashedPassword = @HashedPassword";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32("id");
                        }

                    }
                }
            }
            if (id == default(int))
            {
                return null;
            }
            return Find(id);
        }
        public bool CreateStock(int barId, Stock stock)
        {
            bool result = false;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"insert into Stocks (quantity, ingredient_ID, bar_ID) values(@quantity, @ingredientId, @barId);";
                    cmd.Parameters.AddWithValue("@quantity", stock.Quantity);
                    cmd.Parameters.AddWithValue("@ingredientId", stock.Ingredient.Id);
                    cmd.Parameters.AddWithValue("@barId", barId);
                    result = cmd.ExecuteNonQuery() == 1;
                }
            }
            return result;
        }

        public List<Stock> GetAllStocks(int barId)
        {
            List<Stock> stocks = new List<Stock>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT quantity, name, id, bar_ID, alchVol, measurable FROM Stocks INNER JOIN Ingredients ON ingredient_ID = id WHERE bar_ID = @barId;";
                    cmd.Parameters.AddWithValue("@barId", barId);
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                Id = resultSet.GetInt32("id"),
                                Name = resultSet.GetString("name"),
                                Alch = resultSet.GetDouble("alchVol"),
                                Measurable = resultSet.GetBoolean("measurable")

                            };
                            Stock stock = new Stock
                            {
                                Ingredient = ingredient,
                                Quantity = resultSet.GetDouble("quantity")
                            };
                            stocks.Add(stock);

                        }
                    }
                }
            }
            return stocks;
        }

        public Bar Find(int barId)
        {
            Bar bar = null;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    //Selects Bar and belonging Address, Zipcode & Country with their Ids (with aliases)
                    cmd.CommandText = "Select b.id as barId, b.name as barName, b.address_ID, b.email, b.username, b.phonenumber, " +
                    "a.name as addressName, a.id as addressId, " +
                    "z.name as cityName, z.zipcode, z.id as zipcodeId, " +
                    "c.name as countryName, c.id as countryId " +
                    "from Bars b, Addresses a, Zipcodes z, Countries c " +
                   $"where b.id = @barId and b.address_ID = a.id and a.zipcode_ID = z.id and z.country_ID = c.id;";
                    cmd.Parameters.AddWithValue("@barId", barId);
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            string name = resultSet.GetString(resultSet.GetOrdinal("barName"));
                            Country country = new Country(resultSet.GetInt32(resultSet.GetOrdinal("countryId")), resultSet.GetString(resultSet.GetOrdinal("countryName")));
                            Zipcode zipcode = new Zipcode(resultSet.GetInt32(resultSet.GetOrdinal("zipcodeId")), resultSet.GetString(resultSet.GetOrdinal("zipcode")), resultSet.GetString(resultSet.GetOrdinal("cityName")), country);
                            Address address = new Address(resultSet.GetInt32(resultSet.GetOrdinal("addressId")), resultSet.GetString(resultSet.GetOrdinal("addressName")), zipcode);
                            string phonenumber = resultSet.GetString(resultSet.GetOrdinal("phonenumber"));
                            string email = resultSet.GetString(resultSet.GetOrdinal("email"));
                            string username = resultSet.GetString(resultSet.GetOrdinal("username"));
                            int id = resultSet.GetInt32(resultSet.GetOrdinal("barId"));
                            bar = new Bar(name, address, phonenumber, email, username, id);
                        }
                    }
                }
            }
            return bar;
        }

        public bool Update(Bar bar)
        {
            bool success = false;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"Update Bars SET Name = @barname, address_ID = @address, Phonenumber = @phonenumber, Email = @email, Username = @username WHERE id = @Id";
                    cmd.Parameters.AddWithValue("@barname", bar.Name);
                    cmd.Parameters.AddWithValue("@address", bar.Address.Id);
                    cmd.Parameters.AddWithValue("@phonenumber", bar.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", bar.Email);
                    cmd.Parameters.AddWithValue("@username", bar.Username);
                    cmd.Parameters.AddWithValue("@Id", bar.ID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool Delete(int id)
        {
            bool success = false;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"Delete from Bars where id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                }
            }
            return success;
        }

        //TODO den her skal vi lige have snakket om - Bruger aldrig managerID ? 
        public List<Bar> GetBarsByManagerId(int managerId)
        {
            List<Bar> bars = new List<Bar>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, name from Bars;";
                    using (SqlDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            int id = resultSet.GetInt32(resultSet.GetOrdinal("id"));
                            string name = resultSet.GetString(resultSet.GetOrdinal("name"));
                            Bar bar = new Bar { ID = id, Name = name };
                            bars.Add(bar);

                        }
                    }
                }
            }
            return bars;
        }

        public List<Bar> FindAll(string zipcode)
        {
            List<Bar> bars = new List<Bar>();

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select b.id, b.name as barName, b.phonenumber, b.email, a.name as addressName from Bars b, Addresses a, Zipcodes z where b.address_ID = a.id and a.zipcode_ID=z.id and z.zipcode = @zipcode;";
                    cmd.Parameters.AddWithValue("@zipcode", zipcode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string barName = reader.GetString("barName");
                            Address address = new Address(reader.GetString("addressName"));
                            string phonenumber = reader.GetString("phonenumber");
                            string email = reader.GetString("email");
                            string username = "";

                            bars.Add(new Bar(barName, address, phonenumber, email, username, id));
                        }
                    }
                }
            }
            return bars;
        }

        public double FindIngredientPrice(int ingredientId, int barId)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select p.sellingPrice from Prices p where p.ingredient_ID = @ingredientID and p.bar_ID=@barID;";
                    cmd.Parameters.AddWithValue("@ingredientID", ingredientId);
                    cmd.Parameters.AddWithValue("@barID", barId);

                    return (double)cmd.ExecuteScalar();
                }
            }
        }

        public bool UpdateStock(List<Stock> stocks, int barid)
        {
            TransactionOptions transactionoption = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionoption))
            {
                foreach (var item in stocks)
                {
                    using (SqlConnection connection = DBConnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "update Stocks set quantity = @quant where ingredient_ID = @id and bar_ID = @barID;";
                            cmd.Parameters.AddWithValue("@quant", item.Quantity);
                            cmd.Parameters.AddWithValue("@id", item.Ingredient.Id);
                            cmd.Parameters.AddWithValue("@barID", barid);
                            if (!(cmd.ExecuteNonQuery() == 1))
                            {
                                return false;
                            }
                        }
                    }
                }
                scope.Complete();
            }
            return true;
        }
    }
}