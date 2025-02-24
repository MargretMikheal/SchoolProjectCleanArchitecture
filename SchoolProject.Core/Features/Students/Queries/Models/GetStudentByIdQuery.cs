using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;


namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentResponse>>
    {
        #region fields
        public int Id { get; set; }

        #endregion

        #region Ctor
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
        #endregion
    }
}
 