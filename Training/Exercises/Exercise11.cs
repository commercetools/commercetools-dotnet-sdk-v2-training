using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;
using Type = commercetools.Sdk.Domain.Type;

namespace Training
{
    /// <summary>
    /// Add Discount Code to Cart - Make sure that discount code is active
    // 1- create a cart and get cartId (run ex6)
    // 2- get cart by Id, add the discount code to this cart (this exercise)
    // 3- add product to this cart (run ex7) - don't forget to change cartId
    // 4- Create the order and check the total price (run ex8) - don't forget to change cartId, you can check discounts on orders using MC
    /// </summary>
    public class Exercise11 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise11(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            AddDiscountCodeToTheCart();
        }

        /// <summary>
        /// Add Discount code to the cart
        /// </summary>
        private void AddDiscountCodeToTheCart()
        {
            // retrieve cart by Id
            Cart cart =
                _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Cart>(new Guid(Settings.CARTID))).Result;
            
            AddDiscountCodeUpdateAction addDiscountCodeUpdateAction = new AddDiscountCodeUpdateAction()
            {
               Code = Settings.DISCOUNTCODE
            };
            
            List<UpdateAction<Cart>> updateActions = new List<UpdateAction<Cart>>();
            updateActions.Add(addDiscountCodeUpdateAction);

            Cart retrievedCart = _commercetoolsClient
                .ExecuteAsync(new UpdateByIdCommand<Cart>(new Guid(cart.Id),
                    cart.Version, updateActions))
                .Result;
            Console.WriteLine($"Showing discount code added to cart {retrievedCart.Id}");
            foreach (var codeInfo in retrievedCart.DiscountCodes)
            {
                Console.WriteLine(codeInfo.DiscountCode.Id);
            }
        }
    }
}