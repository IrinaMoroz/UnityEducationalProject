using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class InitializationComponentException : Exception
{
    public InitializationComponentException(string componentName) : base($"{componentName} failed to initialize.")
    {
    }

    public InitializationComponentException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
