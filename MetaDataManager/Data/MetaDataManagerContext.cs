using MetaDataManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MetaDataManager.Data
{
    public class MetaDataManagerContext : DbContext
    {
        public MetaDataManagerContext() : base("DefaultConnection")
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
    }
}