using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Report.Api.HttpRequests
{
    public class Base : Client
    {
        protected HttpClient OpenClient;
        public Base()
        {
            OpenClient = Client.GetClientConnection();
            OpenClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
