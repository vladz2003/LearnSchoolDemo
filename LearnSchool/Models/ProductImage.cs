﻿using System;
using System.Collections.Generic;

namespace LearnSchool;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public int ProductId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
