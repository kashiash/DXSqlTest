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
using GetRecordsFromSqlTest.Module.BusinessObjects;
using DXSqlTest.Module.BusinessObjects;

namespace DXSqlTest.Module.Controllers
{
    public class FindCitiesDetailViewController : ViewController<DetailView>
    {
        SimpleAction fillDataAction;
        public FindCitiesDetailViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(FindArticlesDialog);

            fillDataAction = new SimpleAction(this, $"{GetType().FullName}{nameof(fillDataAction)}", PredefinedCategory.Filters)
            {
                Caption = "Load data"
            };
            fillDataAction.Execute += fillDataAction_Execute;
        }

        private void fillDataAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
           var currentObject = View.CurrentObject as FindArticlesDialog;
           XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace(typeof(Customer));
           Session session = persistentObjectSpace.Session;
           currentObject.LoadData(session);
        }



  private void  GetDataFromSproc()
        {



            //SelectedData results = session.ExecuteQueryWithMetadata(query);
            //Dictionary<string, int> columnNames = new Dictionary<string, int>();
            //for (int columnIndex = 0; columnIndex < results.ResultSet[0].Rows.Length; columnIndex++)
            //{
            //    string columnName = results.ResultSet[0].Rows[columnIndex].Values[0] as string;
            //    columnNames.Add(columnName, columnIndex);
            //}
         
            //foreach (SelectStatementResultRow row in results.ResultSet[1].Rows)
            //{
            //    ResultClassSecond obj = new ResultClassSecond();
            //    obj.Oid = (Guid)row.Values[columnNames["Oid"]];
            //    obj.City = row.Values[columnNames["City"]] as string;
            //    obj.Licznik = (int)row.Values[columnNames["Licznik"]];

            //    currentObject.Results.Add(obj);
            //}
      
        }

    }

}
