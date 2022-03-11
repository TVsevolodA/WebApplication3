using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        public string SNP { get; set; }         // ФИО
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}