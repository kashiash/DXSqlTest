using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DXSqltest.Module.BusinessObjects;

namespace DXSqlTest.Module.BusinessObjects
{
    [DomainComponent]
    public class FindArticlesDialog : BoundNonPersistentObjectBase
    {
        private String cityName;
        [ImmediatePostData]
        public String CityName
        {
            get { return cityName; }
            set { SetPropertyValue<String>(nameof(CityName), ref cityName, value); }
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


        public void ClearData()
        {


            results.RaiseListChangedEvents = false;
            results.Clear();
            results.RaiseListChangedEvents = true;
            results.ResetBindings();
            OnPropertyChanged(nameof(Results));
        }

        internal void LoadData(Session session)
        {
            results.RaiseListChangedEvents = false;
            results.Clear();
            string query = $"select newid() Oid ,City, count(*) Licznik from Customer ";
            if (!string.IsNullOrEmpty(CityName))
            {
                query = $"{query} where City like '%{CityName}%' ";
            }

            query = $" {query} group by City ";


            foreach (var item in session.GetObjectsFromQuery<ResultClassSecond>(query))
            { 
               results.Add(item);
            }
            results.RaiseListChangedEvents = true;
            results.ResetBindings();
            OnPropertyChanged(nameof(Results));
        }

        //private void UpdateResults()
        //{
        //    if (results != null)
        //    {

        //        results.RaiseListChangedEvents = false;
        //        results.Clear();
        //        //foreach (var obj in ObjectSpace.GetObjects<Article>(filter))
        //        //{
        //        //    results.Add(obj);
        //        //}


        //     //   GetDataFromSproc();

        //        results.RaiseListChangedEvents = true;
        //        results.ResetBindings();
        //        OnPropertyChanged(nameof(Results));
        //    }
        //}

    }
}
