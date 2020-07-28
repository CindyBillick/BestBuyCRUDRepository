using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace BestBuyCRUDBestPracticeConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            //MySqlConnection Object (we can access this class's members using --> conn)
            IDbConnection conn = new MySqlConnection(connString);
            //DapperDepartmentsRepository Object (we can access this class's members using --> repo)
            var repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("The current departments are:");
            //depos = the result from GetAllDepartments()
            var depos = repo.GetAllDepartments();

            PrintDepartments(depos);//Print them to the console using the PrintDepartments() (see below)

            Console.WriteLine();

            Console.WriteLine("Do you want to add a department?");
            var userResponse = Console.ReadLine(); //User's respnse variable declaration

            if (userResponse.ToLower() == "yes") //Case insensitive validation
            {
                Console.WriteLine("Enter the department's name");
                userResponse = Console.ReadLine();

                Console.WriteLine();

                repo.InsertDepartment(userResponse); //Here is where we insert a new department using --> userResponse
            }
            PrintDepartments(repo.GetAllDepartments());//Print out the Departments

            Console.WriteLine();//Add some blank spaces for readability
            Console.WriteLine();

            Console.WriteLine("The current products are: ");

            //DapperProductRepository Object (now we can access this class's members using --> prod)
            var prod = new DapperProductRepository(conn);

            var prods = prod.GetAllProducts();//prods = an IEnumerable<Product> "list"

            PrintProducts(prods);//Print out the products using the PrintProducts() (see below)

            Console.WriteLine();

            Console.WriteLine("Would you like to add a product?");
            userResponse = Console.ReadLine();//We already declared a user response variable so now we just reset it's value

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("Enter the product's name");
                var productName = Console.ReadLine();

                Console.WriteLine("Enter the product's price");
                var productPrice = double.Parse(Console.ReadLine());//We must parse our Console.ReadLine() for a double because price is of type double 
                                                                    //and Console.ReadLine() gives us a string

                //For the sake of simplicity we will just list out the category numbers with a Console.WriteLine() --> cw + tab + tab
                Console.WriteLine("Enter the product's categoryID");
                Console.WriteLine();
                Console.WriteLine("1--> Computers");
                Console.WriteLine("2--> Applinces");
                Console.WriteLine("3--> Phones");
                Console.WriteLine("4--> Audio");
                Console.WriteLine("5--> Home Theater");
                Console.WriteLine("6--> Printers");
                Console.WriteLine("7--> Music");
                Console.WriteLine("8--> Games");
                Console.WriteLine("9--> Services");
                Console.WriteLine("10--> Other");
                Console.WriteLine();
                Console.WriteLine("Enter a number from above: ");
                var productCategoryId = int.Parse(Console.ReadLine());//We must parse our Console.ReadLine for an integer bc CategoryID is an integer

                prod.CreateProduct(productName, productPrice, productCategoryId);//Here our INSERT INTO method is called and it needs 3 arguments matching our 3 parameters types
            }

            PrintProducts(prod.GetAllProducts());//now we print the products again and we see our new product we just created at the bottom of the list

            Console.WriteLine();

            Console.WriteLine("Would you like to update any products?");
            userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("Please enter the products ID");
                
                var productID = int.Parse(Console.ReadLine());//Parse for an int

                var productToUpdate = prod.GetProduct(productID);//Here we need a Product so we call the GetProduct() ***This is a bonus and not required*** { see GetProduct() }

                //We name this product --> productToUpdate

                Console.WriteLine("Please enter the products name");
                productToUpdate.Name = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine("Please enter the products price");
                productToUpdate.Price = double.Parse(Console.ReadLine());//Parse for a double again

                Console.WriteLine();
                Console.WriteLine();

                prod.UpdateProduct(productToUpdate);//Here we pass in the Product we just got back as an argument for the UpdateProduct() 
                                                    //The product's name we pass in as an argument is --> productToUpdate

                ShowUpdatedProduct(productToUpdate);//We just want to show the product we just updated not the entire list so we use the ShowUpdatedProduct() (see below)
            }
            Console.WriteLine();

            Console.WriteLine("Would you like to delete any products?");
            userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the products ID");
                var productID = int.Parse(Console.ReadLine());//Parse for an int

                Console.WriteLine();

                prod.DeleteProduct(productID);//For the DeleteProduct() we only need to pass in the products ID as an argument; therefore, we pass in --> productID
            }
            PrintProducts(prod.GetAllProducts());//Finally we print all of the products again to confirm that the product was indeed deleted
        }


        private static void PrintDepartments(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentId}    |     Name: {depo.Name}");
            }
        }

        private static void PrintProducts(IEnumerable<Product> prods)
        {
            foreach (var prod in prods)
            {
                Console.WriteLine();
                Console.WriteLine($"ID: {prod.ProductID} | Name: {prod.Name} | Price: {prod.Price} | " +
                                  $"CategoryID: {prod.CategoryID} | OnSale {prod.OnSale} | StockLevel: {prod.StockLevel}");
                Console.WriteLine();
            }
        }

        private static void ShowUpdatedProduct(Product product)
        {
            Console.WriteLine($"ID: {product.ProductID} | Name: {product.Name} | Price: {product.Price}");
        }
    }

}           