using System;

namespace PackageInstaller.Exceptions
{
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(string message) : base(message)
        {
            
        }
    }
}
