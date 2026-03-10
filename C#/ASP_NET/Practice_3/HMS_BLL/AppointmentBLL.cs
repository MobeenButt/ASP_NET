using HMS_BO;
using HMS_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_BLL
{
    public class AppointmentBLL
    {
        private readonly AppointmentDAL appointmentDAL;
        private List<Appointment_BO> appointments;
        public AppointmentBLL()
        {
            appointmentDAL = new AppointmentDAL();
            appointments = new List<Appointment_BO>();
        }
        public void BookAppointment(int doctorID, string cnic, DateTime date)
        {
            appointmentDAL.BookAppointmentToDatabase(doctorID, cnic, date);
            Appointment_BO appointment = new Appointment_BO(doctorID, cnic, date);
            appointments.Add(appointment);
        }
        public void CancelAppointment(int appointmentID, string cnic)
        {
            appointmentDAL.CancelAppointment(appointmentID, cnic);
        }
        public void GetMostConsultedDoctor()
        {
            appointmentDAL.GetMostConsultedDoctor();
        }

    }
}
