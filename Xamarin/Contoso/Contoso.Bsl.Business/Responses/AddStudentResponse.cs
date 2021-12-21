using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Responses
{
    public class AddStudentResponse : BaseResponse
    {
        public StudentModel Student { get; set; }
    }
}
