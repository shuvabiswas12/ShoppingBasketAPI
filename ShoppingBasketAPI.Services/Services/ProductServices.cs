﻿using AutoMapper;
using ShoppingBasketAPI.Data.UnitOfWork;
using ShoppingBasketAPI.Domain;
using ShoppingBasketAPI.DTOs;
using ShoppingBasketAPI.DTOs.GenericResponse;
using ShoppingBasketAPI.Services.IServices;
using ShoppingBasketAPI.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.Services.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Product> CreateProduct(ProductCreateDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name.Trim(),
                Description = productDto.Description.Trim(),
                Price = productDto.Price,
                Images = productDto.ImageUrls.Select(url => new Image { ImageUrl = url }).ToList(),
            };

            var createdProduct = await _unitOfWork.GenericRepository<Product>().AddAsync(product);
            await _unitOfWork.SaveAsync();
            return createdProduct;
        }

        public async Task DeleteProduct(object id)
        {
            var productToDelete = await _unitOfWork.GenericRepository<Product>().GetTAsync(x => x.Id == id.ToString(), includeProperties: "Images");
            if (productToDelete == null)
            {
                throw new NotFoundException("Product not found.");
            }
            await _unitOfWork.GenericRepository<Product>().DeleteAsync(productToDelete);
            await _unitOfWork.SaveAsync();
        }

        public async Task<GenericResponseDTO<ProductDTO>> GetAllProduct()
        {
            var productsResult = await _unitOfWork.GenericRepository<Product>().GetAllAsync(includeProperties: "Images, Discount, ProductCategory, FeaturedProduct");
            var productsResponse = new GenericResponseDTO<ProductDTO>
            {
                Data = _mapper.Map<IEnumerable<ProductDTO>>(productsResult),
                Count = productsResult.Count()
            };
            return productsResponse;
        }

        public async Task<Product> GetProductById(object id)
        {
            var productResult = await _unitOfWork.GenericRepository<Product>().GetTAsync(x => x.Id == id.ToString(), includeProperties: "Images");
            if (productResult == null)
                throw new NotFoundException(message: "Product not found.");
            return productResult;
        }

        public async Task<Product> UpdateProduct(Object id, ProductUpdateDTO productDto)
        {
            var productToUpdate = await _unitOfWork.GenericRepository<Product>().GetTAsync(x => x.Id == id.ToString(), includeProperties: "Images");
            if (productToUpdate == null)
            {
                throw new NotFoundException(message: "Product not found.");
            }
            if (!string.IsNullOrEmpty(productDto!.Name)) productToUpdate.Name = productDto!.Name;
            if (!string.IsNullOrEmpty(productDto!.Description)) productToUpdate.Description = productDto!.Description;
            if (productDto!.Price > 0) productToUpdate.Price = (decimal)productDto!.Price;
            if (productDto!.ImageUrls!.Any()) productToUpdate.Images = productDto!.ImageUrls!.Select(u => new Image { ImageUrl = u, ProductId = productToUpdate.Id }).ToList();

            await _unitOfWork.SaveAsync();
            return productToUpdate;
        }
    }
}
