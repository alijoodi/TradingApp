using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.TradingUserDtos
{
    public class TradingUserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }
        public string? MobileNumber { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public bool IsActive { get; set; }

    }
}