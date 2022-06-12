using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using WBH.Livescoring.Service.Console.IoC;

namespace WBH.Livescoring.Service.Console;

internal class Program
{
    #region Fields

    private static ConsoleBootstrapper _bootstrapper;

    #endregion
    static void Main(string[] args)
    {
        // Info ausgeben
        System.Console.WriteLine(HeadingInfo.Default.ToString());
        System.Console.WriteLine(CopyrightInfo.Default.ToString());
        System.Console.WriteLine();

        // Bootstrapper instanziieren
        _bootstrapper = new ConsoleBootstrapper();

        // Verb Typen auslesen
        var verbTypes = Assembly.GetEntryAssembly()?.GetTypes()
            .Where(t => typeof(IRunnerVerb).IsAssignableFrom(t))
            .ToList();
        verbTypes ??= new List<Type>();

        // Argumente parsen und Anwendung ausführen
        var parser = new Parser(config => config.HelpWriter = System.Console.Out);
        parser.ParseArguments(args, verbTypes.ToArray())
            .MapResult(
                opts => _bootstrapper.Run(opts as IRunnerVerb) ? 0 : 1,
                _ => 1
            );
    }
}