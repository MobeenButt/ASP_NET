using System;
using System.Collections.Generic;
using System.Text;

namespace Practice_3
{
    internal class Appointment
    {
        private int appointmentId;
        private int doctorId;
        private string? patientCnic;
        private DateTime appointmentDate;
        private int GenerateAppointmentID()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }
        public Appointment(int doctorId,string patientCnic,DateTime date)
        {
            this.appointmentId = GenerateAppointmentID();
            this.doctorId = doctorId;
            this.patientCnic = patientCnic;
            this.appointmentDate = date;
        }
        public int getAppointmentID { get=>appointmentId; }
        public override string ToString()
        {
            return $"Appointment ID: {appointmentId}, Doctor ID: {doctorId}, Patient CNIC: {patientCnic}, Date: {appointmentDate}";
        }

    }
}
