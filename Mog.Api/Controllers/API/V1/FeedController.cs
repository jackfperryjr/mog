using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mog.Api.Core.Models;
using Mog.Api.Core.WebApi;
using Mog.Api.Core.Abstractions;

namespace Mog.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    public class FeedController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Feed>, Guid> _feedFactory;

        public FeedController(
            IFactory<IQueryable<Feed>, Guid> feedFactory)
        {
            _feedFactory = feedFactory;
        }

        [Obsolete]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try
            {
                var feed = await _feedFactory.GetAsync(id, cancellationToken);
                if (feed.Any())
                {
                    return Ok(feed);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "There is no data in the feed."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}