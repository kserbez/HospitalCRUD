namespace Hospital.API.Entities
{
    public class PatientDepartmentAssignment
    {
        public int Id { get; private set; } // surrogate key

        public string PatientAdmissionNumber { get; private set; } = null!;
        public Patient Patient { get; private set; } = null!;

        public int DepartmentId { get; private set; }
        public Department Department { get; private set; } = null!;

        public DateOnly AssignmentDate { get; private set; }

        private PatientDepartmentAssignment() { }

        public static PatientDepartmentAssignment Create(
            Patient patient,
            Department department,
            DateOnly assignmentDate)
        {
            return new PatientDepartmentAssignment
            {
                PatientAdmissionNumber = patient.AdmissionNumber,
                Patient = patient,
                DepartmentId = department.Id,
                Department = department,
                AssignmentDate = assignmentDate
            };
        }
    }
}
