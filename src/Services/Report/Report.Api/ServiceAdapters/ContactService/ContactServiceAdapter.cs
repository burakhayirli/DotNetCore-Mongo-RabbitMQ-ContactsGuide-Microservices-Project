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
    public class ContactServiceAdapter : Client, IContactService
    {
        private const string URI_STRING = "http://localhost:5005/api/";
        public ContactServiceAdapter() : base(URI_STRING)
        {
            Console.WriteLine("ContactServiceAdapter has been triggered");
        }
        public async Task<List<Person>> GetAll()
        {
            HttpClient cLient = GetClientConnection(URI_STRING);
            List<Person> response = await cLient.GetFromJsonAsync<List<Person>>("persons");

            return response;
        }
    }
}
