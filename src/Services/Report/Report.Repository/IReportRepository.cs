using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository
{
    public interface IReportRepository : IBaseRepository<Domain.Report, string>
    {
    }
}
