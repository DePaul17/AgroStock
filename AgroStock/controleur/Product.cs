using System;

namespace AgroStock
{
    public class Product
    {
        private int id;
        private string productName;
        private DateTime productionDate;
        private float totalCarbonFootprint;
        private string resourcesUsed;
        private decimal price;
        private int subcategoryId;

        // Constructeur
        public Product(int id, string productName, DateTime productionDate, float totalCarbonFootprint, string resourcesUsed, decimal price, int subcategoryId)
        {
            this.id = id;
            this.productName = productName;
            this.productionDate = productionDate;
            this.totalCarbonFootprint = totalCarbonFootprint;
            this.resourcesUsed = resourcesUsed;
            this.price = price;
            this.subcategoryId = subcategoryId;
        }

        public Product(string productName, DateTime productionDate, float totalCarbonFootprint, string resourcesUsed, decimal price, int subcategoryId)
        {
            this.productName = productName;
            this.productionDate = productionDate;
            this.totalCarbonFootprint = totalCarbonFootprint;
            this.resourcesUsed = resourcesUsed;
            this.price = price;
            this.subcategoryId = subcategoryId;
        }

        public int Id { get => id; set => id = value; }

        public string ProductName { get => productName; set => productName = value; }

        public DateTime ProductionDate { get => productionDate; set => productionDate = value; }

        public float TotalCarbonFootprint { get => totalCarbonFootprint; set => totalCarbonFootprint = value; }

        public string ResourcesUsed { get => resourcesUsed; set => resourcesUsed = value; }

        public decimal Price { get => price; set => price = value; }

        public int SubcategoryId { get => subcategoryId; set => subcategoryId = value; }
    }
}
