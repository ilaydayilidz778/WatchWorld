namespace ApplicationCore.Entities
{
    public class Address
    {
        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? State { get; set; }

        public string Country { get; set; } = null!;

        public string ZipCode { get; set; } = null!;
    }
}
