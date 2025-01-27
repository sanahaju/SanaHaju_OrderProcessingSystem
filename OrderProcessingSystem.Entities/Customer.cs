using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProcessingSystem.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
