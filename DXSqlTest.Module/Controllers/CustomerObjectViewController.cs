using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Helpers;
using GetRecordsFromSqlTest.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRecordsFromSqlTest.Module.Controllers
{
    public class CustomerObjectViewController : ObjectViewController<ListView, Customer>
    {
        PopupWindowShowAction showQyery1Action;
        public CustomerObjectViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            showQyery1Action = new PopupWindowShowAction(this, $"{GetType().FullName}{nameof(showQyery1Action)}", PredefinedCategory.Unspecified);
            showQyery1Action.Execute += showQyery1Action_Execute;
            showQyery1Action.CustomizePopupWindowParams += showQyery1Action_CustomizePopupWindowParams;
            

        }

        private void showQyery1Action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            string query = "select City, count(*) licznik from Customer group by City ";
          //  NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(OrderHist));

            IObjectSpace directObjectSpace = Application.CreateObjectSpace();

            DevExpress.Xpo.Session session = ((XPObjectSpace)directObjectSpace).Session;
            var SqlResult = session.GetObjectsFromQuery<DXSqltest.Module.BusinessObjects.ResultClass>(query);

           // ....

            e.View = Application.CreateListView(directObjectSpace, typeof(OrderHist), true);


        }

        private void showQyery1Action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var selectedPopupWindowObjects = e.PopupWindowViewSelectedObjects;
            var selectedSourceViewObjects = e.SelectedObjects;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112723/).
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
        }
    }


}
