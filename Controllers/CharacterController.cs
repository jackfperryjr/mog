using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<Character> characters = _context.Characters
                                                .Include(c => c.Pictures)
                                                .Include(c => c.Stats)
                                                .Include(c => c.DatingProfile)
                                                .ThenInclude(c => c.Responses);
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
            IQueryable<Character> characters = _context.Characters
                                                .Include(c => c.Pictures)
                                                .Include(c => c.Stats)
                                                .Include(c => c.DatingProfile)
                                                .ThenInclude(c => c.Responses);

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
            IQueryable<Character> character = _context.Characters
                                                .Include(c => c.Pictures)
                                                .Include(c => c.Stats)
                                                .Include(c => c.DatingProfile)
                                                .ThenInclude(c => c.Responses)
                                                .Where(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }     

        //GET api/characters/random
        [AllowAnonymous]
        [HttpGet("random", Name = "GetRandomCharacter")]
        public IActionResult GetRandom()
        {
            IList<Character> characters = _context.Characters
                                            .Include(c => c.Pictures)
                                            .Include(c => c.Stats)
                                            .Include(c => c.DatingProfile)
                                            .ThenInclude(c => c.Responses)
                                            .ToList();

            Random rand = new Random();
            var character = characters[rand.Next(characters.Count)];

            return Ok(character);
        }  
    }
}