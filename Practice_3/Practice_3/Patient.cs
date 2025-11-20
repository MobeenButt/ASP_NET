using System;
using System.Collections.Generic;
using System.Text;

namespace Practice_3
{
    internal class Patient
    {
        private string ?name;
        private string ?cnic;
        private List<int> Appointments;
        public Patient(string name,string cnic)
        {
            this.name = name;
            this.cnic = cnic;
        }
        public string CNIC { get=>cnic; }
        public bool hasAppointment(int appointmentId)
        {
            return Appointments.Contains(appointmentId);
        }

    }
}
