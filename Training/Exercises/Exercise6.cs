using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Products;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Create a Cart Exercise
    /// </summary>
    public class Exercise6 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise6(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            CreateACart();
        }

        private void CreateACart()
        {
            CartDraft cartDraft = this.GetCartDraft();
            Cart cart = _commercetoolsClient.ExecuteAsync(new CreateCommand<Cart>(cartDraft)).Result;
            if (cart != null)
            {
                Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");
            }
        }
        public CartDraft GetCartDraft()
        {
            string customerId = "e65458d0-b350-4c52-984b-bbf6db188748";
            
            CartDraft cartDraft = new CartDraft();
            cartDraft.CustomerId = customerId;
            cartDraft.Currency = "EUR";
            cartDraft.ShippingAddress = new Address()
            {
                Country = "DE"
            };
            cartDraft.DeleteDaysAfterLastModification = 30;
            return cartDraft;
        }
    }
}