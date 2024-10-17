using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPIDMSPruebaTecnica.Dtos.Models
{
    public partial class Appointment
    {
        public int IdAppointment { get; set; }
        public string DescriptionAppointment { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
