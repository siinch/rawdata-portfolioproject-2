namespace WebService.Models
{
    public class UpdateEmailDto
    {
        public string OldEmail { get; set; }
        public  string Password { get; set; }
        public string NewEmail { get; set; }
    }
}