using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Query.Models;
using SchoolProject.Core.Features.Departments.Query.Results;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Departments.Query.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public DepartmentQueryHandler(IDepartmentService departmentService
            , IStringLocalizer<SharedResources> localizer,
            IMapper mapper) : base(localizer)
        {
            _departmentService = departmentService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //service get department by id
            var response = await _departmentService.GetDepartmentByIdAsync(request.Id);
            //check if department is null
            if (response == null)
            {
                return NotFound<GetDepartmentByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }
            //mapping 
            var responseMapper = _mapper.Map<GetDepartmentByIdResponse>(response);
            //return response
            return Success(responseMapper);
        }
        #endregion
    }
}
