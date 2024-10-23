using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Helper.Exceptions
{
    public class BusinessException : Exception
    {
        override
        public string Message {  get; }
        public Exception InnerException { get; set; }

        public BusinessException()
        {
           
        }

        public BusinessException(string message) 
        {
            this.Message = message;
        }

        public BusinessException(Exception innerException) 
        {
            this.InnerException = innerException;
        }

        public BusinessException(string message, Exception innerException)
        {
            this.Message = message;
            this.InnerException = innerException;
        }
    }
}
