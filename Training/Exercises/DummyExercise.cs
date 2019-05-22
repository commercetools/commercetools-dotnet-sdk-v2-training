using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using Microsoft.Extensions.Hosting;

namespace Training
{
    public class DummyExercise : IExercise
    {

        public DummyExercise()
        {
        }
        public async Task ExecuteAsync()
        {
            Console.WriteLine("Can't find this Exercise, please make sure of exercise name in args");
        }
    }
}
