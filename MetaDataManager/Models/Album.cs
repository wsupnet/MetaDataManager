using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Tracks { get; set; }

        public int ArtistId { get; set; }

        //[ForeignKey("ArtistId")]
        //public virtual Artist Artists { get; set; }

        public string Spotify_Id { get; set; }

        public string Playlist_Id { get; set; }
    }
}