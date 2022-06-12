namespace WBH.Livescoring.Service.IoC
{
    public interface IRunner<TRunnerVerb> where TRunnerVerb : class, IRunnerVerb
    {
        bool Run(TRunnerVerb configuration);
    }
}