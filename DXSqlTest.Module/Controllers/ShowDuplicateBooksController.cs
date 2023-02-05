using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using GetRecordsFromSqlTest.Module.BusinessObjects;

namespace UnboundListView.Module {
    public class ShowDuplicateBooksController : ObjectViewController<ListView, Book> {
     
        PopupWindowShowAction showQyery1Action;
        public ShowDuplicateBooksController()
        {
            PopupWindowShowAction showDuplicatesAction =
                new PopupWindowShowAction(this, "ShowDuplicateBooks", "View");
            showDuplicatesAction.CustomizePopupWindowParams += showDuplicatesAction_CustomizePopupWindowParams;

            showQyery1Action = new PopupWindowShowAction(this, $"{GetType().FullName}{nameof(showQyery1Action)}", PredefinedCategory.Unspecified) { 
            Caption = "Sql Query"
            };
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
            e.View = Application.CreateListView(directObjectSpace, typeof(OrderHist), true);


        }

        private void showQyery1Action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var selectedPopupWindowObjects = e.PopupWindowViewSelectedObjects;
            var selectedSourceViewObjects = e.SelectedObjects;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112723/).
        }

        void showDuplicatesAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e) {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach(Book book in View.CollectionSource.List) {
                if(!string.IsNullOrEmpty(book.Title)) {
                    if(dictionary.ContainsKey(book.Title)) {
                        dictionary[book.Title]++;
                    }
                    else 
                        dictionary.Add(book.Title, 1);
                }
            }
            var nonPersistentOS = Application.CreateObjectSpace(typeof(DuplicatesList));
            DuplicatesList duplicateList =nonPersistentOS.CreateObject<DuplicatesList>();
            int duplicateId = 0;
            foreach(KeyValuePair<string, int> record in dictionary) {
                if (record.Value > 1) {
                    var dup = nonPersistentOS.CreateObject<Duplicate>();
                    dup.Id = duplicateId;
                    dup.Title = record.Key;
                    dup.Count = record.Value;
                    duplicateList.Duplicates.Add(dup);
                    duplicateId++;
                }
            }
            nonPersistentOS.CommitChanges();
            e.View = Application.CreateDetailView(nonPersistentOS, duplicateList);
            e.DialogController.SaveOnAccept = false;
            e.DialogController.CancelAction.Active["NothingToCancel"] = false;
        }
    }
}
