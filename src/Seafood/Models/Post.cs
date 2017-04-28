using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seafood.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}