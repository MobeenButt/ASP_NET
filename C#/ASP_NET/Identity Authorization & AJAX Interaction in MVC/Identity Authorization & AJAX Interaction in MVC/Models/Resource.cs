namespace Identity_Authorization___AJAX_Interaction_in_MVC.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } = null;
        public string? Type { get; set; }
        public string? Location { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
     
    }
}
