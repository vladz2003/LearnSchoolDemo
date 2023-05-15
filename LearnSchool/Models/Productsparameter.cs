using System;
using System.Collections.Generic;

namespace LearnSchool;

public partial class Productsparameter
{
    public int ProductParameterId { get; set; }

    public int ProductId { get; set; }

    public int ParameterId { get; set; }

    public string Value { get; set; } = null!;

    public virtual Parameter Parameter { get; set; } = null!;

    public virtual Product ProductParameter { get; set; } = null!;
}
