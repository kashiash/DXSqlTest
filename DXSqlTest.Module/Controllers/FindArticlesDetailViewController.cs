using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DXSqltest.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace DXSqlTest.Module.Controllers
{
    public class FindArticlesDetailViewController : ViewController<DetailView>
    {
        SimpleAction fillDataAction;
        public FindArticlesDetailViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(FindArticlesDialog);

            fillDataAction = new SimpleAction(this, $"{GetType().FullName}{nameof(fillDataAction)}", PredefinedCategory.Unspecified)
            {
                Caption = "Find articles"
            };
            fillDataAction.Execute += fillDataAction_Execute;
        }

        private void fillDataAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(FindArticlesDialog));
            objectSpace.ObjectsGetting += ObjectSpace_ObjectsGetting;
        }


        private void ObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)sender;
            var collection = new DynamicCollection(objectSpace, e.ObjectType, e.Criteria, e.Sorting, e.InTransaction);
            collection.FetchObjects += DynamicCollection_FetchObjects;
            e.Objects = collection;
        }
        private void DynamicCollection_FetchObjects(object sender, FetchObjectsEventArgs e)
        {

            e.Objects = GetDataFromSproc();
            e.ShapeData = true;
        }

        List<ResultClass> GetDataFromSproc()
        {
            XPObjectSpace persistentObjectSpace = (XPObjectSpace)ObjectSpace;
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
                obj.Licznik = (int)row.Values[columnNames["Licznik"]];

                objects.Add(obj);
            }
            return objects;
        }

    }

}
