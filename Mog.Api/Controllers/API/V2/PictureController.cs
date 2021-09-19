using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mog.Api.Core.Models;
using Mog.Api.Core.WebApi;
using Mog.Api.Core.Abstractions;

namespace Mog.Api.Controllers.API.V2
{
    [ApiVersion("2")]
    public class PictureController : ApiControllerBase
    {
        private readonly IStore<Picture> _pictureStore;

        public PictureController(
            IStore<Picture> pictureStore)
        {
            _pictureStore = pictureStore;
        }

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Picture model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try
            {
                await _pictureStore.AddAsync(model, cancellationToken);
                return Ok(new 
                {
                    message = "Picture added successfully."
                });  
            }
            catch
            {
                return BadRequest(new
                {
                    message = "An error occurred processing the update."
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] Picture model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try
            {
                await _pictureStore.UpdateAsync(model, cancellationToken);
                return Ok(new 
                {
                    message = "Picture updated successfully."
                });  
            }
            catch
            {
                return BadRequest(new
                {
                    message = "An error occurred processing the update."
                });
            }
        }
    }
}