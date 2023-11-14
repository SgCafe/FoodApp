using FoodApp.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodApp.Services
{
    public class ProductService : IProductService
    {
        private SQLiteConnection _dbConnection;

        public ProductService()
        {
            SetupDb();
        }

        private void SetupDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductDB.db3");

                _dbConnection = new SQLiteConnection(dbPath);
                _dbConnection.CreateTable<Product>();
                _dbConnection.CreateTable<Category>();
            }
        }


        public void AddProduct(Product product)
        {
            _dbConnection.Insert(product);
        }

        public void UpdateItems(Product product)
        {
            _dbConnection.UpdateWithChildren(product);
        }

        public void AddCategory(Category category)
        {
            _dbConnection.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            _dbConnection.Update(category);
        }

        public List<Product> GetProducts()
        {
            var products = _dbConnection.GetAllWithChildren<Product>();
            return products;
        }

        public List<Category> GetCategories()
        {
            var categories = _dbConnection.Table<Category>().ToList();
            return categories;
        }

        public bool ItemExists(string itemName, string category, double? price)
        {
            var itemExists = _dbConnection.Table<Product>().FirstOrDefault(i => i.Name == itemName && i.Category.NameCategory == category && i.Price == price);
            return itemExists != null;
        }

        public bool CategoryExists(string nameCategory)
        {
            var categoryExists = _dbConnection.Table<Category>().FirstOrDefault(c => c.NameCategory == nameCategory);
            return categoryExists != null;
        }
    }
}
