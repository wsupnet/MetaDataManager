using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public string Image { get; set; }

        [DisplayName("Artist Bio")]
        public string Description { get; set; }

        public string Website { get; set; }

        public string Spotify_Id { get; set; }
    }
}