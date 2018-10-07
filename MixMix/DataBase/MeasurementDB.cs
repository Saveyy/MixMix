using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class MeasurementDB
    {
        public Measurement Insert(Measurement measurement)
        {
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "insert into Measurements (type, proportion) output inserted.id values(@type, @proportion);";
                    cmd.Parameters.AddWithValue("@type", measurement.Type);
                    cmd.Parameters.AddWithValue("@proportion", measurement.Proportion);
                    measurement.Id = (int)cmd.ExecuteScalar();
                }
                if(measurement.Id == default(int))
                {
                    return null;
                }
            }
            return measurement;
        }


        public Measurement Find(int id)
        {
            /*
             * id int primary key identity(1,1),
            type varchar(100) not null unique,
            proportion float
            */
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select type, proportion from Measurements where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader.GetString("type");
                            try
                            {
                                float proportion = reader.GetFloat("proportion");
                                return new Measurement() { Id = id, Type = type, Proportion = proportion };
                            }
                            catch(Exception e)
                            {
                                return new Measurement() { Id = id, Type = type};
                            }
                        }
                    }
                }
            }
            return null;
        }

        public List<Measurement> FindAll()
        {
            List<Measurement> measurements = new List<Measurement>();

            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT id, type from Measurements";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Measurement measurement = new Measurement
                            {
                                Id = reader.GetInt32("id"),
                                Type = reader.GetString("type")
                            };
                            measurements.Add(measurement);
                        }
                    }
                }
            }
            return measurements;
        }

        public Measurement Find(string type)
        {
            /*
             * id int primary key identity(1,1),
            type varchar(100) not null unique,
            proportion float
            */
            using (SqlConnection connection = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select id, proportion from Measurements where type = @type";
                    cmd.Parameters.AddWithValue("@type", type);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            float proportion = reader.GetFloat("proportion");
                            return new Measurement() { Id = id, Type = type, Proportion = proportion };
                        }
                    }
                }
            }
            return null;
        }
        //public bool Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}


        //public Measurement Insert(Measurement obj, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Update(Measurement obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
