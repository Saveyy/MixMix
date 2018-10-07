using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataBase
{
    public class CustomerDB : IDbCRUD<Customer>
    {
        public Customer Insert(Customer c, string password)
        {
            string hashedPassword = Security.GenerateHashedPassword(password, out string salt);

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"insert into customers (name, phonenumber, email, username, hashedpassword, salt) output INSERTED.ID values (@name, @phonenumber, @email, @username, @hashedpassword, @salt)";
                    cmd.Parameters.AddWithValue("@name", c.Name);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@phonenumber", c.PhoneNumber);
                    cmd.Parameters.AddWithValue("@username", c.Username);
                    cmd.Parameters.AddWithValue("@hashedpassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    c.Id = (int)cmd.ExecuteScalar();
                    //If id equals unsetValue, return null, as no object was created in Database.
                    if (c.Id == -1)
                    {
                        return null;
                    }
                }
            }
            return c;
        }

        public Customer Find(int id)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, name, phonenumber, email, username from customers where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cId = reader.GetInt32("id");
                            string name = reader.GetString("name");
                            string phonenumber = reader.GetString("phonenumber");
                            string email = reader.GetString("email");
                            string username = reader.GetString("username");
                            return new Customer(name, phonenumber, email, username, cId);
                        }
                    }
                }
            }
            return null;
        }

        public Customer Find(string username)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, name, phonenumber, email, username from customers where username = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cId = reader.GetInt32("id");
                            string name = reader.GetString("name");
                            string phonenumber = reader.GetString("phonenumber");
                            string email = reader.GetString("email");
                            return new Customer(name, phonenumber, email, username, cId);
                        }
                    }
                }
            }
            return null;
        }

        public List<Customer> Find(CustomerColumn column, string search)
        {
            List<Customer> c = new List<Customer>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select id, name, phonenumber, email, username from customers where {column.ToString()} = @search";
                    cmd.Parameters.AddWithValue($"@search", search);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string name = reader.GetString("name");
                            string phonenumber = reader.GetString("phonenumber");
                            string email = reader.GetString("email");
                            string username = reader.GetString("username");
                            c.Add(new Customer(name, phonenumber, email, username, id));
                        }
                    }
                }
            }
            if (c.Count == 0)
            {
                return null;
            }
            return c;
        }

        public bool Update(Customer customer)
        {
            bool isSuccess = false;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "update customers set name = @name, phonenumber = @phonenumber, email = @email where id=@id;";
                    cmd.Parameters.AddWithValue("@name", customer.Name);
                    cmd.Parameters.AddWithValue("@phonenumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        isSuccess = true;
                    }
                }
            }
            return isSuccess;
        }

        public bool Delete(int id)
        {
            bool isSuccess = false;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "delete from customers where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        isSuccess = true;
                    }
                }
            }
            return isSuccess;
        }

        public Customer Login(string username, string password)
        {
            string salt = "";
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select salt from customers where username = @username";
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

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, name, phonenumber, email, username from customers where username = @username AND HashedPassword = @HashedPassword";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Customer() { Id = reader.GetInt32("id"), Name = reader.GetString("name"), PhoneNumber = reader.GetString("phonenumber"), Email= reader.GetString("email"), Username= reader.GetString("username") };
                        }
                    }
                }
            }
            return null;
        }
    }
}
