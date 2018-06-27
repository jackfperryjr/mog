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
    [Route("/Api/[controller]")]
    public class MonstersController : ControllerBase
    {
        private readonly CharacterContext _context;

        public MonstersController(CharacterContext context)
        {
            _context = context;
        }

        //GET all api/monsters
        [AllowAnonymous]
        [HttpGet]
        public List<Monster> GetAll()
        {
            var monsters = from m in _context.Monsters select m;
            monsters = monsters.OrderBy(m => m.Name);
            return monsters.ToList();
        }

        //GET api/monsters/id
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetMonster")]
        public IActionResult Get(int? id)
        {
            var monster = _context.Monsters.Find(id);
            if (monster == null)
            {
                return NotFound();
            }
            return Ok(monster);
        }   
    }
}