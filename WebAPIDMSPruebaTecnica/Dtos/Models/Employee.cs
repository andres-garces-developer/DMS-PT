using System;
using System.Collections.Generic;

namespace WebAPIDMSPruebaTecnica.Dtos.Models
{
    public partial class Employee
    {
        public int IdEmployee { get; set; }
        public string NameEmployee { get; set; } = null!;
        public string LastnameEmployee { get; set; } = null!;
        public int FkTypeAppointment { get; set; }
        public string EmailEmployee { get; set; } = null!;
        public DateTime DateContratation { get; set; }
        public string Avatar { get; set; } = null!;
        public bool? ConfirmEmail { get; set; }
        public bool? Status { get; set; }

        public virtual Appointment FkTypeAppointmentNavigation { get; set; } = null!;
    }
}
