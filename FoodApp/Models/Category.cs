using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public string Image { get; set; }

        [OneToMany]
        public List<Product> Products { get; set; }

    }
}
