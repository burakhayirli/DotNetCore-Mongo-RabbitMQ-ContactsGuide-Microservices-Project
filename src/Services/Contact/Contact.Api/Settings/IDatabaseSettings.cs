using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Settings
{
   public interface IDatabaseSettings
    {
        public string PersonCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}