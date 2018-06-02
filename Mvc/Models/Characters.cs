using System;  
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;  

namespace Mvc.Models  
{  
    public class Characters  
    {  
        public int Id { get; set; }  
        [Required]  
        public string Name { get; set; }  
        [Required]  
        public string Age { get; set; } 
        [Required]
        public string Gender { get; set; }
        [Required]  
        public string Race { get; set; }  
        [Required]  
        public string Job { get; set; } 
        public string Height { get; set; }     
        public string Weight { get; set; } 
        [Required]  
        public string Origin { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }  
}