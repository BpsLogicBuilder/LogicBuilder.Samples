using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Requests
{
    public class AddStudentRequest : BaseRequest
    {
        public StudentModel Student { get; set; }
    }
}
