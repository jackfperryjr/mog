using System;  
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
        public string Picture { get; set; }
        [JsonIgnore] 
        public string Picture2 { get; set; }
        [JsonIgnore]
        public string Picture3 { get; set; }
        [JsonIgnore]
        public string Picture4 { get; set; }
        [JsonIgnore]
        public string Picture5 { get; set; }
        [JsonIgnore]
        public string Response1 { get; set; }
        [JsonIgnore]
        public string Response2 { get; set; }
        [JsonIgnore]
        public string Response3 { get; set; }
        [JsonIgnore]
        public string Response4 { get; set; }
        [JsonIgnore]
        public string Response5 { get; set; }
        [JsonIgnore]
        public string Response6 { get; set; }
        [JsonIgnore]
        public string Response7 { get; set; }
        [JsonIgnore]
        public string Response8 { get; set; }
        [JsonIgnore]
        public string Response9 { get; set; }
        [JsonIgnore]
        public string Response10 { get; set; }
    }  
}