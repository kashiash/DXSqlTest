using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace DXSqltest.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    public class ResultClass : NonPersistentLiteObject
    {

        int licznik;
        string city;

       
        public string City
        {
            get => city;
            set => SetPropertyValue(ref city, value);
        }

        public int Licznik
        {
            get => licznik;
            set => SetPropertyValue(ref licznik, value);
        }
    }


    //[DomainComponent]

    //public class ResultClass 
    //{

    //    int licznik;
    //    string city;


    //    public string City { get; set; }


    //    public int Licznik { get; set; }

    //}
}
