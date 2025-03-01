using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstract;
using System.Linq.Expressions;


namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
            IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
            IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
            IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer)
            : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Methods 
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);

            return Success(studentListMapper, new { TotalCount = studentList.Count });
        }

        public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<GetStudentResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var studentMapper = _mapper.Map<GetStudentResponse>(student);
            return Success(studentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = x => new GetStudentPaginatedListResponse
            (
                x.StudID,
                x.Localize(x.NameAr, x.NameEn),
                x.Address,
                x.Department != null ? x.Department.Localize(x.Department.DNameAr, x.Department.DNameEn) : null
            );

            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.TotalCount };
            return paginatedList;
        }
        #endregion
    }
}
