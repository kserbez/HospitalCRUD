namespace API.Data.Entities;
public class Department
{
    public int Id { get; private set; }
    public string ShortName { get; set; } = null!;
    public string LongName { get; set; } = null!;

    private readonly List<PatientDepartmentAssignment> _assignments = new();

    public IReadOnlyCollection<PatientDepartmentAssignment> PatientDepartmentAssignments
        => _assignments.AsReadOnly();

    private Department() { }

    public static Department Create(string shortName, string longName)
    {
        return new Department { ShortName = shortName, LongName = longName };
    }
}