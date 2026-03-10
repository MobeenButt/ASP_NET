using HMS_BO;
using HMS_BLL;

namespace HMS_View
{
    public class AdminView
    {
        private readonly PatientBLL patientBLL;
        private readonly DoctorBLL doctorBLL;
        private readonly AppointmentBLL appointmentBLL;

        public AdminView()
        {
            patientBLL = new PatientBLL();
            doctorBLL = new DoctorBLL();
            appointmentBLL = new AppointmentBLL();
        }
        
        public void Menu()
        {
            Console.WriteLine("1. Add Patient");
            Console.WriteLine("2. Update Patient (string cnic)");
            Console.WriteLine("3. Delete Patient(string cnic)");
            Console.WriteLine("4. Display Patients");
            Console.WriteLine("5. Add Doctor");
            Console.WriteLine("6. Update Doctor(int doctorID)");
            Console.WriteLine("7. Delete Doctor(int doctorID)");
            Console.WriteLine("8. Display Doctors");
            Console.WriteLine("9. Book Appointment(int doctorID,string cnic,DateTime date)");
            Console.WriteLine("10. Cancel Appointment(int appointmentID,string cnic)");
            Console.WriteLine("11. Declare Most Consulted Doctor");
            Console.WriteLine("12. Exit");

            int option;
            do
            {
                Console.WriteLine("Enter your option:");
                option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {

                    AddPatient();
                }
                if (option == 2)
                {
                    UpdatePatient();
                }
                if (option == 3)
                {
                    DeletePatient();
                }
                if (option == 4)
                {
                    DisplayPatient();
                }
                if (option == 5)
                {
                    AddDoctor();
                }
                if (option == 6)
                {
                    UpdateDoctor();
                }
                if (option == 7)
                {
                    DeleteDoctor();
                }
                if (option == 8)
                {
                    DisplayDoctors();
                }
                if (option == 9)
                {
                    BookAppointment();
                }
                if (option == 10)
                {
                    CancelAppointment();
                }
                if (option == 11)
                {
                    DeclareMostConsultedDoctor();
                }
                if (option == 12)
                {
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                    Menu();
                }

            } while (option != 12);
        }

        public void AddPatient()
        {
            // Method to get input from admin
            Console.WriteLine("Enter Patient Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Patient CNIC:");
            string? cnic = Console.ReadLine();
            Patient_BO p = new Patient_BO(name, cnic);
            patientBLL.AddPatient(p);
            Console.WriteLine("Patient added successfully.");
            Menu();
        }

        public void UpdatePatient()
        {
            Console.WriteLine("Enter Patient CNIC to update:");
            string? cnic = Console.ReadLine();
            Console.WriteLine("Enter new Patient Name:");
            string? name = Console.ReadLine();
            patientBLL.UpdatePatient(cnic, name);
            Console.WriteLine("Patient updated successfully.");
            Menu();
        }
        public void DeletePatient()
        {
            Console.WriteLine("Enter Patient CNIC to delete:");
            string? cnic = Console.ReadLine();
            patientBLL.DeletePatient(cnic);
            Menu();
        }
        public void DisplayPatient()
        {
            Console.WriteLine("Displaying all patients:");
            var list = patientBLL.DisplayPatient();
            foreach (var patients in list)
            {
                Console.WriteLine($"Name: {patients.NAME}, CNIC: {patients.CNIC}");
            }
            Menu();
        }

        public void AddDoctor()
        {
            Console.WriteLine("Enter Doctor Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Doctor Specializatoin:");
            string? specialization = Console.ReadLine();
            Doctor_BO d = new Doctor_BO(name, specialization);
            doctorBLL.AddDoctor(d);
            Console.WriteLine("Doctor added successfully.");
            Menu();
        }
        public void UpdateDoctor()
        {
            Console.WriteLine("Enter Doctor ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Doctor Name:");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter Doctor Specializatoin:");
            string? specialization = Console.ReadLine();
            doctorBLL.UpdateDoctor(id, name, specialization);
            Console.WriteLine("Doctor updated successfully.");
            Menu();
        }
        public void DeleteDoctor()
        {
            Console.WriteLine("Enter Doctor ID to delete:");
            int id = Convert.ToInt32(Console.ReadLine());
            doctorBLL.DeleteDoctor(id);
            Console.WriteLine("Doctor deleted successfully.");
            Menu();
        }
        public void DisplayDoctors()
        {
            Console.WriteLine("Displaying ALL Doctors:");

            Menu();
        }
        public void BookAppointment()
        {
            Console.WriteLine("Enter Doctor ID:");
            int doctorID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Patient CNIC:");
            string? cnic = Console.ReadLine();
            DateTime date = DateTime.Now;
            appointmentBLL.BookAppointment(doctorID, cnic, date);
            Menu();
        }
        public void CancelAppointment()
        {
            Console.WriteLine("Enter Appointment ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Patient CNIC:");
            string? cnic = Console.ReadLine();
            appointmentBLL.CancelAppointment(id, cnic);
            Menu();
        }
        public void DeclareMostConsultedDoctor()
        {
            appointmentBLL.GetMostConsultedDoctor();
            Menu();
        }
    }
}
