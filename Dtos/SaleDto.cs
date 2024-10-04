using EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3EntityFramework.Dtos
{
    public class SaleDto
    {
        public string ProductName { get; set;  }

        public string CustomerName { get; set; }

        public decimal? Price { get; set; }

        public SaleDto(Sale sale) 
        {
            ProductName = sale.Product?.Name;

            CustomerName = $"{sale.Customer?.FirstName} {sale.Customer?.LastName}";

            Price = sale.Product.Price;
        }
    }
}
