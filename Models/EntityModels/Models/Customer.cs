using System;
using System.Collections.Generic;

namespace EntityModels.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
