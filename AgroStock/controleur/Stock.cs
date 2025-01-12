using System;

namespace AgroStock
{
    public class Stock
    {
        private int id;
        private int productId;
        private DateTime entryDate;
        private DateTime? exitDate;
        private int storageLocationId;
        private int quantity;

        // Constructeur
        public Stock(int id, int productId, DateTime entryDate, DateTime? exitDate, int storageLocationId, int quantity)
        {
            this.id = id;
            this.productId = productId;
            this.entryDate = entryDate;
            this.exitDate = exitDate;
            this.storageLocationId = storageLocationId;
            this.quantity = quantity;
        }

        public Stock(int productId, DateTime entryDate, DateTime? exitDate, int storageLocationId, int quantity)
        {
            this.productId = productId;
            this.entryDate = entryDate;
            this.exitDate = exitDate;
            this.storageLocationId = storageLocationId;
            this.quantity = quantity;
        }

        public int Id { get => id; set => id = value; }

        public int ProductId { get => productId; set => productId = value; }

        public DateTime EntryDate { get => entryDate; set => entryDate = value; }

        public DateTime? ExitDate { get => exitDate; set => exitDate = value; }

        public int StorageLocationId { get => storageLocationId; set => storageLocationId = value; }

        public int Quantity { get => quantity; set => quantity = value; }
    }
}
