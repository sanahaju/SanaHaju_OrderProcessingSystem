using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingSystem.BusinessLayer.DTO
{
    public class CreateOrderRequestDTO
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
