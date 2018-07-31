
using System;  
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;  
using System.Collections.Generic;

namespace Moogle.Models  
{  
    public class Monster
    {  
        public Guid MonsterId { get; set; }  
        [Required]  
        public string Name { get; set; }  
        public string Strength { get; set; }
        public string Weakness { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }  
}
