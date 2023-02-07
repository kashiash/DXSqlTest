using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DXSqltest.Module.BusinessObjects;
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

            string query = "select newid() Oid ,City, count(*) licznik from Customer group by City ";
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(ResultClassSecond));

            objectSpace.ObjectsGetting += ObjectSpace_ObjectsGetting;
            e.View = Application.CreateListView(objectSpace, typeof(ResultClassSecond), true);


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


        List<ResultClassSecond> GetDataFromSproc()
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
            List<ResultClassSecond> objects = new List<ResultClassSecond>();
            foreach (SelectStatementResultRow row in results.ResultSet[1].Rows)
            {
                ResultClassSecond obj = new ResultClassSecond();
                obj.Oid = (Guid)row.Values[columnNames["Oid"]];
                obj.City = row.Values[columnNames["City"]] as string;
                obj.Licznik = (int)row.Values[columnNames["Licznik"]];

                objects.Add(obj);
            }
            return objects;
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
