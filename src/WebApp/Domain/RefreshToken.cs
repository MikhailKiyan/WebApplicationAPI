using System;

using WebApplicationAPI.Domain.Identity;

namespace WebApplicationAPI.Domain {
    public class RefreshToken {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
