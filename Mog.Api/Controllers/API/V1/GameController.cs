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
    public class GameController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Game>, Guid> _gameFactory;

        public GameController(
            IFactory<IQueryable<Game>, Guid> gameFactory)
        {
            _gameFactory = gameFactory;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var games = await _gameFactory.GetAsync(id, cancellationToken);
                return Ok(games);
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
                var game = await _gameFactory.GetByKeyAsync(id, cancellationToken);
                return Ok(game);
            }
            catch
            {
                return NotFound();
            }
        } 
    }
}