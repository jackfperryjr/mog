
using System;
using System.ComponentModel.DataAnnotations;

namespace Moogle.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}