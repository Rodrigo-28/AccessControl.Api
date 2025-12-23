using System.ComponentModel.DataAnnotations.Schema;

namespace AccessControlApi.Domian.Models
{
    [Table("users")]
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
