using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        List<Category> GetCategories();
        void AddProduct(Product product);
        void UpdateItems(Product product);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        bool ItemExists(string itemName, string category, double? price);
        bool CategoryExists(string nameCategory);
    }
}
