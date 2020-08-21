using System;  
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Mog.Api.Core.Models  
{      
    
    public class Stat
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }  
        public string Platform { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public int HitPoints { get; set; }
        public int ManaPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int MagicDefense { get; set; }
        public int Agility { get; set; }
        public int Spirit { get; set; }
        [JsonIgnore]
        public Guid CollectionId { get; set; }
        [ForeignKey("CollectionId")]
        public Character Character { get; set; }
    } 
}