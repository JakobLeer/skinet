using System.Collections.Generic;
using System.Net;

namespace API.Errors
{
    public class ApiValidationError : ApiResponse
    {
        public ApiValidationError() : base((int)HttpStatusCode.BadRequest)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}