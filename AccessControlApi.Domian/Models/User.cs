using AccessControlApi.Domian.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlApi.Domian.Models
{
    [Table("users")]
    public class User : BaseModel, ISoftDeletable
    {

        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; } = false;
        [Column("first_login")]
        public bool FirstLogin { get; set; } = true;
        [Column("role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
