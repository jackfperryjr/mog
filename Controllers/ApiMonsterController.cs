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
using Moogle.Data;
using Moogle.Models;

namespace Moogle.Controllers
{
    [Route("/api/v1/monsters")]
    public class ApiMonsterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiMonsterController(ApplicationDbContext context)
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
        public IActionResult Get(Guid? id)
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