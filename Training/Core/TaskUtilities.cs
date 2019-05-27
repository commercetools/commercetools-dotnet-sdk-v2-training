using System;
using System.Threading.Tasks;

namespace Training
{
    public static class TaskUtilities
    {
        public static async Task FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
