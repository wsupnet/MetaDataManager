using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaDataManager.ViewModel
{
    public class FullInfoViewModel
    {
        public int Id { get; set; }

        public string Track { get; set; }

        public int TrackId { get; set; }

        public string Album { get; set; }

        public int AlbumId { get; set; }

        public string Artist { get; set; }

        public int ArtistId { get; set; }

        public int Duration { get; set; }

        public string Preview_Url { get; set; }

    }
}