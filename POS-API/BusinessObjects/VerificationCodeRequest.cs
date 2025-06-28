namespace POS_API.BusinessObjects
{
    public class VerificationCodeRequest
    {
        public string? code { get; set; }
        public string? email { get; set; }
    }
}
