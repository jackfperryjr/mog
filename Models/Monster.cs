
using System;  
using System.ComponentModel.DataAnnotations;  
using Newtonsoft.Json;

namespace Moogle.Models  
{  
    public class Monster
    {  
        public Guid MonsterId { get; set; }  
        [Required] 
        public string Name { get; set; }  
        public string JapaneseName { get; set; }  
        public string ElementalAffinity { get; set; }
        public string ElementalWeakness { get; set; }
        public int HitPoints{ get; set; }
        public int ManaPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Game { get; set; }
        [JsonIgnore]
        public string AddedBy { get; set; }
    }  
}
