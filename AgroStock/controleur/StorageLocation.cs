using System;

namespace AgroStock
{
    public class StorageLocation
    {
        private int id;
        private string locationName;
        private string storageType;
        private int maxCapacity;

        // Constructeur
        public StorageLocation(int id, string locationName, string storageType, int maxCapacity)
        {
            this.id = id;
            this.locationName = locationName;
            this.storageType = storageType;
            this.maxCapacity = maxCapacity;
        }

        public StorageLocation(string locationName, string storageType, int maxCapacity)
        {
            this.locationName = locationName;
            this.storageType = storageType;
            this.maxCapacity = maxCapacity;
        }

        public int Id { get => id; set => id = value; }

        public string LocationName { get => locationName; set => locationName = value; }

        public string StorageType { get => storageType; set => storageType = value; }

        public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
    }
}
