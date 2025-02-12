using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501.Dtos
{
    public class ResultProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductStock { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
