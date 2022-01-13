using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Report.Api.HttpRequests
{
    public class Client : ClientBase
    {
        protected HttpClient OpenClient;
        public Client(string uri)
        {
            OpenClient = ClientBase.GetClientConnection(uri);
            OpenClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
