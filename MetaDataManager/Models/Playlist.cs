using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Public { get; set; }

        public bool Collaborative { get; set; }

        public string Description { get; set; }
    }
}