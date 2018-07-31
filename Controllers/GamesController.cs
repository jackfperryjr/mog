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
using Moogle.Models;

namespace Moogle.Controllers
{
    [Route("/api/games")]
    public class GamesController : ControllerBase
    {
        private readonly CharacterContext _context;

        public GamesController(CharacterContext context)
        {
            _context = context;
        }

        //GET all api/games
        [AllowAnonymous]
        [HttpGet]
        public List<Game> GetAll()
        {
            var games = from g in _context.Games select g;
            games = games.OrderBy(g => g.Title);
            return games.ToList();
        }

        //GET api/games/id
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult Get(Guid? id)
        {
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }   
    }
}