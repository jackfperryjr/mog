namespace Mog.Models
{
    public class ApplicationUserViewModel  
    {  
        public string UserId { get; set; }  
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        
        public string Picture { get; set; }
        public string Email { get; set; }  
        public string EmailConfirmed { get; set; }
        public ApplicationUserRole Role { get; set; }
    }
}
