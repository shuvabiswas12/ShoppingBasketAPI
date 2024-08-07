﻿using ShoppingBasketAPI.Services.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketAPI.Services.Services
{
    public class PaymentServices : IPaymentServices
    {
        public async Task<string> CreatePaymentIntentAsync(long amount)
        {
            if (amount == 0) throw new ArgumentNullException("Amount argument should not be empty.");

            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }
    }
}
