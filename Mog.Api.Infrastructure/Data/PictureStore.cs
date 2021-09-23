using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Extensions;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class PictureStore : IStore<Picture>
    {
        private SerahDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PictureStore(
            SerahDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Picture> AddAsync(Picture model, CancellationToken cancellationToken = new CancellationToken())
        {
            //This container is used for blob storage uploads.
            var container = ApplicationExtensions.ConfigureBlobContainer(
                                    _configuration["AzureStorageConfig:AccountName"], 
                                    _configuration["AzureStorageConfig:AccountKey"]); 
            await container.CreateIfNotExistsAsync();

            _context.Add(model);
            _context.SaveChanges();
            var picture = _context.Pictures.Find(model.Id);
            var files = _httpContextAccessor.HttpContext.Request.Form.Files;

            if (files.Count != 0) 
            {
                for (var i = 0; i < files.Count; i++)
                {
                    if (files[i].Name == "photo")
                    {
                        var newBlob = container.GetBlockBlobReference(picture.Id + ".jpg");

                        using (var filestream = new MemoryStream())
                        {   
                            files[i].CopyTo(filestream);
                            filestream.Position = 0;
                            await newBlob.UploadFromStreamAsync(filestream);
                        }

                        picture.Url = "https://mooglestorage.blob.core.windows.net/images/" + picture.Id + ".jpg";
                    }
                }
            }

            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("sub")?.Value;

            var user = await ApplicationExtensions.Get<User>(userName);
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = character.Name;
            feed.CharacterId = character.Id;
            feed.TimeStamp = DateTime.Now;
            feed.PhotoUpdate = 1;
            _context.Add(feed);
            _context.SaveChanges();
            return picture;
        }

        public async Task<Picture> UpdateAsync(Picture model, CancellationToken cancellationToken = new CancellationToken())
        {
            //This container is used for blob storage uploads.
            var container = ApplicationExtensions.ConfigureBlobContainer(
                                    _configuration["AzureStorageConfig:AccountName"], 
                                    _configuration["AzureStorageConfig:AccountKey"]); 
            await container.CreateIfNotExistsAsync();

            _context.Pictures.RemoveRange(_context.Pictures.Where(x => x.CollectionId == model.CollectionId));
            _context.SaveChanges();
            model.Id = Guid.NewGuid();
            _context.Add(model);
            _context.SaveChanges();
            var picture = _context.Pictures.Find(model.Id);
            var files = _httpContextAccessor.HttpContext.Request.Form.Files;

            if (files.Count != 0) 
            {
                for (var i = 0; i < files.Count; i++)
                {
                    if (files[i].Name == "photo")
                    {
                        var newBlob = container.GetBlockBlobReference(picture.Id + ".jpg");

                        using (var filestream = new MemoryStream())
                        {   
                            files[i].CopyTo(filestream);
                            filestream.Position = 0;
                            await newBlob.UploadFromStreamAsync(filestream);
                        }

                        picture.Url = "https://mooglestorage.blob.core.windows.net/images/" + picture.Id + ".jpg";
                    }
                }
            }

            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("sub")?.Value;

            var user = await ApplicationExtensions.Get<User>(userName);
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.CollectionId);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = character.Name;
            feed.CharacterId = character.Id;
            feed.TimeStamp = DateTime.Now;
            feed.PhotoUpdate = 1;
            _context.Add(feed);
            _context.SaveChanges();
            return model;
        }

        public async Task<Picture> DeleteAsync(Picture model, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}