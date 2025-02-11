using System;

namespace AgroStock
{
    public class ProductSubcategory
    {
        // Propriétés automatiques
        public int Id { get; set; }                  // ID de la sous-catégorie
        public string SubcategoryName { get; set; }  // Nom de la sous-catégorie
        public string Description { get; set; }      // Description de la sous-catégorie
        public int CategoryId { get; set; }          // ID de la catégorie parente
        public string ParentCategory { get; set; }   // Nom de la catégorie parente

        // Constructeur pour l'insertion (sans ID)
        public ProductSubcategory(string subcategoryName, string description, int categoryId, string parentCategory)
        {
            SubcategoryName = subcategoryName;
            Description = description;
            CategoryId = categoryId;
            ParentCategory = parentCategory;
        }

        // Constructeur pour la modification (avec ID)
        public ProductSubcategory(int id, string subcategoryName, string description, int categoryId, string parentCategory)
        {
            Id = id;
            SubcategoryName = subcategoryName;
            Description = description;
            CategoryId = categoryId;
            ParentCategory = parentCategory;
        }
    }
}