using System.Threading;
using System.Threading.Tasks;

namespace Training
{
    public interface IExercise
    {
        Task ExecuteAsync();
    }
}
