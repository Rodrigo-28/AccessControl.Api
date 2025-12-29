using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlApi.Domian.Models
{
    [Table("role")]
    public class Role : BaseModel
    {


        [Column("name")]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
