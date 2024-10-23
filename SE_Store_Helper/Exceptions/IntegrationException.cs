using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Helper.Exceptions
{
    public class IntegrationException : Exception
    {
        public IntegrationException()
        {

        }

        public IntegrationException(string message) : base(message)
        {

        }

        public IntegrationException(Exception innerException) : base("No se pudo realizar la operación.", innerException)
        {
            // Not ImplementedException
        }

        public IntegrationException(string message, Exception innerException) : base(message, innerException)
        {
            // Not ImplementedException
        }
    }
}
