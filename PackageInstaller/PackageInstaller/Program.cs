using System;
using Autofac;
using PackageInstaller.Controllers;
using System.Reflection;

namespace PackageInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetContainer();
            RunInstaller(args, container);
        }

        private static void RunInstaller(string[] args, IContainer container)
        {
            var packageInstaller = container.Resolve<IPackageInstaller>();
            var output = packageInstaller.GetPackageInstallationString(args);
            Console.WriteLine(output);
        }

        private static IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            var container = containerBuilder.Build();
            return container;
        }
    }
}
