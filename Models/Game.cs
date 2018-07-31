
using System;  
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;  
using System.Collections.Generic;
using System.ComponentModel;
using Moogle.Models;

namespace Moogle.Models  
{  
    public class Game
    {  
        public Guid GameId { get; set; }  
        [Required]  
        public string Title { get; set; } 
        public string Picture { get; set; } 
        public string Platform { get; set; }
        [DisplayName("Release Date")]
        public string ReleaseDate { get; set; } 
        public string Description { get; set; }
    }  
}
