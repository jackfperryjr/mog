using System;  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Mog.Api.Core.Models  
{  
    public class DatingProfile
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Bio { get; set; }
        public string Age { get; set; } 
        public string Gender { get; set; }
        public ICollection<DatingResponse> Responses { get; set; }
        [JsonIgnore]
        public Guid CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }
    }  

    public class DatingResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public Guid Id { get; set ;}
        public string Response { get; set; }
        [JsonIgnore]
        public Guid DatingProfileId { get; set; }
        [ForeignKey("DatingProfileId")]
        public DatingProfile DatingProfile { get; set; }
    }
}