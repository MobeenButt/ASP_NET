namespace Identity_Authorization___AJAX_Interaction_in_MVC.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string? UserId { get; set; }
        public string? Status { get; set; }
        public DateTime RequestedOn { get; set; }

        public string? ResourceName { get; set; }
        public string? ResourceType { get; set; }
        public string? ResourceLocation { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
