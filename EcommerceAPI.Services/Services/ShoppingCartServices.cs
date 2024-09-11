﻿using EcommerceAPI.Data.UnitOfWork;
using EcommerceAPI.Domain;
using EcommerceAPI.DTOs;
using EcommerceAPI.Services.IServices;
using EcommerceAPI.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Services.Services
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private IUnitOfWork _unitOfWork;

        public ShoppingCartServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProductToShoppingCart(CartCreateDTO shoppingCartCreateRequestDTO)
        {
            // Check if the same product is found for same user
            var existingShoppingCart = await _unitOfWork.GenericRepository<ShoppingCart>().GetTAsync(predicate: c =>
            c.ApplicationUserId == shoppingCartCreateRequestDTO.UserId && c.ProductId == shoppingCartCreateRequestDTO.ProductId);

            // If cart is not found, create one new cart.
            if (existingShoppingCart == null)
            {
                var newlyCreatedShoppingCart = await _unitOfWork.GenericRepository<ShoppingCart>().AddAsync(new ShoppingCart
                {
                    ApplicationUserId = shoppingCartCreateRequestDTO.UserId,
                    Count = shoppingCartCreateRequestDTO.Count,
                    ProductId = shoppingCartCreateRequestDTO.ProductId,
                });
            }
            // If count is greater than zeros. Update the existing one,
            else if (shoppingCartCreateRequestDTO.Count > 0)
            {
                existingShoppingCart.Count = shoppingCartCreateRequestDTO.Count;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<CartResponseDTO> GetShoppingCartsByUserId(string userId)
        {
            var carts = await _unitOfWork.GenericRepository<ShoppingCart>().GetAllAsync(includeProperties: "Product", c => c.ApplicationUserId == userId);
            CartResponseDTO shoppingCartResponse = new CartResponseDTO { ShoppingCarts = carts };
            if (carts != null)
            {
                shoppingCartResponse.TotalCost = carts
                    .Where(cart => cart.Product != null)
                    .Sum(cart => (double)cart.Product!.Price * cart.Count);
            }
            return shoppingCartResponse;
        }

        public async Task RemoveProductsFromShoppingCart(List<string> productIDs, string userId)
        {
            if (productIDs == null || productIDs.Count == 0)
            {
                throw new ArgumentException("No product IDs provided.", nameof(productIDs));
            }

            var cartsToRemove = await _unitOfWork.GenericRepository<ShoppingCart>()
            .GetAllAsync(predicate: x => productIDs.Contains(x.ProductId) && x.ApplicationUserId == userId);

            if (!cartsToRemove.Any())
            {
                throw new NotFoundException("No matching products found in the shopping cart.");
            }

            await _unitOfWork.GenericRepository<ShoppingCart>().DeleteRangeAsync(cartsToRemove);
            await _unitOfWork.SaveAsync();
        }
    }
}
