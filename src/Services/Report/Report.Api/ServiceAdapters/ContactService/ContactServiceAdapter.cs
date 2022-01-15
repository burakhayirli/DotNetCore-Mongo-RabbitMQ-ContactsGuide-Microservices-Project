using Microsoft.Extensions.Configuration;
using Report.Api.HttpRequests;
using Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Report.Api.ServiceAdapters.ContactService
{
    public class ContactServiceAdapter : IContactService
    {
        //private readonly string URI_STRING = "http://localhost:5005/api/";
        public string UriString { get; set; }
        private readonly IConfiguration _configuration;
        public ContactServiceAdapter(IConfiguration configuration)//: base(UriString)
        {
            //this.UriString = UriString;    
            _configuration = configuration;
            UriString = _configuration.GetSection("OuterServices:ContactServiceEndpoint").Value;
            Console.WriteLine("ContactServiceAdapter has been triggered");
            Console.WriteLine("******************************************");
        }
        public async Task<List<Person>> GetAll()
        {
            HttpClient cLient = Client.GetClientConnection(UriString);
            List<Person> response = await cLient.GetFromJsonAsync<List<Person>>("persons");

            return response;
        }
    }
}
