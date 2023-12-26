namespace AzureFunWithDapper.Entity
{
    public class NasherDetails
    {
        public int NasherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Department { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string GitURL { get; set; }
        public string LinkedInURL { get; set; }
    }
}
