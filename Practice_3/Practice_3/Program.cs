using HMS_View;
using System;

namespace Practice_3
{
  public class Program
    {
        public static void Main(string[] args)
        {
            AdminView adminView = new AdminView();
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
                    
                    adminView.AddPatient();
                }
                if(option == 2)
                {
                    adminView.UpdatePatient();
                }
                if(option == 3)
                {
                    adminView.DeletePatient();
                }
                if(option == 4)
                {
                    adminView.DisplayPatient();
                }
                if(option == 5)
                {
                    adminView.AddDoctor();
                }
                if(option == 6)
                {
                    adminView.UpdateDoctor();
                }
                if(option == 7)
                {
                    adminView.DeleteDoctor();
                }
                if(option == 8)
                {
                    adminView.DisplayDoctors();
                }
                if(option == 9)
                {
                    // Book Appointment
                }
                if(option == 10)
                {
                    // Cancel Appointment
                }
                if(option==11)
                {
                    // Declare Most Consulted Doctor
                }
                if(option == 12)
                {
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);                }

            } while (option != 12);

        }


    }
}