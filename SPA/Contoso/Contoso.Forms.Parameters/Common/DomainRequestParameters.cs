using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Common
{
    public class DomainRequestParameters
    {
        public DomainRequestParameters()
        {
        }

        public DomainRequestParameters
        (
            [Comments("Defines the request state including the sort, filter, page and page size.")]
            DataRequestStateParameters state,

            [Comments("URL and other meta data for the data request.")]
            RequestDetailsParameters requestDetails
        )
        {
            State = state;
            RequestDetails = requestDetails;
        }

        public DataRequestStateParameters State { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
    }
}