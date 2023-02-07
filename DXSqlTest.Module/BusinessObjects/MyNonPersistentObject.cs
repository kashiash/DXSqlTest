using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;

namespace DXSqlTest.Module.BusinessObjects
{
    [DomainComponent, DefaultClassOptions]
    public class MyNonPersistentObject
    {
        [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; internal set; }
        public string CustomerName { get; internal set; }
        public string City { get; internal set; }
        public string Street { get; internal set; }
    }

    class MyNonPersistentObjectAdapter
    {
        NonPersistentObjectSpace objectSpace;
        public MyNonPersistentObjectAdapter(NonPersistentObjectSpace npos)
        {
            objectSpace = npos;
            objectSpace.ObjectsGetting += Npos_ObjectsGetting;
        }
        private void Npos_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType != typeof(MyNonPersistentObject))
            {
                return;
            }
            var collection = new DynamicCollection(objectSpace, e.ObjectType, e.Criteria, e.Sorting, e.InTransaction);
            collection.FetchObjects += DynamicCollection_FetchObjects;
            e.Objects = collection;
        }
        private void DynamicCollection_FetchObjects(object sender, FetchObjectsEventArgs e)
        {
            if (e.ObjectType == typeof(MyNonPersistentObject))
            {
                e.Objects = GetDataFromSproc();
                e.ShapeData = true;
            }
        }
        List<MyNonPersistentObject> GetDataFromSproc()
        {
            XPObjectSpace persistentObjectSpace = objectSpace.AdditionalObjectSpaces.OfType<XPObjectSpace>().First();
            Session session = persistentObjectSpace.Session;
            SelectedData results = session.ExecuteQueryWithMetadata("select Oid,CustomerName,City,Street from Customer");
            Dictionary<string, int> columnNames = new Dictionary<string, int>();
            for (int columnIndex = 0; columnIndex < results.ResultSet[0].Rows.Length; columnIndex++)
            {
                string columnName = results.ResultSet[0].Rows[columnIndex].Values[0] as string;
                columnNames.Add(columnName, columnIndex);
            }
            List<MyNonPersistentObject> objects = new List<MyNonPersistentObject>();
            foreach (SelectStatementResultRow row in results.ResultSet[1].Rows)
            {
                MyNonPersistentObject obj = new MyNonPersistentObject();
                obj.Oid = (Guid)row.Values[columnNames["Oid"]];
                obj.CustomerName = row.Values[columnNames["CustomerName"]] as string;
                obj.City = row.Values[columnNames["City"]] as string;
                obj.Street = row.Values[columnNames["Street"]] as string;
                objects.Add(obj);
            }
            return objects;
        }
    }
}
