using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Products;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Add Product to Cart
    /// </summary>
    public class Exercise7 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise7(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            AddProductToCart();
        }

        private void AddProductToCart()
        {
            // retrieve cart by customer Id
            Cart cart = GetCartByCustomerId();
            
            // get lineItemDraft and create AddLineItemUpdateAction
            var lineItemDraft = this.GetLineItemDraft(Settings.PRODUCTVARIANTSKU, 6);
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
            
            foreach (var lineItem in retrievedCart.LineItems)
            {
                Console.WriteLine($"LineItem Name: {lineItem.Name["en"]}, Quantity: {lineItem.Quantity}");
            }
            
        }
        /// <summary>
        /// Get the active Cart By Customer Id (that has been modified most recently) 
        /// </summary>
        /// <returns></returns>
        private Cart GetCartByCustomerId()
        {
            Cart cart =
                _commercetoolsClient.ExecuteAsync(new GetCartByCustomerIdCommand(Settings.CUSTOMERID)).Result;
            return cart;
        }
        
        public LineItemDraft GetLineItemDraft(string productVariantSku, int quantity = 1)
        {
            LineItemDraft lineItemDraft = new LineItemDraft();
            lineItemDraft.Sku = productVariantSku;
            lineItemDraft.Quantity = quantity;
            return lineItemDraft;
        }

        /// <summary>
        /// Get Cart By Id
        /// </summary>
        /// <returns></returns>
        private Cart GetCartById()
        {
            Cart cart =
                _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Cart>(new Guid(Settings.CARTID))).Result;
            return cart;
        }
     
    }
}