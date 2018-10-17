using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Moogle.Models;

namespace Moogle.Controllers
{
    [Route("/api/characters")]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterContext _context;

        public CharactersController(CharacterContext context)
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
        [HttpGet("{name}", Name = "GetCharacter")]
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