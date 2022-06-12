namespace WBH.Livescoring.Service.Console.IoC;

public interface IRunner<TRunnerVerb> where TRunnerVerb : class, IRunnerVerb
{
    bool Run(TRunnerVerb configuration);
}