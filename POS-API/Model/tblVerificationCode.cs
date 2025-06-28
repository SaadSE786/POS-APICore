using POS_API.Models;

namespace POS_API.Model
{
    public class tblVerificationCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual tblUser User { get; set; } = null!;
    }
}
