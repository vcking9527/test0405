using Nest;

namespace ELK.Demo.WebApi.Models
{
    public class Account
    {
        [PropertyName("account_number")]
        public int AccountNumber { get; set; }

        [PropertyName("balance")]
        public int Balance { get; set; }

        [PropertyName("firstname")]
        public string FirstName { get; set; }

        [PropertyName("lastname")]
        public string LastName { get; set; }

        [PropertyName("age")]
        public int Age { get; set; }

        [PropertyName("gender")]
        public string Gender { get; set; }

        [PropertyName("address")]
        public string Address { get; set; }

        [PropertyName("employer")]
        public string Employer { get; set; }

        [PropertyName("email")]
        public string Email { get; set; }

        [PropertyName("city")]
        public string City { get; set; }

        [PropertyName("state")]
        public string State { get; set; }
    }
}