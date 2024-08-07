﻿using ShoppingBasketAPI.Domain;
using ShoppingBasketAPI.DTOs;
using ShoppingBasketAPI.DTOs.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.Services.IServices
{
    public interface IProductServices
    {
        public Task<GenericResponseDTO<ProductDTO>> GetAllProduct();
        public Task<Product> GetProductById(object id);
        public Task DeleteProduct(object id);
        public Task<Product> UpdateProduct(Object id, ProductUpdateDTO productDto);
        public Task<Product> CreateProduct(ProductCreateDTO productDto);
    }
}
