
namespace StringFilterExercise.Queries
{
    public interface IQuery<in TIn, out TOut>
    {
        TOut Invoke(TIn input);
    }
}
