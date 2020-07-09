using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Mog.Api.Core.Models;
using Mog.Api.Core.WebApi;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Extensions;

namespace Mog.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    public class CharacterController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Character>, Guid> _characterFactory;

        public CharacterController(
            IFactory<IQueryable<Character>, Guid> characterFactory)
        {
            _characterFactory = characterFactory;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var characters = await _characterFactory.GetAsync(id, cancellationToken);
                return Ok(characters);
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
                var character = await _characterFactory.GetByKeyAsync(id, cancellationToken);
                return Ok(character);
            }
            catch
            {
                return NotFound();
            }
        } 

        [AllowAnonymous]
        [HttpGet("random")]
        public async Task<IActionResult> Random(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var characters = await _characterFactory.GetAsync(id, cancellationToken);
                var character = (from c in characters orderby Guid.NewGuid() select c).First();
                return Ok(character);
            }
            catch
            {
                return BadRequest();
            }
        } 

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string name, string gender, string job, string race, string origin, CancellationToken cancellationToken = new CancellationToken()) 
        { 
            Guid id = Guid.NewGuid();

            try
            {
                var characters = await _characterFactory.GetAsync(id, cancellationToken);

                if (name != null) {
                    characters = characters.OrderBy(c => c.Name).Where(c => c.Name.Contains(name));
                }
                if (gender != null) {
                    characters = characters.OrderBy(c => c.Name).Where(c => c.Gender == gender);
                }
                if (job != null) {
                    characters = characters.OrderBy(c => c.Name).Where(c => c.Job.Contains(job));
                }
                if (race != null) {
                    characters = characters.OrderBy(c => c.Name).Where(c => c.Race.Contains(race));
                }
                if (origin != null) {
                    characters = characters.OrderBy(c => c.Name).Where(c => c.Origin.Contains(origin));
                }

                return Ok(characters);
            }
            catch
            {
                return BadRequest();
            }
        }     
    }
}