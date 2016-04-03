using System;

namespace TF.Data.Business
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Elevation { get; set; }
    }
}
