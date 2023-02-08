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

namespace DXSqlTest.Module.Controllers
{
    public class FindArticlesController : ViewController
    {

        private PopupWindowShowAction action;
       
        public FindArticlesController()
        {

            


            action = new PopupWindowShowAction(this, "FindArticles", PredefinedCategory.View);
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

    [DomainComponent]
    public class FindArticlesDialog : BoundNonPersistentObjectBase
    {
        private String _Author;
        [ImmediatePostData]
        public String Author
        {
            get { return _Author; }
            set { SetPropertyValue<String>(nameof(Author), ref _Author, value); }
        }
        private float _AuthorMinRating;
        [Appearance("", Enabled = false, Criteria = "Author is not null")]
        public float AuthorMinRating
        {
            get { return _AuthorMinRating; }
            set { SetPropertyValue<float>(nameof(AuthorMinRating), ref _AuthorMinRating, value); }
        }
        private BindingList<ResultClassSecond> results;
        public BindingList<ResultClassSecond> Results
        {
            get
            {
                if (results == null)
                {
                    results = new BindingList<ResultClassSecond>();
                }
                return results;
            }
        }
        private void UpdateResults()
        {
            if (results != null)
            {
                
                results.RaiseListChangedEvents = false;
                results.Clear();
                //foreach (var obj in ObjectSpace.GetObjects<Article>(filter))
                //{
                //    results.Add(obj);
                //}
                results.RaiseListChangedEvents = true;
                results.ResetBindings();
                OnPropertyChanged(nameof(Results));
            }
        }

        [Action(PredefinedCategory.Filters)]
        public void Find()
        {
            UpdateResults();
        }



    }
}
