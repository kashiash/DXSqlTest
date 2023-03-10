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

    public class ResultClass
    {
        [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; set; }


        public string City { get; set; }


        public int Licznik { get; set; }

    }


    class ResultClassAdapter
    {
        NonPersistentObjectSpace objectSpace;
        public ResultClassAdapter(NonPersistentObjectSpace npos)
        {
            objectSpace = npos;
            objectSpace.ObjectsGetting += Npos_ObjectsGetting;
        }
        private void Npos_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType != typeof(ResultClass))
            {
                return;
            }
            var collection = new DynamicCollection(objectSpace, e.ObjectType, e.Criteria, e.Sorting, e.InTransaction);
            collection.FetchObjects += DynamicCollection_FetchObjects;
            e.Objects = collection;
        }
        private void DynamicCollection_FetchObjects(object sender, FetchObjectsEventArgs e)
        {
            if (e.ObjectType == typeof(ResultClass))
            {
                e.Objects = GetDataFromSproc();
                e.ShapeData = true;
            }
        }
        List<ResultClass> GetDataFromSproc()
        {
            XPObjectSpace persistentObjectSpace = objectSpace.AdditionalObjectSpaces.OfType<XPObjectSpace>().First();
            Session session = persistentObjectSpace.Session;
            SelectedData results = session.ExecuteQueryWithMetadata("select newid() Oid ,City, count(*) Licznik from Customer group by City ");
            Dictionary<string, int> columnNames = new Dictionary<string, int>();
            for (int columnIndex = 0; columnIndex < results.ResultSet[0].Rows.Length; columnIndex++)
            {
                string columnName = results.ResultSet[0].Rows[columnIndex].Values[0] as string;
                columnNames.Add(columnName, columnIndex);
            }
            List<ResultClass> objects = new List<ResultClass>();
            foreach (SelectStatementResultRow row in results.ResultSet[1].Rows)
            {
                ResultClass obj = new ResultClass();
                obj.Oid = (Guid)row.Values[columnNames["Oid"]];
                obj.City = row.Values[columnNames["City"]] as string;
                obj.Licznik = (int)row.Values[columnNames["Licznik"]] ;
            
                objects.Add(obj);
            }
            return objects;
        }
    }

}
