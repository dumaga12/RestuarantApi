namespace RestaurantAPI.Models
{
    public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = new Customer();  
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();  
}

}

