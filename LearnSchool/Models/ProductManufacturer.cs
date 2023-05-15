﻿using System;
using System.Collections.Generic;

namespace LearnSchool;

public partial class ProductManufacturer
{
    public int ProductManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfBegging { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
