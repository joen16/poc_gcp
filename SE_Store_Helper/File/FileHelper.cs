using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Helper.File
{
    public static class FileHelper
    {
        public static string GetExtension(string fileName)
        {
            string response = "";
            var parts = fileName.Split(".");

            response = parts[parts.Length - 1];
            return response;
        }
    }
}
