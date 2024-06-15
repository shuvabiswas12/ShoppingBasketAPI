﻿using ShoppingBasketAPI.Domain;
using ShoppingBasketAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.Services.IServices
{
    public interface ICategoryServices
    {
        public Task<CategoryResponseDTO> GetAllCategories();
        public Task<Category> GetCategoryById(object id);
        public Task DeleteCategory(object id);
        public Task<Category> UpdateCategory(object id, string categoryName);
    }
}
