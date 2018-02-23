using MetaDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaDataManager.ViewModel
{
    public class SongViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AlbumId { get; set; }

        public string Album { get; set; }
    }
}