using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Add Discount Code to Cart - Make sure that discount code is active
    // 1- Create a cart and add product to it
    // 2- Add the discount code to this cart
    // 3- Create the order and check the total price - you can check discounts on orders using MC
    /// </summary>
    public class Exercise18 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise18(IClient commercetoolsClient)
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
            // retrieve Active cart by Customer ID
            Cart cart =
                this.CreateACartWithProduct();

            if (cart != null)
            {
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

                //Then Create Order from this Cart
                OrderFromCartDraft orderFromCartDraft = new OrderFromCartDraft();
                orderFromCartDraft.Id = retrievedCart.Id;
                orderFromCartDraft.Version = retrievedCart.Version;
                orderFromCartDraft.OrderNumber = $"Order{Settings.RandomInt()}";
                Order order = _commercetoolsClient.ExecuteAsync(new CreateCommand<Order>(orderFromCartDraft)).Result;

                //Display Order Number
                Console.WriteLine($"Order Created with order number: {order.OrderNumber}, and Total Price: {order.TotalPrice.CentAmount} cents");
            }
        }

        /// <summary>
        /// Create A Cart and add Product to it
        /// </summary>
        /// <returns>Cart with Product</returns>
        private Cart CreateACartWithProduct()
        {
            CartDraft cartDraft = this.GetCartDraft();
            Cart cart = _commercetoolsClient.ExecuteAsync(new CreateCommand<Cart>(cartDraft)).Result;
            if (cart != null)
            {
                // get lineItemDraft and create AddLineItemUpdateAction
                var lineItemDraft = this.GetLineItemDraft(Settings.PRODUCTVARIANTSKU, 2);
                AddLineItemBySkuUpdateAction addLineItemUpdateAction = new AddLineItemBySkuUpdateAction()
                {
                    LineItem = lineItemDraft,
                    Sku = Settings.PRODUCTVARIANTSKU,
                    Quantity = lineItemDraft.Quantity
                };

                List<UpdateAction<Cart>> updateActions = new List<UpdateAction<Cart>>();
                updateActions.Add(addLineItemUpdateAction);

                Cart retrievedCart = _commercetoolsClient
                    .ExecuteAsync(new UpdateByIdCommand<Cart>(new Guid(cart.Id),
                        cart.Version, updateActions))
                    .Result;

                return retrievedCart;
            }

            return null;
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
        private LineItemDraft GetLineItemDraft(string productVariantSku, int quantity = 1)
        {
            LineItemDraft lineItemDraft = new LineItemDraft();
            lineItemDraft.Sku = productVariantSku;
            lineItemDraft.Quantity = quantity;
            return lineItemDraft;
        }
    }
}
