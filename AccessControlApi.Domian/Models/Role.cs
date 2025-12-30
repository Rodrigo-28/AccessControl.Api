using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlApi.Domian.Models
{
    [Table("role")]
    public class Role
    {

        [Column("role_id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; } = false;
        public ICollection<User> Users { get; set; }
    }
}
