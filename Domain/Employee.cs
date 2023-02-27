using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public abstract class Employee : Base
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [DisplayName("Employee Status")]
        public EmployeeStatus EmployeeStatus { get; set; } = EmployeeStatus.Other;
        [DisplayName("Date Of Hire")]
        public DateTime HireDate { get; set; }
    }

    public class Engineer : Employee, IShouldBeTracked
    {
        
        [ForeignKey("LastKnownLocation")]
        public int LastKnownLocationId { get; set; }

        [DisplayName("Last Known Location")]
        public Location? LastKnownLocation { get; set; }
    }
}
