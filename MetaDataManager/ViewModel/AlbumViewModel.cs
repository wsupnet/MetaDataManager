using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetaDataManager.ViewModel
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        [DisplayName("Album Name")]
        public string Name { get; set; }

        public int Year { get; set; }

        public int Tracks { get; set; }

        public int ArtistId { get; set; }

        public string Artist { get; set; }
    }
}