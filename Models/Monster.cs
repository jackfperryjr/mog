
using System;  
using System.ComponentModel.DataAnnotations;  

namespace Moogle.Models  
{  
    public class Monster
    {  
        public Guid MonsterId { get; set; }  
        [Required] 
        public string Name { get; set; }  
        public string Strength { get; set; }
        public string Weakness { get; set; }
        // public string ElementalAffinity { get; set; }
        // public string ElementalWeakness { get; set; }
        // public int Hp{ get; set; }
        // public int Mp { get; set; }
        // public int Attack { get; set; }
        // public int Defense { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        // public string Game { get; set; }
        // public string AddedBy { get; set; }
    }  
}
