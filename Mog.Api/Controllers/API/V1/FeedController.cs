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
        private readonly IStore<object[]> _feedStore;

        public FeedController(
            IFactory<IQueryable<Feed>, Guid> feedFactory,
            IStore<object[]> feedStore)
        {
            _feedFactory = feedFactory;
            _feedStore = feedStore;
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

        [Obsolete]
        [HttpPut("like/{id}")]
        public async Task<IActionResult> Like(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "like"};
            try
            {
                await _feedStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Obsolete]
        [HttpPut("dislike/{id}")]
        public async Task<IActionResult> Dislike(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "dislike"};
            try
            {
                await _feedStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Obsolete]
        [HttpPut("love/{id}")]
        public async Task<IActionResult> Love(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "love"};
            try
            {
                await _feedStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}