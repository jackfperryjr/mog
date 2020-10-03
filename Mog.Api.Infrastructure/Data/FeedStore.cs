using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Extensions;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class FeedStore : IStore<object[]>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedStore(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object[]> AddAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }

        public async Task<object[]> UpdateAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            var feed = await _context.Feed.FirstOrDefaultAsync(x => x.Id == new Guid(reaction[0].ToString()));

            if (reaction[1].ToString() == "like")
            {
                feed.Like++;
            }

            if (reaction[1].ToString() == "dislike")
            {
                feed.Dislike++;
            }

            if (reaction[1].ToString() == "love")
            {
                feed.Love++;
            }

            _context.SaveChanges();
            return reaction;
        }

        public async Task<object[]> DeleteAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }
    }
}