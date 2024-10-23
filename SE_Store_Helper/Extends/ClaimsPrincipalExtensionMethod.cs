using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Helper.Extends
{
    public static class ClaimsPrincipalExtensionMethod
    {
        public static string Email(this ClaimsPrincipal User)
        {
           var r = User.Claims.FirstOrDefault(c => c.Type == "Email");
            if(r != null)
            {
                return r.Value; 
            }
            return null;
        }

        public static long Id(this ClaimsPrincipal User)
        {
            var r = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (r != null)
            {
                return Convert.ToInt64(r.Value);
            }
            return 0;
        }

        public static long IdEntidad(this ClaimsPrincipal User)
        {
            var r = User.Claims.FirstOrDefault(c => c.Type == "IdEntidad");
            if (r != null)
            {
                return Convert.ToInt64(r.Value);
            }
            return 0;
        }
    }
}
