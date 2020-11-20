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
    public class CharacterController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Character>, Guid> _characterFactory;
        private readonly IStore<Character> _characterStore;

        public CharacterController(
            IFactory<IQueryable<Character>, Guid> characterFactory,
            IStore<Character> characterStore)
        {
            _characterFactory = characterFactory;
            _characterStore = characterStore;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var characters = await _characterFactory.GetAsync(id, cancellationToken);
                if (characters.Any())
                {
                    return Ok(characters);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "There are no characters in the database."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var characters = await _characterFactory.GetAsync(id, cancellationToken);
                if (characters.Any())
                {
                    return Ok(characters.Count());
                }
                else 
                {
                    return Ok(0);       
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
                var character = await _characterFactory.GetByKeyAsync(id, cancellationToken);
                if (character.Any())
                {
                    return Ok(character);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "Couldn't find a character with that id."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        } 

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

                if (characters.Any())
                {
                    return Ok(characters);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "Couldn't find that character."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        }  

        [Authorize(Roles = "Admin")]
        [Obsolete]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Character model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try 
            {
                var character = await _characterStore.AddAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Character record added successfully.",
                    character = character
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
        public async Task<IActionResult> Update(Guid id, [FromForm] Character model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            var character = await _characterFactory.GetByKeyAsync(id, cancellationToken);
            bool verify = false;

            if (character.FirstOrDefault().Id == model.Id)
            {
                verify = true;
            }

            if (verify)
            {
                await _characterStore.UpdateAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Character updated successfully.",
                    verified = verify,
                    character = model
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
                var model = await _characterFactory.GetByKeyAsync(id, cancellationToken);
                await _characterStore.DeleteAsync(model.FirstOrDefault(), cancellationToken);
                return Ok(new
                {
                    message = "Character records removed successfully."
                });
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}