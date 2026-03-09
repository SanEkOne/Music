using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Salt { get; set; }
    }
}
