namespace Domain
{
    public class Location : Base
    {
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
    }
}
