using EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3EntityFramework.Dtos
{
    public class CustomerDto
    {
        public string? CustomerName;

        public CustomerDto(Customer cust)
        {
            CustomerName = cust.FirstName + " " + cust.LastName;
        }
    }
}
