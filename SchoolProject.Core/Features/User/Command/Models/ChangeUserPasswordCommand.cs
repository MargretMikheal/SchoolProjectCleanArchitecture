using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.User.Command.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        #region Fields
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
        #endregion
        #region Ctor
        public ChangeUserPasswordCommand(string userId, string currentPassword, string newPassword, string confirmedNewPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmedNewPassword = confirmedNewPassword;
        }
        #endregion

    }
}
