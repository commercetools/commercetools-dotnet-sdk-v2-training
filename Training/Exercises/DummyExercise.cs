using System;
using System.Threading.Tasks;

namespace Training
{
    public class DummyExercise : IExercise
    {
        public async Task ExecuteAsync()
        {
            Console.WriteLine("Can't find this Exercise, please make sure of exercise name in args");
        }
    }
}
