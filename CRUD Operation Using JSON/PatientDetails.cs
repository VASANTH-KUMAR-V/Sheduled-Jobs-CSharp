namespace PatientLibrary
{
    public abstract class PatientDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
