using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Extensions;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class BlogStore : IStore<Blog>
    {
        private AsheDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogStore(
            AsheDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Blog> AddAsync(Blog model, CancellationToken cancellationToken = new CancellationToken())
        {
            model.Created = DateTimeOffset.Now;
            _context.Add(model);
            _context.SaveChanges();
            var blog = _context.Blogs.Find(model.Id);

            return blog;
        }

        public async Task<Blog> UpdateAsync(Blog model, CancellationToken cancellationToken = new CancellationToken())
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == model.Id);

            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.Updated = DateTimeOffset.Now;
            _context.SaveChanges();
            return model;
        }

        public async Task<Blog> DeleteAsync(Blog model, CancellationToken cancellationToken = new CancellationToken())
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == model.Id);

            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return model;
        }
    }
}