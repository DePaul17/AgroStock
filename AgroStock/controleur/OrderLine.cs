using System;

namespace AgroStock
{
    public class OrderLine
    {
        private int id;
        private int orderId;
        private int productId;
        private int quantity;
        private decimal price;

        // Constructeur
        public OrderLine(int id, int orderId, int productId, int quantity, decimal price)
        {
            this.id = id;
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
            this.price = price;
        }

        public OrderLine(int orderId, int productId, int quantity, decimal price)
        {
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
            this.price = price;
        }

        public int Id { get => id; set => id = value; }

        public int OrderId { get => orderId; set => orderId = value; }

        public int ProductId { get => productId; set => productId = value; }

        public int Quantity { get => quantity; set => quantity = value; }

        public decimal Price { get => price; set => price = value; }
    }
}
