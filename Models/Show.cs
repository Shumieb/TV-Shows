namespace TV_Shows.Models
{
    public class Show
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }
        public int PlatformID { get; set; }
        public Platform? Platform { get; set; }
        public string? Note { get; set; }
        public bool Like { get; set; } = false;
    }
}
