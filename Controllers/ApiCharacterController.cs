using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Moogle.Data;
using Moogle.Models;

namespace Moogle.Controllers
{
    [Route("/api/v1/characters")]
    public class ApiCharacterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiCharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET all api/characters
        [AllowAnonymous]
        [HttpGet]
        public List<Characters> GetAll()
        {
            var characters = from c in _context.Character select c;
            characters = characters.OrderBy(c => c.Origin).ThenBy(c => c.Name);
            return characters.ToList();
        }

        //GET api/characters/id
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(Guid? id)
        {
            var character = _context.Character.Find(id);
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
            IQueryable<Characters> characters = from c in _context.Character select c;
            IList<Characters> characterList = characters.ToList();

            Random rand = new Random();
            var character = characterList[rand.Next(characterList.Count)];

            return Ok(character);
        }  
    }
}