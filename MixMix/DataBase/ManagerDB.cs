using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ManagerDB : IDbCRUD<Manager>
    {
        public Manager Insert(Manager manager, string password)
        {
            string hashedPassword = Security.GenerateHashedPassword(password, out string salt);
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"insert into Managers (name, phonenumber, email, username, hashedpassword, salt) output INSERTED.ID values (@name, @phonenumber, @email, @username, @hashedpassword, @salt)";
                    cmd.Parameters.AddWithValue("@name", manager.Name);
                    cmd.Parameters.AddWithValue("@email", manager.Email);
                    cmd.Parameters.AddWithValue("@phonenumber", manager.PhoneNumber);
                    cmd.Parameters.AddWithValue("@username", manager.Username);
                    cmd.Parameters.AddWithValue("@hashedpassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    manager.Id = (int)cmd.ExecuteScalar();
                    //If id is equals unsetValue, return null, as no object was created in Database.
                    if (manager.Id == -1)
                    {
                        return null;
                    }
                }
            }
            return manager;
        }
        public Manager Find(int id)
        {
            Manager m = null;
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, name, phonenumber, email, username from managers where id = @id";
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
                            m = new Manager(name, phonenumber, email, username, cId);
                        }
                    }
                }
            }
            return m;
        }

        public List<Manager> Find(ManagerColumn column, string search)
        {

            List<Manager> m = new List<Manager>();
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"select id, name, phonenumber, email, username from managers where {column.ToString()} = @search";
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
                            m.Add(new Manager(name, phonenumber, email, username, id));
                        }
                    }
                }
            }
            if (m.Count == 0)
            {
                return null;
            }
            return m;
        }

        public bool Update(Manager manager)
        {
            bool isSuccess = false;

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "update managers set name = @name, phonenumber = @phonenumber, email = @email where id=@id;";
                    cmd.Parameters.AddWithValue("@name", manager.Name);
                    cmd.Parameters.AddWithValue("@phonenumber", manager.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", manager.Email);
                    cmd.Parameters.AddWithValue("@id", manager.Id);
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
                    cmd.CommandText = "delete from managers where id = @id";
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



        public Manager Login(string username, string password)
        {
            string salt = "";
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select salt from managers where username = @username";
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
            // Return null if no user was foind
            if (salt == "")
            {
                return null;
            }
            string hashedPassword = Security.GenerateHashedPassword(password, salt);

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, name, phonenumber, email, username from managers where username = @username AND HashedPassword = @HashedPassword";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Manager() { Id = reader.GetInt32("id"), Name = reader.GetString("name"), PhoneNumber = reader.GetString("phonenumber"), Email = reader.GetString("email"), Username = reader.GetString("username") };
                        }
                    }
                }
            }
            //Incase password doesn't match
            return null;
        }
    }
}