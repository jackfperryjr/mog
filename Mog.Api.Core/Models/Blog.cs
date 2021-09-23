using System;  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using Newtonsoft.Json;

namespace Mog.Api.Core.Models  
{  
    public class Blog
    {  
        public Guid Id { get; set; }
        [Required]  
        public string Content { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Love { get; set; }
        public DateTimeOffset Created { get; set;}
    }  
}