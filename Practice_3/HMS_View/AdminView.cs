using HMS_BO;
using HMS_DAL;

namespace HMS_View
{
    public class AdminView
    {
        public void AddPatient()
        {
            // Method to get input from admin
            Console.WriteLine("Enter Patient Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Patient CNIC:");
            string? cnic = Console.ReadLine();
            Patient_BO p = new Patient_BO(name, cnic);
            PatientDAL pd = new PatientDAL();
            pd.AddPatientToDatabase(p);
            Console.WriteLine("Patient added successfully.");
        }

        public void UpdatePatient()
        {
            Console.WriteLine("Enter Patient CNIC to update:");
            string? cnic = Console.ReadLine();
            Console.WriteLine("Enter new Patient Name:");
            string? name = Console.ReadLine();
            PatientDAL pd = new PatientDAL();
            pd.UpdatePatientToDatabase(cnic,name);
            Console.WriteLine("Patient updated successfully.");
        }
        public void DeletePatient()
        {
            Console.WriteLine("Enter Patient CNIC to delete:");
            string? cnic = Console.ReadLine();
            PatientDAL pd = new PatientDAL();
            pd.DeletePatientToDatabase(cnic);
        }
        public void DisplayPatient()
        {
            Console.WriteLine("Displaying all patients:");
            PatientDAL pd = new PatientDAL();
            var list = pd.DisplayPatient();
            foreach (var patients in list)
            {
                Console.WriteLine($"Name: {patients.NAME}, CNIC: {patients.CNIC}");
            }
        }

        public void AddDoctor()
        {
            Console.WriteLine("Enter Doctor Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Doctor Specializatoin:");
            string? specialization = Console.ReadLine();
            Doctor_BO d = new Doctor_BO(name, specialization);
            DoctorDAL dd = new DoctorDAL();
            dd.AddDoctorToDatabase(d);
            Console.WriteLine("Doctor added successfully.");
        }
        public void UpdateDoctor()
        {
            Console.WriteLine("Enter Doctor ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Doctor Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Doctor Specializatoin:");
            string? specialization = Console.ReadLine();
            DoctorDAL dd = new DoctorDAL();
            dd.UpdateDoctorToDatabase(id,name,specialization);
            Console.WriteLine("Doctor updated successfully.");
        }
        public void DeleteDoctor()
        {
            Console.WriteLine("Enter Doctor ID to delete:");
            int id = Convert.ToInt32(Console.ReadLine());
            DoctorDAL dl = new DoctorDAL();
            dl.DeleteDoctorToDatabase(id);
            Console.WriteLine("Doctor deleted successfully.");
        }
        public void DisplayDoctors()
        {
            Console.WriteLine("Displaying ALL Doctors:");
            DoctorDAL dl = new DoctorDAL();
            var list = dl.DisplayDoctor();
            foreach (var doctors in list)
            {
                Console.WriteLine($"DoctorID: {doctors.GetDoctorID},Doctor Name: {doctors.GetName},Specialization: {doctors.GetSpecialization}");
            }
        }
    }
}
