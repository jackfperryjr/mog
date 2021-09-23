using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mog.Api.Core.Models;
using Mog.Api.Core.WebApi;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Extensions;

namespace Mog.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    [Obsolete]
    public class BlogController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Blog>, Guid> _blogFactory;
        private readonly IStore<Blog> _blogStore;
        private readonly IStore<object[]> _blogReactionStore;

        public BlogController(
            IFactory<IQueryable<Blog>, Guid> blogFactory,
            IStore<Blog> blogStore,
            IStore<object[]> blogReactionStore)
        {
            _blogFactory = blogFactory;
            _blogStore = blogStore;
            _blogReactionStore = blogReactionStore;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var blogs = await _blogFactory.GetAsync(id, cancellationToken);
                if (blogs.Any())
                {
                    return Ok(blogs);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "There are no blogs in the database."
                    });                
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
                var blog = await _blogFactory.GetByKeyAsync(id, cancellationToken);
                if (blog.Any())
                {
                    return Ok(blog);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "Couldn't find a blog with that id."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        } 

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Blog model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try 
            {
                var blog = await _blogStore.AddAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Blog record added successfully.",
                    blog = blog
                });
            }
            catch 
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] Blog model, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            var blog = await _blogFactory.GetByKeyAsync(id, cancellationToken);
            bool verify = false;

            if (blog.FirstOrDefault().Id == model.Id)
            {
                verify = true;
            }

            if (verify)
            {
                await _blogStore.UpdateAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Blog updated successfully.",
                    verified = verify,
                    blog = model
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> Like(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "like"};
            try
            {
                await _blogReactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("dislike/{id}")]
        public async Task<IActionResult> Dislike(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "dislike"};
            try
            {
                await _blogReactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("love/{id}")]
        public async Task<IActionResult> Love(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "love"};
            try
            {
                await _blogReactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            try 
            {
                var model = await _blogFactory.GetByKeyAsync(id, cancellationToken);
                await _blogStore.DeleteAsync(model.FirstOrDefault(), cancellationToken);
                return Ok(new
                {
                    message = "Blog records removed successfully."
                });
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}