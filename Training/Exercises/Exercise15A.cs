using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;

namespace Training
{
    /// <summary>
    /// Create a Cart Exercise
    /// </summary>
    public class Exercise15A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise15A(IClient commercetoolsClient)
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
            CartDraft cartDraft = new CartDraft();
            cartDraft.CustomerId = Settings.CUSTOMERID;
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
