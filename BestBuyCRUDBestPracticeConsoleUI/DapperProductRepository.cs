using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Text;


namespace BestBuyCRUDBestPracticeConsoleUI
{
    public class DapperProductRepository : IProductRepository
    {

        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Product GetProduct(int productID)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM Products WHERE ProductID = @productID;", new { ProductID = productID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }
        public void CreateProduct(string productName, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                "VALUES (@Name, @Price, @CategoryID);",
                new { Name = productName, Price = price, CategoryID = categoryID });
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE Products SET Name = @Name, Price = @Price  WHERE ProductID = @ProductID;",
                new { Name = product.Name, Price = product.Price, ProductID = product.ProductID });
        }
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM Products WHERE ProductID = @ID;", new { ID = productID });
            _connection.Execute("DELETE FROM Sales Where ProductID = @ID;", new { ID = productID });
            _connection.Execute("DELETE FROM Reviews WHERE ProductID = @ID;", new { ID = productID });
        }
        public Product ShowUpdatedProduct(int productID)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @ID;", new { ID = productID });
        }

        public Product ShowUpdateProduct(int productID)
        {
            throw new NotImplementedException();
        }
    }
}




        