using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using GetRecordsFromSqlTest.Module.BusinessObjects;
using UnboundListView.Module;
using Bogus;

namespace DXSqlTest.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        //string name = "MyName";
        //DomainObject1 theObject = ObjectSpace.FirstOrDefault<DomainObject1>(u => u.Name == name);
        //if(theObject == null) {
        //    theObject = ObjectSpace.CreateObject<DomainObject1>();
        //    theObject.Name = name;
        //}

        //ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).




        Book bookOne = ObjectSpace.CreateObject<Book>();
        bookOne.Title = "A Visitor For Bear";
        bookOne.Save();

        Book bookTwo = ObjectSpace.CreateObject<Book>();
        bookTwo.Title = "Dirt on My Shirt";
        bookTwo.Save();

        Book bookThree = ObjectSpace.CreateObject<Book>();
        bookThree.Title = "Bats at the Library";
        bookThree.Save();

        Book bookFour = ObjectSpace.CreateObject<Book>();
        bookFour.Title = "Fancy Nancy at the Museum";
        bookFour.Save();

        Book bookFive = ObjectSpace.CreateObject<Book>();
        bookFive.Title = "Fancy Nancy at the Museum";
        bookFive.Save();

        Book bookSix = ObjectSpace.CreateObject<Book>();
        bookSix.Title = "Bats at the Library";
        bookSix.Save();

        Book bookSeven = ObjectSpace.CreateObject<Book>();
        bookSeven.Title = "Bats at the Library";
        bookSeven.Save();

        var cusFaker = new Faker<Customer>("pl")
    .CustomInstantiator(f => ObjectSpace.CreateObject<Customer>())

    .RuleFor(o => o.Notes, f => f.Company.CatchPhrase())
    .RuleFor(o => o.CustomerName, f => f.Company.CompanyName())

    .RuleFor(o => o.City, f => f.Address.City())
    .RuleFor(o => o.PostalCode, f => f.Address.ZipCode())
    .RuleFor(o => o.Street, f => f.Address.StreetName())
    .RuleFor(o => o.Phone, f => f.Person.Phone)
    .RuleFor(o => o.Email, (f, c) => f.Internet.Email());
        cusFaker.Generate(100);



        ObjectSpace.CommitChanges(); //This line persists created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
