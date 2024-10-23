using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Dto.Enum
{
    public class ConfigEnum
    {
        public static string STORE_BUCKET_ID = "STORE.BUCKET.ID";
        public static string STORE_BUCKET_URL_PUBLIC = "STORE.BUCKET.URL.PUBLIC";
        public static string JWT_SECRET_KEY = "JWT.SECRET.KEY";
        public static string JWT_AUDIENCE_TOKEN = "JWT.AUDIENCE.TOKEN";
        public static string JWT_ISSUER_TOKEN = "JWT.ISSUER.TOKEN";
        public static string JWT_EXPIRE_MINUTES = "JWT.EXPIRE.MINUTES";
        public static string JWT_REFRESH_EXPIRE_DAYS = "JWT.REFRESH.EXPIRE.DAYS";
    }
}
