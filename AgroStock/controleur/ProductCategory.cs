using System;

namespace AgroStock
{
    public class ProductCategory
    {
        private int id;
        private string categoryName;
        private string description;

        // Constructeur
        public ProductCategory(int id, string categoryName, string description)
        {
            this.id = id;
            this.categoryName = categoryName;
            this.description = description;
        }

        public ProductCategory(string categoryName, string description)
        {
            this.categoryName = categoryName;
            this.description = description;
        }

        public int Id { get => id; set => id = value; }

        public string CategoryName { get => categoryName; set => categoryName = value; }

        public string Description { get => description; set => description = value; }
    }
}
