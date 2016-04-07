using System;

namespace TF.Data.Business
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid PersonId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual Person Person { get; set; }
    }
}
