using System;

namespace CRUD_Operation_Using_JSON
{
    public abstract class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
