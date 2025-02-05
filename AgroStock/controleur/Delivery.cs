using System;

namespace AgroStock
{
    public class Delivery
    {
        private int id;
        private int orderId;
        private DateTime deliveryDate;
        private string deliveryAddress;
        private string deliveryStatus;

        // Constructeur
        public Delivery(int id, int orderId, DateTime deliveryDate, string deliveryAddress, string deliveryStatus)
        {
            this.id = id;
            this.orderId = orderId;
            this.deliveryDate = deliveryDate;
            this.deliveryAddress = deliveryAddress;
            this.deliveryStatus = deliveryStatus;
        }

        public Delivery(int orderId, DateTime deliveryDate, string deliveryAddress, string deliveryStatus)
        {
            this.orderId = orderId;
            this.deliveryDate = deliveryDate;
            this.deliveryAddress = deliveryAddress;
            this.deliveryStatus = deliveryStatus;
        }

        public int Id { get => id; set => id = value; }

        public int OrderId { get => orderId; set => orderId = value; }

        public DateTime DeliveryDate { get => deliveryDate; set => deliveryDate = value; }

        public string DeliveryAddress { get => deliveryAddress; set => deliveryAddress = value; }

        public string DeliveryStatus { get => deliveryStatus; set => deliveryStatus = value; }
    }
}
