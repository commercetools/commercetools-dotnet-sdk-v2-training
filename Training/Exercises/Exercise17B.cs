using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Orders;

namespace Training
{
    /// <summary>
    /// Delete Order
    /// </summary>
    public class Exercise17B : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise17B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //retrieve order by ordernumber to get it's version
            Order retrievedOrder = await _commercetoolsClient
                .ExecuteAsync(new GetOrderByOrderNumberCommand(Settings.ORDERNUMBER));

            retrievedOrder = await _commercetoolsClient
                .ExecuteAsync(new DeleteByOrderNumberCommand(retrievedOrder.OrderNumber, retrievedOrder.Version));

            Console.WriteLine($"Order Number: {retrievedOrder.OrderNumber} has been deleted");
        }
    }
}
