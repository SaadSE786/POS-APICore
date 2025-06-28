namespace POS_API.BusinessObjects
{
    public class ResetPassword
    {
        public int userId { get; set; }
        public string? newPassword { get; set; }
    }
}
