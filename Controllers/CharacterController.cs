using System;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Mog.Data;
using Mog.Models;

namespace Mog.Controllers
{
    [Route("/api/v1/characters")]
    public class CharacterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _configuration;

        public CharacterController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET all api/characters
        [AllowAnonymous]
        [HttpGet]
        public List<Character> GetAll()
        {
            var characters = from c in _context.Characters select c;
            characters = characters.OrderBy(c => c.Origin).ThenBy(c => c.Name);
            return characters.ToList();
        }

        //GET api/characters/search?name=lightning
        //GET api/characters/search?gender=female
        //GET api/characters/search?job=l'cie
        //GET api/characters/search?race=human
        //GET api/characters/search?origin=13
        [AllowAnonymous]
        [HttpGet("search")]
        public List<Character> Search([FromQuery]string name, string gender, string job, string race, string origin) 
        { 
            var characters = from c in _context.Characters select c;

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
            return characters.ToList();
        }

        //GET api/characters/id
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(Guid? id)
        {
            var character = _context.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }     

        //GET api/characters/random
        [AllowAnonymous]
        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            IQueryable<Character> characters = from c in _context.Characters select c;
            IList<Character> characterList = characters.ToList();

            Random rand = new Random();
            var character = characterList[rand.Next(characterList.Count)];

            return Ok(character);
        }  

        //POST api/characters/add
        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<IActionResult> AddCharacter([FromBody] Character character)
        {
            var container = ApplicationHelper.ConfigureBlobContainer(
                        _configuration["StorageConfig:AccountName"], 
                        _configuration["StorageConfig:AccountKey"]); 
            await container.CreateIfNotExistsAsync();
            var jwt = Request.Headers["Authorization"].ToString();
            var isToken = ApplicationHelper.CheckForToken(jwt);

            if (isToken)
            {
                jwt = jwt.Replace("Bearer ", string.Empty);
            }
            else 
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "No token provided."
                });
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(jwt);    
            var issuer = _configuration.GetValue<string>("Issuer");        

            if (ApplicationHelper.VerifyToken(token, issuer))
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                
                if (HttpContext.Request.ContentType == "multipart/form-data") 
                { 
                    var characterFromDb = _context.Characters.Find(character.Id);
                    var files = HttpContext.Request.Form.Files;

                    if (files.Count != 0) 
                    {
                        for (var i = 0; i < files.Count; i++) {
                            var extension = Path.GetExtension(files[i].FileName);
                            var newBlob = container.GetBlockBlobReference("Character-" + character.Id + (i + 1).ToString() + extension);

                            using (var filestream = new MemoryStream())
                            {
                                files[i].CopyTo(filestream);
                                filestream.Position = 0;
                                await newBlob.UploadFromStreamAsync(filestream);
                            }
                            if (i == 0) 
                            {
                                characterFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Character-" + character.Id + (i + 1).ToString() + extension;
                            }
                            if (i == 1) 
                            {
                                characterFromDb.Picture2 = "https://mooglestorage.blob.core.windows.net/images/Character-" + character.Id + (i + 1).ToString() + extension;
                            }
                            if (i == 2) 
                            {
                                characterFromDb.Picture3 = "https://mooglestorage.blob.core.windows.net/images/Character-" + character.Id + (i + 1).ToString() + extension;
                            }
                            if (i == 3) 
                            {
                                characterFromDb.Picture4 = "https://mooglestorage.blob.core.windows.net/images/Character-" + character.Id + (i + 1).ToString() + extension;
                            }
                            if (i == 4) 
                            {
                                characterFromDb.Picture5 = "https://mooglestorage.blob.core.windows.net/images/Character-" + character.Id + (i + 1).ToString() + extension;
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                
                return Ok(new
                {
                    status = 200,
                    message = "Character added."
                });
            }
            else 
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Invalid token or no token provided."
                });
            }
        } 

        //DELETE api/characters/delete
        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCharacter(Guid? id)
        {
            var jwt = Request.Headers["Authorization"].ToString();
            var isToken = ApplicationHelper.CheckForToken(jwt);

            if (isToken)
            {
                jwt = jwt.Replace("Bearer ", string.Empty);
            }
            else 
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "No token provided."
                });
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(jwt);    
            var issuer = _configuration.GetValue<string>("Issuer");        

            if (ApplicationHelper.VerifyToken(token, issuer))
            {
                var character = _context.Characters.Find(id);

                if (character == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Character could not be found."
                    });
                }
                else 
                {
                    _context.Characters.Remove(character);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        status = 200,
                        message = "Character deleted."
                    });
                }
            }
            else 
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Invalid token or no token provided."
                });
            }
        }
    }
}