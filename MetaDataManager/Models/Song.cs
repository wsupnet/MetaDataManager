using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Song
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        public string Name { get; set; }

        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public virtual Album Albums { get; set; }

        public string Spotify_Id { get; set; }
    }
}