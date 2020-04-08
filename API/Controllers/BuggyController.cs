using System.Net;
using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            Product product = null;
            product.ToString();
            return Ok();
        }

        [HttpGet("maths")]
        public ActionResult GetMaths()
        {
            int divisor = 0;
            int quotient = 10 / divisor;
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest));
        }

        [HttpGet("validationerror/{id}")]
        public ActionResult GetValidationError(int id)
        {
            return Ok();
        }
    }
}