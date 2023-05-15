using System;
using System.Collections.Generic;

namespace LearnSchool;

public partial class Parameter
{
    public int ParameterId { get; set; }

    public string ParameterName { get; set; } = null!;

    public int UnitTypeId { get; set; }

    public virtual ICollection<Productsparameter> Productsparameters { get; set; } = new List<Productsparameter>();

    public virtual UnitType UnitType { get; set; } = null!;
}
