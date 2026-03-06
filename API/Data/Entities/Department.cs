namespace API.Data.Entities;
public class Department
{
    public int Id { get; private set; }
    public string ShortName { get; private set; } = null!;
    public string LongName { get; private set; } = null!;

    private readonly List<PatientDepartmentAssignment> _patientDepartmentAssignments = new();

    public IReadOnlyCollection<PatientDepartmentAssignment> PatientDepartmentAssignments
        => _patientDepartmentAssignments.AsReadOnly();

    private Department() { }

    public static Department Create(string shortName, string longName)
    {
        return new Department { ShortName = shortName, LongName = longName };
    }

    public void ChangeShortName(string newShortName)
    {
        if (string.IsNullOrWhiteSpace(newShortName))
            throw new ArgumentException("ShortName cannot be empty");

        ShortName = newShortName;
    }

    public void ChangeLongName(string newLongName)
    {
        if (string.IsNullOrWhiteSpace(newLongName))
            throw new ArgumentException("ShortName cannot be empty");

        LongName = newLongName;
    }

}