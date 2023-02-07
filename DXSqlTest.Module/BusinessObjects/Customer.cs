using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRecordsFromSqlTest.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class Customer : BaseObject
	{
		public Customer(Session session) : base(session)
		{ }


		string notes;
		string email;
		string phone;
		string postalCode;
		string city;
		string adres;
		string nazwa;

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string CustomerName
		{
			get => nazwa;
			set => SetPropertyValue(nameof(CustomerName), ref nazwa, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string City
		{
			get => city;
			set => SetPropertyValue(nameof(City), ref city, value);
		}

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string PostalCode
		{
			get => postalCode;
			set => SetPropertyValue(nameof(PostalCode), ref postalCode, value);
		}
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Street

		{

			get => adres;
			set => SetPropertyValue(nameof(Street), ref adres, value);
		}

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Phone
		{
			get => phone;
			set => SetPropertyValue(nameof(Phone), ref phone, value);
		}

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Email
		{
			get => email;
			set => SetPropertyValue(nameof(Email), ref email, value);
		}
		
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Notes
		{
			get => notes;
			set => SetPropertyValue(nameof(Notes), ref notes, value);
		}
	}
}
