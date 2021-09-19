using System;  
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;

namespace Mog.Api.Core.Models  
{  
    public class Picture
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }  
        public string Url { get; set; }
        public int Primary { get; set; }
        public Guid CollectionId { get; set; }
        [ForeignKey("CollectionId")]
        public Character Character { get; set; }
    }  
}