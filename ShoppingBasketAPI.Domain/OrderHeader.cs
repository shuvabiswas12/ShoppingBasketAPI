﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShoppingBasketAPI.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.Domain
{
    public class OrderHeader
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey("ApplicationUserId"), ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal OrderTotal { get; set; }

        [Required]
        public string Status { get; set; } = OrderStatus.StatusPending;

        public string? PaymentStatus { get; set; }

        [Required]
        public string ShippingAddress { get; set; } = null!;

        [Required] public string FullName { get; set; } = null!;

        [Required] public string Phone { get; set; } = null!;

        [Required] public string Email { get; set; } = null!;

        [Required] public string City { get; set; } = null!;

        [Required] public string Country { get; set; } = null!;

        [Required] public string State { get; set; } = null!;

        [Required] public string PostCode { get; set; } = null!;


        public string? TrackingNumber { get; set; }

        // Navigation property
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }

}
