using Application.Validation;

namespace Application.Contracts.Requests.User
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }

        [StrongPasswrod]
        public string NewPassword { get; set; }

        [StrongPasswrod]
        public string ConfirmPassword { get; set; }
    }
}
