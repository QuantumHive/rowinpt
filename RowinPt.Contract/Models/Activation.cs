using System;

namespace RowinPt.Contract.Models
{
    public class Activation
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
