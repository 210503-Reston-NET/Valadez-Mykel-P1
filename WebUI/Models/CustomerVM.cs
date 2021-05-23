using System;
namespace WebUI.Models
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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

    }
}
