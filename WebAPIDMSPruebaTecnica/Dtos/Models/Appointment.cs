using System;
using System.Collections.Generic;

namespace WebAPIDMSPruebaTecnica.Dtos.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdAppointment { get; set; }
        public string DescriptionAppointment { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
