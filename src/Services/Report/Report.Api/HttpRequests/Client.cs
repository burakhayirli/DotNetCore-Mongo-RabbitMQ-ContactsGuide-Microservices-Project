using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Report.Api.HttpRequests
{
    public class Client
    {
        public static HttpClient GetClientConnection()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5005/api/")
            };

            return client;
        }
    }
}
