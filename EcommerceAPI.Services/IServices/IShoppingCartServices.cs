﻿using EcommerceAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Services.IServices
{
    /// <summary>
    /// Interface for managing shopping carts.
    /// </summary>
    public interface IShoppingCartServices
    {
        /// <summary>
        /// Retrieves the current available shopping carts for a specific user.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, containing a 
        /// <see cref="CartResponseDTO"/> with the details of the shopping cart.
        /// </returns>
        public Task<CartResponseDTO> GetShoppingCartsByUserId(string userId);

        /// <summary>
        /// Adds a new product to the shopping cart or updates the existing shopping cart.
        /// Each shopping cart contains a list of products with their quantities.
        /// </summary>
        /// <param name="shoppingCartCreateRequestDTO">
        /// A <see cref="CartCreateDTO"/> containing the details of the products 
        /// to be added or updated in the shopping cart.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public Task AddProductToShoppingCart(CartCreateDTO shoppingCartCreateRequestDTO);

        /// <summary>
        /// Removes all products or specific products from the shopping cart.
        /// </summary>
        /// <param name="productId">
        /// A list of product IDs representing the products to be removed from the shopping cart.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public Task RemoveProductsFromShoppingCart(List<string> productIDs, string userId);
    }
}
