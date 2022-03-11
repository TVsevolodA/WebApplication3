using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public int Customer_Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
    }
}