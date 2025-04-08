using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Instructor.Command.Handler
{
    public class InstructorCommandHandler : ResponseHandler, IRequestHandler<AddInstructorCommand, Response<string>>
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorCommandHandler(IInstructorService instructorService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<SchoolProject.Data.Entities.Instructor>(request);

            var result = await _instructorService.AddInstructorAsync(instructor);

            if (result == "Success")
                return Success<string>("Instructor added successfully");
            else
                return BadRequest<string>("Failed to add instructor");
        }
    }
}
