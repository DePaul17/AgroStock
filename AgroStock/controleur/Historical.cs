using System;

namespace AgroStock
{
    public class Historical
    {
        private int id;
        private int userId;
        private string action;
        private DateTime actionTimestamp;

        // Constructeur
        public Historical(int id, int userId, string action, DateTime actionTimestamp)
        {
            this.id = id;
            this.userId = userId;
            this.action = action;
            this.actionTimestamp = actionTimestamp;
        }

        public Historical(int userId, string action)
        {
            this.userId = userId;
            this.action = action;
            this.actionTimestamp = DateTime.Now; // On attribuera le timestamp actuel, si l'ID est généré automatiquement
        }

        public int Id { get => id; set => id = value; }

        public int UserId { get => userId; set => userId = value; }

        public string Action { get => action; set => action = value; }

        public DateTime ActionTimestamp { get => actionTimestamp; set => actionTimestamp = value; }
    }
}
