using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_BO
{
    public class Doctor_BO
    {
        private int doctorId;
        private string? name;
        private string? specialization;
        private bool isAvailable;
        private int GenerateDoctorID()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }
        public Doctor_BO()
        {
            this.doctorId = GenerateDoctorID();
            this.name = string.Empty;
            this.specialization = string.Empty;
            this.isAvailable = true;
        }
        public Doctor_BO(string name, string specialization)
        {
            this.doctorId = GenerateDoctorID();
            this.name = name;
            this.specialization = specialization;
            this.isAvailable = true;
        }
        public int GetDoctorID { get => doctorId; set => doctorId = value; }
        public string? GetName { get => name; set => name = value; }
        public string? GetSpecialization { get => specialization; set => specialization = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public void MarkUnavailable()
        {
            isAvailable = false;
        }
        public void MarkAvailable()
        {
            isAvailable = true;
        }
        public override string ToString()
        {
            return $"Doctor ID: {doctorId}, Name: {name}, Specialization: {specialization}, Available: {isAvailable}";
        }
    }
}
