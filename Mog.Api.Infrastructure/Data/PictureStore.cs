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
    public class PictureStore : IStore<Picture>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PictureStore(
            ApplicationDbContext context,
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

            _context.SaveChanges();
            return model;
        }

        public async Task<Picture> DeleteAsync(Picture model, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}