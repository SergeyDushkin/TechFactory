using System;

namespace TF.Data.Business
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Midname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Guid? UserGuid { get; set; }

        public Guid? BatchGuid { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean Deleted { get; set; }
    }
}
