using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Add([FromBody] Character character)
        {
            return Ok(new
                    {
                        status = 200,
                        message = "This endpoint does nothing for now."
                    });
        } 
    }
}