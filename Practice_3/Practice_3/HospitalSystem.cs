using System;
using System.Collections.Generic;
using System.Text;

namespace Practice_3
{
    internal class HospitalSystem
    {
        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<Appointment> appointments;
        public HospitalSystem() { }
        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
            // inserting in both file and database

        }
    }

}
