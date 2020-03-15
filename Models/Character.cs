using System;  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using Newtonsoft.Json;

namespace Mog.Models  
{  
    public class Character
    {  
        public Guid Id { get; set; }
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
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Stat> Stats { get; set; }
        public DatingProfile DatingProfile { get; set; }
    }  
}