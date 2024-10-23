using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto
{
    public class ResponseEntity
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ResponseEntity()
        {

        }

        public ResponseEntity(int code, dynamic data)
        {
            Code = code;
            Data = data;
        }

        public ResponseEntity(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ResponseEntity(int code, string message, dynamic data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
