using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAL.Entities
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public byte[] AudioData { get; set; }
    }
}
