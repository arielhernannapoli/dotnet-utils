using System;
using System.Linq;
using System.Reflection;
using dotnet_utils.src.builders;
using dotnet_utils.src.contracts;
using dotnet_utils.src.parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace dotnet_utils
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var versionString = Assembly.GetEntryAssembly()
                                            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                            .InformationalVersion
                                            .ToString();

            if(args.Length == 0) {                
                Console.WriteLine($"dotnet-utils v{versionString}");                
                Console.WriteLine("\nUsage: dotnet-utils [options]");
                Console.WriteLine("\nOptions:");
                Console.WriteLine("-h|--help    Display help.");
            }

            if(args.Length == 1 && (args[0].Contains("--help") || args[0].Contains("-h")))
            {
                Console.WriteLine($"dotnet-utils v{versionString}");
                Console.WriteLine("\nComandos:");
                Console.WriteLine("  create      Crea un nuevo microservicio.");
            }

            if(args.Length == 2 && args[0] == "create" && (args[1].Contains("--help") || args[1].Contains("-h")))
            {
                Console.WriteLine("Uso: dotnet-utils create [opciones]");
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("  --name <NOMBRE_MICROSERVICIO>      Nombre del microservicio.");
            }

            if(args.Length == 3 && args[0] == "create" && args[1] == "--name")
            {
                var builder = serviceProvider.GetService<MicroServiceBuilder>();
                var result = builder.Build(args[2]);
            }
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                                            .AddLogging()
                                            .AddTransient<IParser, SkeletonParser>()
                                            .AddTransient<MicroServiceBuilder>()
                                            .BuildServiceProvider();
            return serviceProvider;                                        
        }
    }
}
