using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;

namespace Training
{
    /// <summary>
    /// Add Product to Cart
    /// </summary>
    public class Exercise15B : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise15B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //Get the active Cart By Customer Id
            Cart cart =
                await _commercetoolsClient.ExecuteAsync(new GetCartByCustomerIdCommand(new Guid(Settings.CUSTOMERID)));

            AddLineItemUpdateAction addLineItemUpdateAction = new AddLineItemUpdateAction()
            {
                Sku = Settings.PRODUCTVARIANTSKU,
                Quantity = 6
            };

            List<UpdateAction<Cart>> updateActions = new List<UpdateAction<Cart>>
            {
                addLineItemUpdateAction
            };

            Cart retrievedCart = await _commercetoolsClient
                .ExecuteAsync(new UpdateByIdCommand<Cart>(cart, updateActions));

            foreach (var lineItem in retrievedCart.LineItems)
            {
                Console.WriteLine($"LineItem Name: {lineItem.Name["en"]}, Quantity: {lineItem.Quantity}");
            }
        }
    }
}
