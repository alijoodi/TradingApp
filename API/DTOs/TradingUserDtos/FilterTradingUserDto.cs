using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.TradingUserDtos
{
    public class FilterTradingUserDto
    {
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}