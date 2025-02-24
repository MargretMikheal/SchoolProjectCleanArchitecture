using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Students.Command.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
    {
        #region Fields
        public IStudentService _studentService;
        public IMapper _mapper;
        #endregion

        #region Ctor
        public StudentCommandHandler(IStudentService studentService , IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between request and Student
            var studentMapper= _mapper.Map<Student>(request);
            //add
            var result = await _studentService.AddAsync(studentMapper);
            //return response
            if(result == "Exists")
            {
                return UnprocessableEntity<string>();
            }
            else if(result == "Success")
            return Created<string>(result);
            else
            {
                return BadRequest<string>();
            }
        }
        #endregion

    }
}
