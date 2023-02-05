using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace GetRecordsFromSqlTest.Module.BusinessObjects
{
    [DomainComponent]
    public class OrderHist
    {
        [DevExpress.ExpressApp.Data.Key]
        public string ProductName { get; internal set; }
        public int Total { get; internal set; }
    }
}
