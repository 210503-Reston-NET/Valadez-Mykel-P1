namespace Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Product(){

        }
        public Product(string name, double price){
            this.Name = name;
            this.Price = price;
        }
        public Product(int id, string name, double price): this(name, price){
            this.ProductId = id;
        }
    }
}
