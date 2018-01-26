using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Artist { get; set; }

        public int Year { get; set; }

        public int Tracks { get; set; }

        //public string Genre { get; set; }

        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public virtual Artist Artists { get; set; }


    }
}