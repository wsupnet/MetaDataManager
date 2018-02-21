using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class ArtistNameModel
    {
        public int Id { get; set; }

        [DisplayName("Artist Name Here")]
        public string ArtistName { get; set; }

        [DisplayName("Song Name Here")]
        public string SongName { get; set; }
    }
}