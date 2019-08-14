using Autofac;
using PackageInstaller.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var output = packageInstaller.InstallPackages(args[0]);
        }
    }
}
