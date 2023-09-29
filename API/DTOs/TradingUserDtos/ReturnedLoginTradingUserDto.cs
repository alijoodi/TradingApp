using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.TradingUserDtos
{
    public class ReturnedLoginTradingUserDto
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}