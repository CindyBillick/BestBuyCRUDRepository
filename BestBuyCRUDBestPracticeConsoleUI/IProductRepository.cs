using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProduct(int productID);
        public void CreateProduct(string name, double price, int categoryID);
        public void UpdateProduct(Product product);//Bonus
        public void DeleteProduct(int productID); //Bonus #2
        public Product ShowUpdateProduct(int productID);//Bonus #3
    }
}
