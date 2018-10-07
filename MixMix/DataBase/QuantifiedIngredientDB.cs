using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataBase
{
    public class QuantifiedIngredientDB
    {
        public bool Insert(List<QuantifiedIngredient> quantifiedIngredients, int drinkId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (SqlConnection connection = DBConnection.GetSqlConnection())
                {
                    foreach (QuantifiedIngredient quantifiedIngredient in quantifiedIngredients)
                    {
                        using (SqlCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "insert into QuantifiedIngredients (quantity, ingredient_ID, measurement_ID, drink_ID) output inserted.id values(@quantity, @ingredient_ID, @measurement_ID, @drink_ID);";
                            cmd.Parameters.AddWithValue("@quantity", quantifiedIngredient.Quantity);
                            cmd.Parameters.AddWithValue("@ingredient_ID", quantifiedIngredient.Ingredient.Id);
                            cmd.Parameters.AddWithValue("@measurement_ID", quantifiedIngredient.Measurement.Id);
                            cmd.Parameters.AddWithValue("@drink_ID", drinkId);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                scope.Complete();
                return true;
            }
        }
    }
}
