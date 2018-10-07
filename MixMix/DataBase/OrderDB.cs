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
    public class OrderDB
    {
        public int CreateOrder(Order order)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (SqlConnection connection = DBConnection.GetSqlConnection())
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into Orders (orderTime, status, bar_ID, customer_ID) output inserted.orderNumber, inserted.id values(@orderTime, @status, @bar_ID, @customer_ID);";
                        cmd.Parameters.AddWithValue("@orderTime", order.OrderTime);
                        cmd.Parameters.AddWithValue("@status", order.Status);
                        cmd.Parameters.AddWithValue("@bar_ID", order.Bar.ID);
                        cmd.Parameters.AddWithValue("@customer_ID", order.Customer.Id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderNumber = reader.GetInt32("orderNumber");
                                order.Id = reader.GetInt32("id");
                            }
                        }

                        foreach (OrderLine orderLine in order.OrderLines)
                        {
                            using (SqlCommand cmdOrderLines = connection.CreateCommand())
                            {
                                cmdOrderLines.CommandText = "insert into OrderLines(quantity, subtotal, drink_ID, order_ID) values(@quantity, @subtotal, @drink_ID, @order_ID);";
                                cmdOrderLines.Parameters.AddWithValue("@quantity", orderLine.Quantity);
                                cmdOrderLines.Parameters.AddWithValue("@subtotal", orderLine.Subtotal);
                                cmdOrderLines.Parameters.AddWithValue("@drink_ID", orderLine.Drink.Id);
                                cmdOrderLines.Parameters.AddWithValue("@order_ID", order.Id);
                                cmdOrderLines.ExecuteNonQuery();
                            }
                        }
                    }

                    scope.Complete();
                }
            }
            return order.OrderNumber;
        }
    }
}
