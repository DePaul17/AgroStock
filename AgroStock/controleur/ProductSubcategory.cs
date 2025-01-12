using System;

namespace AgroStock
{
    public class ProductSubcategory
    {
        private int id;
        private string subcategoryName;
        private string description;
        private int categoryId;

        // Constructeur
        public ProductSubcategory(int id, string subcategoryName, string description, int categoryId)
        {
            this.id = id;
            this.subcategoryName = subcategoryName;
            this.description = description;
            this.categoryId = categoryId;
        }

        public ProductSubcategory(string subcategoryName, string description, int categoryId)
        {
            this.subcategoryName = subcategoryName;
            this.description = description;
            this.categoryId = categoryId;
        }

        public int Id { get => id; set => id = value; }

        public string SubcategoryName { get => subcategoryName; set => subcategoryName = value; }

        public string Description { get => description; set => description = value; }

        public int CategoryId { get => categoryId; set => categoryId = value; }
    }
}
