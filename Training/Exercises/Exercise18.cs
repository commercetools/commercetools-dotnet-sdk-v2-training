using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Orders;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;
using Type = commercetools.Sdk.Domain.Type;

namespace Training
{
    /// Add Discount Code to Cart - Make sure that discount code is active

    // 1- Create a cart (run Exercise15A)
    // 2- Add Product to it (run Exercise15B)
    // 3- Add the discount code to this cart (run this exercise)
    // 3- Create the order and check the total price (Exercise17A) - you can check discounts on orders using MC
    public class Exercise18 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise18(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            try
            {
                //Get filled Cart and add DiscountCode to it
                Cart cart =
                    await _commercetoolsClient.ExecuteAsync(new GetCartByCustomerIdCommand(new Guid(Settings.CUSTOMERID)));

                AddDiscountCodeUpdateAction addDiscountCodeUpdateAction = new AddDiscountCodeUpdateAction()
                {
                    Code = Settings.DISCOUNTCODE //must be active discount code for active cart discount
                };

                List<UpdateAction<Cart>> updateActions = new List<UpdateAction<Cart>> { addDiscountCodeUpdateAction };

                Cart retrievedCart = await _commercetoolsClient
                    .ExecuteAsync(new UpdateByIdCommand<Cart>(new Guid(cart.Id),
                        cart.Version, updateActions));

                Console.WriteLine($"Showing discount code added to cart {retrievedCart.Id}");
                foreach (var codeInfo in retrievedCart.DiscountCodes)
                {
                    Console.WriteLine(codeInfo.DiscountCode.Id);
                }

                //Then Create Order from this Cart
                OrderFromCartDraft orderFromCartDraft = new OrderFromCartDraft
                {
                    Id = retrievedCart.Id,
                    Version = retrievedCart.Version,
                    OrderNumber = $"Order{Settings.RandomInt()}"
                };
                Order order = await _commercetoolsClient.ExecuteAsync(new CreateCommand<Order>(orderFromCartDraft));

                //Display Order Number
                Console.WriteLine($"Order Created with order number: {order.OrderNumber}, and Total Price: {order.TotalPrice.CentAmount} cents");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
