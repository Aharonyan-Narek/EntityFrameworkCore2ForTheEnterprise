using System;

namespace Store.Core.EntityLayer.HumanResources
{
	public class Employee : IEntity
	{
		public Employee()
		{
		}

		public Int32? EmployeeID { get; set; }

		public String FirstName { get; set; }

		public String MiddleName { get; set; }

		public String LastName { get; set; }

		public DateTime? BirthDate { get; set; }
	}
}
