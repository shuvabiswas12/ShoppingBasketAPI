﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.DTOs
{
    public class ProductResponseDTO
    {
        public int ProductsCount { get; set; } = 0;
        public IEnumerable<ProductDTO> Products { get; set; } = null!;
    }
}
