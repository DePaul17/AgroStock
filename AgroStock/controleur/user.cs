using System;

namespace AgroStock
{
    public class User
    {
        private int id;
        private string firstName;
        private string lastName;
        private string address;
        private string phoneNumber;
        private string email;
        private string password;
        private string role;
        private string qualification;

        // Constructeur
        public User(int id, string firstName, string lastName, string address, string phoneNumber, string email, string password, string role, string qualification)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
            this.role = role;
            this.qualification = qualification;
        }

        public User(string firstName, string lastName, string address, string phoneNumber, string email, string password, string role, string qualification)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.password = password;
            this.role = role;
            this.qualification = qualification;
        }

        public int Id { get => id; set => id = value; }

        public string FirstName { get => firstName; set => firstName = value; }

        public string LastName { get => lastName; set => lastName = value; }

        public string Address { get => address; set => address = value; }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public string Email { get => email; set => email = value; }

        public string Password { get => password; set => password = value; }

        public string Role { get => role; set => role = value; }

        public string Qualification { get => qualification; set => qualification = value; }
    }
}
