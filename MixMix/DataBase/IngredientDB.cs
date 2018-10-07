using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class IngredientDB
    {
        public Ingredient Insert(Ingredient ingredient)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "insert into Ingredients (name, measurable, alchVol) output INSERTED.id values (@name, @measurable, @alchVol)";
                    cmd.Parameters.AddWithValue("@name", ingredient.Name);
                    cmd.Parameters.AddWithValue("@measurable", ingredient.Measurable);
                    cmd.Parameters.AddWithValue("@alchVol", ingredient.Alch);
                    ingredient.Id = (int)cmd.ExecuteScalar();
                }
            }
            if (ingredient.Id == default(int))
            {
                return null;
            }
            return ingredient;
        }

        public List<Ingredient> FindAll()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, name, measurable, alchVol from Ingredients";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Measurable = reader.GetBoolean("measurable"),
                                Alch = reader.GetDouble("alchVol")
                            };
                            ingredients.Add(ingredient);
                        }
                    }
                }
            }
            return ingredients;
        }

        public List<Ingredient> Find(string name)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Select id, name, measurable, alchVol from Ingredients where name = @name";
                    cmd.Parameters.AddWithValue("@name", name);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("Id");
                            bool measurable = reader.GetBoolean("measurable");
                            float alchVol = reader.GetFloat("alchVol");
                            ingredients.Add(new Ingredient() { Id = id, Name = name, Measurable = measurable, Alch = alchVol });
                        }
                        if (ingredients.Count == 0)
                        {
                            return null;
                        }
                        else return ingredients;
                    }
                }
            }
        }

        public Ingredient Find(int id)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Select id, name, measurable, alchVol from Ingredients where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString("name");
                            bool measurable = reader.GetBoolean("measurable");
                            float alchVol = reader.GetFloat("alchVol");
                            return new Ingredient() { Id = id, Name = name, Measurable = measurable, Alch = alchVol };
                        }
                    }
                }
            }
            return null;
        }

        public bool Update(Ingredient obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
