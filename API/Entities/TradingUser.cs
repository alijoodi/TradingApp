using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("TradingUser")]
    public class TradingUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Family { get; set; }
        public string? MobileNumber { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required byte[] Password { get; set; }
        [Required]
        public required byte[] PasswordSalt { get; set; }
        [Required]
        public bool IsActive { get; set; }
        
    }
}