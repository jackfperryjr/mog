using Newtonsoft.Json;
using System;

namespace Mog.Api.Core.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public string Wallpaper { get; set; }
        public string RoleName { get; set; }
        public DateTime JoinDate { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
    }
}
