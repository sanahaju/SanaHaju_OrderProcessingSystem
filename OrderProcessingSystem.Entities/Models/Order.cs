using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProcessingSystem.Entities.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }  

        public decimal TotalPrice
        {
            get
            {
                return Products.Sum(product => product.Price);
            }
        }
    }

    public enum OrderStatus
    {
        Pending,
        Fulfilled,
        Canceled
    }
}