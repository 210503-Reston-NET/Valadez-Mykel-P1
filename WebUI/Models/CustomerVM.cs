using System.ComponentModel.DataAnnotations;
using Models;

namespace WebUI.Models
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$",
        ErrorMessage = "Please input a valid name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress( 
        ErrorMessage = "Please input a valid email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string Password { get; set; }

        public CustomerVM()
        {

        }
        public CustomerVM(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
        public CustomerVM(int id, string name, string email, string password) : this(name, email, password)
        {
            this.CustomerId = id;
        }

        public CustomerVM(int id)
        {
            this.CustomerId = id;
        }

        public CustomerVM(Customer Cust)
        {
            this.CustomerId = Cust.CustomerId;
            this.Name = Cust.Name;
            this.Email = Cust.Email;
            this.Password = Cust.Password;

        }


    }
}
