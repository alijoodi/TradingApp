using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        public required byte[] Password { get; set; }
        public required byte[] PasswordSalt { get; set; }
    }

}