using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Emails.Commands.Handler
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEmailService _emailService;
        #endregion

        #region Ctor
        public EmailsCommandHandler(IStringLocalizer<SharedResources> localizer, IEmailService emailService) : base(localizer)
        {
            _localizer = localizer;
            _emailService = emailService;
        }
        #endregion

        #region Methods

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>(_localizer[SharedResourcesKeys.Success]);
            return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToSendEmail]);
        }
        #endregion
    }
}
