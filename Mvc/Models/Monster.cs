
using System;  
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;  
using System.Collections.Generic;

namespace Mvc.Models  
{  
    public class Monster
    {  
        public int MonsterId { get; set; }  
        [Required]  
        public string Name { get; set; }  
        public string Strength { get; set; }
        public string Weakness { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }

        // { More monster properties } 
    }  
}
