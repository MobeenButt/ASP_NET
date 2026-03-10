using HMS_BO;
using HMS_DAL;

namespace HMS_BLL
{
    public class PatientBLL
    {
        private readonly PatientDAL patientDAL;
        private List<Patient_BO> patients;
        public PatientBLL()
        {
            patientDAL = new PatientDAL();
            patients = new List<Patient_BO>();
        }
        public void AddPatient(Patient_BO patient)
        {
            patientDAL.AddPatientToDatabase(patient);
            patients.Add(patient);
        }
        public void UpdatePatient(string cnic, string name)
        {
            patientDAL.UpdatePatientToDatabase(cnic, name);
        }
        public void DeletePatient(string cnic)
        {
            patientDAL.DeletePatientToDatabase(cnic);
        }
        public List<Patient_BO> DisplayPatient()
        {
            var list = patientDAL.DisplayPatient();
            return list;
        }
    }
}
