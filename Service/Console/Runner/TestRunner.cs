using CommandLine;
using WBH.Livescoring.Service.Console.IoC;
using WBH.Livescoring.Service.IoC;

namespace WBH.Livescoring.Service.Console.Runner
{
    internal sealed class TestRunner : IRunner<TestOption>
    {
        #region IRunner

        public bool Run(TestOption configuration)
        {
            System.Console.WriteLine("Erfolgreich!");
            return true;
        }

        #endregion
    }

    [Verb("test", HelpText = "Gibt Erfolgreich aus!", Hidden = true)]
    internal class TestOption : IRunnerVerb {}
}
