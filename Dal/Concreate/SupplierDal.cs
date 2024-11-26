using Dal.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dal.Concreate
{
    public class SupplierDal : ISupplierDal
    {
        private readonly SqlConnection _connectionString;

        public SupplierDal(string connectionString)
        {
            _connectionString = new SqlConnection(connectionString);
        }

        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, IsBlocked FROM Suppliers", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsBlocked = reader.GetBoolean(2)
                        });
                    }
                }
            }

            return suppliers;
        }

        public Supplier GetById(int supplierId)
        {
            Supplier supplier = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, IsBlocked FROM Suppliers WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", supplierId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supplier = new Supplier
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsBlocked = reader.GetBoolean(2)
                        };
                    }
                }
            }

            return supplier;
        }

        public void Add(Supplier supplier)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Suppliers (Name, IsBlocked) VALUES (@Name, @IsBlocked); SELECT SCOPE_IDENTITY();",
                    connection
                );
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@IsBlocked", supplier.IsBlocked);
                supplier.SupplierId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(Supplier supplier)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Suppliers SET Name = @Name, IsBlocked = @IsBlocked WHERE Id = @Id",
                    connection
                );
                cmd.Parameters.AddWithValue("@Id", supplier.SupplierId);
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@IsBlocked", supplier.IsBlocked);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int supplierId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Suppliers WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", supplierId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Supplier> Search(string query)
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT Id, Name, IsBlocked FROM Suppliers WHERE Name LIKE @Query", connection
                );
                cmd.Parameters.AddWithValue("@Query", "%" + query + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsBlocked = reader.GetBoolean(2)
                        });
                    }
                }
            }

            return suppliers;
        }

        public List<Supplier> GetBlockedSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, IsBlocked FROM Suppliers WHERE IsBlocked = 1", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsBlocked = reader.GetBoolean(2)
                        });
                    }
                }
            }

            return suppliers;
        }

        public List<Supplier> GetAllSorted(string sortBy, bool ascending)
        {
            List<Supplier> suppliers = new List<Supplier>();
            string sortOrder = ascending ? "ASC" : "DESC";
            string query = $"SELECT Id, Name, IsBlocked FROM Suppliers ORDER BY {sortBy} {sortOrder}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsBlocked = reader.GetBoolean(2)
                        });
                    }
                }
            }

            return suppliers;
        }
    }
}
