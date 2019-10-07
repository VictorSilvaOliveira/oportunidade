using MinutoSeguro.Entity;

namespace MinutoSeguro.Manager.ActionManipulation
{
    public abstract class IActionManipulation<S, T>
    {
        public delegate void ExecuteFinish(T result);

        public ExecuteFinish OnExecuteFinished;

        public abstract void Execute(S source);
    }
}