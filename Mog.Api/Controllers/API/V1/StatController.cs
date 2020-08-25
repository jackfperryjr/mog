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
    public class StatController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Stat>, Guid> _statFactory;
        private readonly IStore<Stat> _statStore;

        public StatController(
            IFactory<IQueryable<Stat>, Guid> statFactory,
            IStore<Stat> statStore)
        {
            _statFactory = statFactory;
            _statStore = statStore;
        }

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Stat model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try 
            {
                await _statStore.AddAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Stat record added successfully."
                });
            }
            catch 
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] Stat model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            var stat = await _statFactory.GetByKeyAsync(id, cancellationToken);
            bool verify = false;

            if (stat.FirstOrDefault().Id == model.Id)
            {
                verify = true;
            }

            if (verify)
            {
                await _statStore.UpdateAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Stats updated successfully.",
                    verified = verify,
                    stat = model
                });
            }
            else 
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try 
            {
                var model = await _statFactory.GetByKeyAsync(id, cancellationToken);
                await _statStore.DeleteAsync(model.FirstOrDefault(), cancellationToken);
                return Ok(new
                {
                    message = "Stat record removed successfully."
                });
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}