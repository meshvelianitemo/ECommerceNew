namespace ECommerceNew.Domain.Entities.UserSide
{
    public class EmailVerification
    {
        public int VerificationId { get; set; }
        public string VerificationCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
        public bool IsVerified { get; set; }
    }
}