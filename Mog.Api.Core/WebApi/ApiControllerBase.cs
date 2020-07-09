using Microsoft.AspNetCore.Mvc;

namespace Mog.Api.Core.WebApi
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]s")]
    public abstract class ApiControllerBase : ControllerBase
    { }
}