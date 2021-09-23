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
    public class MonsterController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Monster>, Guid> _monsterFactory;

        public MonsterController(
            IFactory<IQueryable<Monster>, Guid> monsterFactory)
        {
            _monsterFactory = monsterFactory;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var monsters = await _monsterFactory.GetAsync(id, cancellationToken);
                return Ok(monsters);
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var monster = await _monsterFactory.GetByKeyAsync(id, cancellationToken);
                return Ok(monster);
            }
            catch
            {
                return BadRequest();
            }
        } 

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string name, CancellationToken cancellationToken = new CancellationToken()) 
        { 
            Guid id = Guid.NewGuid();

            try
            {
                var monsters = await _monsterFactory.GetAsync(id, cancellationToken);
                if (name != null) {
                    monsters = monsters.OrderBy(c => c.Name).Where(c => c.Name.Contains(name));
                }

                return Ok(monsters);
            }
            catch
            {
                return BadRequest();
            }
        }     
    }
}