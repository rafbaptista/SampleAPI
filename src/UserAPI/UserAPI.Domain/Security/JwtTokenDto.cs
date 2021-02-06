using System;

namespace UserAPI.Domain.Security
{
    public class JwtTokenDto
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
