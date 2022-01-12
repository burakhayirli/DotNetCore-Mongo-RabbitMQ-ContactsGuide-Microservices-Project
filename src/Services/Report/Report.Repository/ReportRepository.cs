using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository
{
    public class ReportRepository : BaseRepository<Domain.Report>, IReportRepository
    {
        private readonly IDbContext<Domain.Report> context;

        public ReportRepository(IDbContext<Domain.Report> context) : base(context)
        {
            this.context = context;
        }
    }
}
