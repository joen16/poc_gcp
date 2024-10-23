using Google.Cloud.Storage.V1;
using Microsoft.VisualBasic;
using SE_Store_Dto.Integration.Izipay.CreateToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Integration
{
    public class GcpIntegration : BaseInt
    {
        private string _bucketName;

        public void InitBucket(string bucketName)
        {
            this._bucketName = bucketName;
        }



        public void UploadFile(string name, string contentType, byte[] data)
        {
            var storage = StorageClient.Create();

            MemoryStream stream = new MemoryStream(data);
            var response = storage.UploadObject(_bucketName, name, "application/octet-stream", stream);

            Console.WriteLine($" {name} uploaded to bucket {_bucketName} ");
        }
    }
}
