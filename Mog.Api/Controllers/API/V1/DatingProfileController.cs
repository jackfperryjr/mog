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
    [Authorize(Roles = "Admin")]
    [Obsolete]
    public class DatingProfileController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<DatingProfile>, Guid> _datingProfileFactory;

        public DatingProfileController(
            IFactory<IQueryable<DatingProfile>, Guid> datingProfileFactory)
        {
            _datingProfileFactory = datingProfileFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var datingProfiles = await _datingProfileFactory.GetAsync(id, cancellationToken);
                if (datingProfiles.Any())
                {
                    return Ok(datingProfiles);
                }
                else
                {
                    return Ok(new
                    {
                        message = "There are no profiles yet."
                    });
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var datingProfile = await _datingProfileFactory.GetByKeyAsync(id, cancellationToken);
                if (datingProfile.Any())
                {
                    return Ok(datingProfile);
                }
                else
                {
                    return Ok(new
                    {
                        message = "There is no profile associated with this id."
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