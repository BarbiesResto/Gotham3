using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gotham3.domain
{
    public enum Status
    {
        Attente,
        Publiee
    }

    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}