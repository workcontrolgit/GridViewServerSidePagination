namespace WebFormBoostrap.BusinessLayer.Models

{
    public class UserProfile
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string IsActive { get; set; }
    }
}