using System;

namespace LegacyApp
{
    public class User : Client
    {
        public object Client { get; internal set; } // dodać dziedziczenie po klasie Client?
        //public User() : base() { }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public User(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = email;
            this.DateOfBirth = dateOfBirth;
        }
    }
}