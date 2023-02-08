using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DXSqltest.Module.BusinessObjects;
using DXSqlTest.Module.BusinessObjects;
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using GetRecordsFromSqlTest.Module.BusinessObjects;

namespace DXSqlTest.Module.Controllers
{
    public class FindArticlesController : ViewController
    {

        private PopupWindowShowAction action;
       
        public FindArticlesController()
        {

            


            action = new PopupWindowShowAction(this, "Find Cities", PredefinedCategory.View);
            action.CustomizePopupWindowParams += Action_CustomizePopupWindowParams;
            action.Execute += Action_Execute;
        }
        private void fillDataAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        private void Action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace(typeof(FindArticlesDialog));
            var obj = new FindArticlesDialog();
            var detailView = Application.CreateDetailView(objectSpace, obj);
            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.View = detailView;
        }

        private void Action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
        }

    }


}
