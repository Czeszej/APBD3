using System;
using System.Net.Mail;

namespace LegacyApp
{
    public class UserService 
    {
               
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if(!CheckInfo(firstName, lastName, email) && !CheckAge(dateOfBirth))
                return false;
           

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = CreateUser (firstName, lastName, email, dateOfBirth);

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        public bool CheckInfo(string firstName, string lastName, string email)
        {
            return (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) && !email.Contains("@") && !email.Contains("."));
        }

        public bool CheckAge(DateTime dateOfBirth) {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age >= 21;
            }

        public User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth) {

            return new User (firstName, lastName, email, dateOfBirth);
            
        }

    }
    

}

