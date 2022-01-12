using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Report.Api.HttpRequests
{
    public class ClientManager : Base
    {
        public ClientManager()
        {

        }

        //public async Task<ICollection<BooksViewModel>> GetAll()
        //{
        //    HttpResponseMessage response = OpenClient.GetAsync("books").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return response.Content.ReadAsAsync<ICollection<BooksViewModel>>().Result;
        //    }
        //    return null;
        //}

        //public async Task<bool> New(CreateBookModel book)
        //{
        //    if (book == null) { return false; }

        //    HttpResponseMessage response = OpenClient.PostAsJsonAsync("Books/", book).Result;
        //    //HttpResponseMessage response = OpenClient.PostAsync<>("Books/", book).Result;
        //    var result = response.EnsureSuccessStatusCode();

        //    if (result.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    //if(result.StatusCode!= System.Net.HttpStatusCode.OK)
        //    //{
        //    //    return false;
        //    //}
        //    return false;
        //}
    }
}
