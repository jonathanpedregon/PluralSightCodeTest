using Autofac;
using PackageInstaller.Controllers;
using System.Reflection;

namespace PackageInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            var container = containerBuilder.Build();
            var packageInstaller = container.Resolve<IPackageInstaller>();
            var output = packageInstaller.GetPackageInstallationString(args);
        }
    }
}
