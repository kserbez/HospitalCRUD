namespace API.Data.Entities
{
    public class Patient
    {
        public string AdmissionNumber { get; private set; } = null!;  // PK
        public string PatientName { get; set; } = null!;

        // ?? todo
        public int? CurrentDepartmentId { get; set; }
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
