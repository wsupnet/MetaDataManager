using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Record Label")]
        public string Label { get; set; }

        [DisplayName("Release Date")]
        public string Release_Date { get; set; }

        [DisplayName("Album Art")]
        public string Image { get; set; }

        public string ArtistId { get; set; }

        public int AlbumId { get; set; }

        [DisplayName("Artist")]
        public string Artist_Name { get; set; }

        public string Spotify_Id { get; set; }

        public string Playlist_Id { get; set; }
    }
}