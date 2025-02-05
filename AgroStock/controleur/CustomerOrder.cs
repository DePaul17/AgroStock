using System;

namespace AgroStock
{
    public class CustomerOrder
    {
        private int orderId;
        private int customerId;
        private DateTime orderDate;
        private string status;
        private decimal totalAmount;

        // Constructeur
        public CustomerOrder(int orderId, int customerId, DateTime orderDate, string status, decimal totalAmount)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.status = status;
            this.totalAmount = totalAmount;
        }

        public CustomerOrder(int customerId, DateTime orderDate, string status, decimal totalAmount)
        {
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.status = status;
            this.totalAmount = totalAmount;
        }

        public int OrderId { get => orderId; set => orderId = value; }

        public int CustomerId { get => customerId; set => customerId = value; }

        public DateTime OrderDate { get => orderDate; set => orderDate = value; }

        public string Status { get => status; set => status = value; }

        public decimal TotalAmount { get => totalAmount; set => totalAmount = value; }
    }
}
