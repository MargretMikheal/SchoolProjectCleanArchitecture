using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Students.Command.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
                                                            , IRequestHandler<EditStudentCommand, Response<string>>
                                                            , IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        public IStudentService _studentService;
        public IMapper _mapper;
        #endregion

        #region Ctor
        public StudentCommandHandler(IStudentService studentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between request and Student
            var studentMapper = _mapper.Map<Student>(request);
            //add
            var result = await _studentService.AddAsync(studentMapper);

            if (result == "Success")
                return Created<string>("");
            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //check if the student exists
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>();
            }
            //mapping Between request and Student
            var studentMapper = _mapper.Map(request, student);
            //edit
            var result = await _studentService.EditAsync(studentMapper);
            //return response
            if (result == "Success")
                return Success<string>("Student updated successfully", studentMapper);
            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //check if the student exists
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return await Task.FromResult(NotFound<string>());
            }
            //delete
            var result = await _studentService.DeleteAsync(student);
            if (!result)
            {
                return await Task.FromResult(BadRequest<string>());
            }
            //return response
            return await Task.FromResult(Deleted<string>());
        }
        #endregion

    }
}
