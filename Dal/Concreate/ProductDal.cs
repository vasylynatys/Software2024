using Dal.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dal.Concreate
{
    public class ProductDal : IProductDal
    {
        private readonly SqlConnection _connectionString;

        public ProductDal(string connectionString)
        {
            _connectionString = new SqlConnection(connectionString);
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(MapReaderToProduct(reader));
                }
            }
            return products;
        }

        public Product GetById(int productId)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product = MapReaderToProduct(reader);
                }
            }
            return product;
        }

        public void Add(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Products (Name, Price, SupplierId) VALUES (@Name, @Price, @SupplierId)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@SupplierId", product.SupplierId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Products SET Name = @Name, Price = @Price, SupplierId = @SupplierId WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@SupplierId", product.SupplierId);
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetBySupplierId(int supplierId)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Products WHERE SupplierId = @SupplierId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(MapReaderToProduct(reader));
                }
            }
            return products;
        }

        public List<Product> GetAllSorted(string sortBy, bool ascending)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM Products ORDER BY {sortBy} {(ascending ? "ASC" : "DESC")}";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(MapReaderToProduct(reader));
                }
            }
            return products;
        }

        private Product MapReaderToProduct(SqlDataReader reader)
        {
            return new Product
            {
                ProductId = (int)reader["ProductId"],
                Name = reader["Name"].ToString(),
                Price = (decimal)reader["Price"],
                SupplierId = (int)reader["SupplierId"]
            };
        }
    }
}
