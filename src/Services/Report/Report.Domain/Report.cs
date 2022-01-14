using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain
{
    public class Report : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? RequestDate { get; set; } = DateTime.Now;
        public DateTime? CreatedDate { get; set; }
        public DocumentStatus DocumentStatus { get; set; }        
    }

    public enum DocumentStatus
    {
        Creating,
        Completed
    }
}
