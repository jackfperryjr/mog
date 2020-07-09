using System;
using Microsoft.OpenApi.Models;

namespace Mog.Api.Core.Swagger
{
    public class SwaggerSettings
    {
        public SwaggerSettings()
        {
            Name = "MoogleApi";
            Info = new OpenApiInfo
            {
                Title = "MoogleApi",
                Description = "A simple web API for Final Fantasy characters, monsters, and games!",
                Contact = new OpenApiContact
                {
                    Name = "Jack F. Perry, Jr.",
                    Email = "jackfperryjr@gmail.com",
                    Url = new Uri("https://www.moogleapi.com")
                }
            };
        }

        public string Name { get; set; }
        public OpenApiInfo Info { get; set; }
    }
}