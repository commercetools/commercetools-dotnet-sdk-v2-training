using System;
using System.Threading.Tasks;
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
        public async Task ExecuteAsync()
        {
            try
            {
                CartDraft cartDraft = this.GetCartDraft();
                Cart cart = await _commercetoolsClient.ExecuteAsync(new CreateCommand<Cart>(cartDraft));
                if (cart != null)
                {
                    Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private CartDraft GetCartDraft()
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
