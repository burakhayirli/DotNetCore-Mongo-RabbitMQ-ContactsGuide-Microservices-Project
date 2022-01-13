using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Report.Api.HttpRequests
{
    public class ClientBase
    {
        public static HttpClient GetClientConnection(string uriString)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(uriString)
            };

            return client;
        }
    }
}
