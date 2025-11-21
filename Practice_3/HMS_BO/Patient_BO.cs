namespace HMS_BO
{
    public class Patient_BO
    {
        private string? name;
        private string? cnic;
        private List<int> appointments;
        public Patient_BO(string name, string cnic)
        {
            this.name = name;
            this.cnic = cnic;
        }
        public string CNIC { get => cnic; }
        public string NAME { get => name; }
        public bool hasAppointment(int appointmentId)
        {
            return appointments.Contains(appointmentId);
        }

    }
}
