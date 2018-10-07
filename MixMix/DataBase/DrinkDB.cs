using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace DataBase
{
    public class DrinkDB
    {
        //public Drink Insert(Drink obj)
        //{
        //    //DrinkRecipe
        //    using (SqlConnection connection = DBConnection.GetSqlConnection())
        //    {
        //        using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            using (TransactionScope scope2 = new TransactionScope(TransactionScopeOption.Required))
        //            {
        //                using (SqlCommand cmd = connection.CreateCommand())
        //                {
        //                    cmd.CommandText = "insert into Drinks (recipe) output INSERTED.ID values (@recipe)";
        //                    cmd.Parameters.AddWithValue("@recipe", obj.Recipe);
        //                    obj.Id = (int)cmd.ExecuteScalar();
        //                }
        //                if (obj.Id == default(int))
        //                {
        //                    //TODO MAKE EXCEPTION
        //                    throw new NotImplementedException();
        //                }
        //                scope2.Complete();
        //            }

        //            //DrinkNames
        //            using (TransactionScope scope3 = new TransactionScope(TransactionScopeOption.Required))
        //            {
        //                foreach (var item in obj.Names)
        //                {
        //                    using (SqlCommand cmd2 = connection.CreateCommand())
        //                    {
        //                        cmd2.CommandText = "insert into DrinkNames (name, drink_ID) values (@name, @drink_ID)";
        //                        cmd2.Parameters.AddWithValue("@name", item);
        //                        cmd2.Parameters.AddWithValue("@drink_ID", obj.Id);
        //                        int rowsAffected = cmd2.ExecuteNonQuery();
        //                        if (rowsAffected == 0)
        //                        {
        //                            //TODO implement Exception
        //                            throw new NotImplementedException();
        //                        }
        //                    }
        //                }
        //                scope3.Complete();
        //            }
        //            //DrinkIngrediences
        //            using (TransactionScope scope4 = new TransactionScope(TransactionScopeOption.Required))
        //            {
        //                using (SqlCommand cmd3 = connection.CreateCommand())
        //                {
        //                    foreach (var item in obj.Ingredients)
        //                    {
        //                        cmd3.CommandText = "insert into QuantifiedIngredients (quantity, ingredient_ID, measurement_ID, drink_ID) values (@quantity, @ingredientID, @measurementID, @drinkID)";
        //                        cmd3.Parameters.AddWithValue("@quantity", item.Quantity);
        //                        cmd3.Parameters.AddWithValue("@ingredientID", item.Ingredient.Id);
        //                        cmd3.Parameters.AddWithValue("@measurementID", item.Measurement.Id);
        //                        cmd3.Parameters.AddWithValue("@drinkID", obj.Id);
        //                    }
        //                }
        //                scope4.Complete();
        //            }
        //            scope1.Complete();
        //        }
        //    }
        //    return obj;
        //}

        public Drink Insert(Drink drink)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "insert into Drinks (recipe) output inserted.id values(@recipe);";
                    cmd.Parameters.AddWithValue("@recipe", drink.Recipe);
                    drink.Id = (int)cmd.ExecuteScalar();

                    if (drink.Id == default(int))
                    {
                        return null;
                    }
                }
            }
            return drink;
        }

        public bool InsertDrinkNames(string drinkName, int drinkId)
        {
            bool success = true;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "insert into DrinkNames (name, drink_ID) output inserted.id values(@name, @drink_ID);";
                    cmd.Parameters.AddWithValue("@name", drinkName);
                    cmd.Parameters.AddWithValue("@drink_ID", drinkId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        success = false;
                    }
                }
            }
            return success;
        }

        public bool CheckDrinkName(string name)
        {
            bool found = false;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select name from DrinkNames where name = @name;";
                    cmd.Parameters.AddWithValue("@name", name);

                    found = cmd.ExecuteReader().HasRows;
                }
            }

            return found;
        }

        public double FindDrinkPriceById(int drinkId, int barId)
        {
            double price = 0;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select p.sellingPrice, qfi.quantity from Prices p, Ingredients i, Drinks d, QuantifiedIngredients qfi, Bars b where d.id = @drinkId and b.id = @barId and d.id = qfi.drink_ID and qfi.ingredient_ID = i.id and p.ingredient_ID = i.id;";
                    cmd.Parameters.AddWithValue("@drinkId", drinkId);
                    cmd.Parameters.AddWithValue("@barId", barId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            price += (double)reader.GetDecimal("sellingPrice") * reader.GetDouble("quantity");
                        }
                    }
                }
            }
            return price;
        }

        public void InsertPrice(Bar bar, Price price)
        {
            int result = 0;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "insert into Prices (sellingPrice, byingPrice, ingredient_ID, bar_ID) values (@SellingPRice, @buyingPrice, @ingredientID, @barID)";
                    cmd.Parameters.AddWithValue("@SellingPrice", price.SellingPrice);
                    cmd.Parameters.AddWithValue("@buyingPrice", price.BuyingPrice);
                    cmd.Parameters.AddWithValue("@ingredientID", price.Ingredient.Id);
                    cmd.Parameters.AddWithValue("@barID", bar.ID);
                    result = cmd.ExecuteNonQuery();
                }
            }
            if (result != 1)
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<int, DrinkViewModel> FindAllAvailableDrinks(Bar bar)
        {
            Dictionary<int, DrinkViewModel> drinks = new Dictionary<int, DrinkViewModel>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                TransactionOptions transactionoption = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionoption))
                {
                    using (SqlCommand cmd = new SqlCommand("exec FindAllAvailableDrinks @BarId = @inputBarId", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@inputBarId", bar.ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //Der bliver returneret to resultset. Det første er ingredienserne der skal bruges til de drinks der kan lavet. Det andet er alle navnene på drinksne.
                            while (reader.Read())
                            {
                                int quantity = reader.GetInt32(0);
                                string type = reader.GetString(1);
                                string name = reader.GetString(2);
                                int id = reader.GetInt32(3);

                                string ingredient = $"{quantity} {type} {name}, "; //quantity.ToString() + " " + type + " " + name + ", ";

                                if (drinks.Count == 0 || !drinks.ContainsKey(id))
                                {
                                    DrinkViewModel drinkView = new DrinkViewModel
                                    {
                                        Id = id,
                                        Ingredients = ingredient,
                                        Names = new List<string>()
                                    };
                                    drinks.Add(id, drinkView);
                                }
                                else
                                {
                                    drinks[id].Ingredients += ingredient;
                                }
                            }

                            //Avancerer til næste resultset.
                            reader.NextResult();

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);

                                drinks[id].Names.Add(name);
                            }
                        }
                    }
                }
                return drinks;
            }
        }

    }
}
