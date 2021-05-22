namespace Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Customer(){

        }
        public Customer(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
        public Customer(int id, string name, string email, string password): this(name, email, password){
            this.CustomerId = id;
        }
    }
}