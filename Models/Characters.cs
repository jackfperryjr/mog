using System;  
using System.ComponentModel.DataAnnotations;  

namespace Moogle.Models  
{  
    public class Characters 
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
        public string Picture { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public string Picture4 { get; set; }
        public string Picture5 { get; set; }
        public string Response1 { get; set; }
        public string Response2 { get; set; }
        public string Response3 { get; set; }
        public string Response4 { get; set; }
        public string Response5 { get; set; }
        public string Response6 { get; set; }
        public string Response7 { get; set; }
        public string Response8 { get; set; }
        public string Response9 { get; set; }
        public string Response10 { get; set; }

    }  
}