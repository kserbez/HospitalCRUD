namespace Hospital.API.Entities
{
    public class Patient
    {
        public string AdmissionNumber { get; private set; } = null!;  // PK
        public string PatientName { get; private set; } = null!;

        // ?? todo
        public int? CurrentDepartmentId { get; private set; }
        public Department? CurrentDepartment { get; private set; }

        // 1 to many
        public ICollection<PatientDepartmentAssignment> Assignments { get; private set; }
            = new List<PatientDepartmentAssignment>();

        private Patient() { }

        public static Patient Create(string admissionNumber, string name)
        {
            return new Patient
            {
                AdmissionNumber = admissionNumber,
                PatientName = name
            };
        }
    }
}
