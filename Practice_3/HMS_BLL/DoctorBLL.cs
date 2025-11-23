using HMS_BO;
using HMS_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_BLL
{

    public class DoctorBLL
    {

        private List<Doctor_BO> doctors;
        private readonly DoctorDAL doctorDAL;
        public DoctorBLL()
        {
            doctors = new List<Doctor_BO>();
            doctorDAL = new DoctorDAL();
        }
        public void AddDoctor(Doctor_BO doctor)
        {
            doctorDAL.AddDoctorToDatabase(doctor);
            doctors.Add(doctor);
        }
        public void UpdateDoctor(int doctorId, string name, string specialization)
        {
            doctorDAL.UpdateDoctorToDatabase(doctorId, name, specialization);
        }
        public void DeleteDoctor(int doctorId)
        {
            doctorDAL.DeleteDoctorToDatabase(doctorId);
        }
        public void DisplayDoctor()
        {
            var list = doctorDAL.DisplayDoctor();
            foreach (var doctors in list)
            {
                Console.WriteLine($"DoctorID: {doctors.GetDoctorID},Doctor Name: {doctors.GetName},Specialization: {doctors.GetSpecialization}");
            }
        }
    }
}
