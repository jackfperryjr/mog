
using System;  
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;  
using System.Collections.Generic;
using System.ComponentModel;
using Mvc.Models;

namespace Mvc.Models  
{  
    public class Game
    {  
        public int GameId { get; set; }  
        [Required]  
        public string Title { get; set; } 
        public string Picture { get; set; } 
        public string Platform { get; set; }
        [DisplayName("Release Date")]
        public string ReleaseDate { get; set; } 
        public List<Characters> Characters { get; set; }
    }  
}
