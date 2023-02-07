using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DXSqlTest.Module.BusinessObjects;

namespace DXSqltest.Module.BusinessObjects
{
  


    [DomainComponent, DefaultClassOptions]

    public class ResultClassSecond
    {
        [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; set; }


        public string City { get; set; }


        public int Licznik { get; set; }

    }


 

}
