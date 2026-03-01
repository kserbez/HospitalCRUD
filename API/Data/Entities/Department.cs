namespace API.Data.Entities
{
    public class Department
    {
        public int Id { get; private set; }
        public string ShortName { get; private set; } = null!;
        public string LongName { get; private set; } = null!;

        private Department() { }

        public static Department Create(string shortName, string longName)
        {
            return new Department { ShortName = shortName, LongName = longName };
        }
    }
}
