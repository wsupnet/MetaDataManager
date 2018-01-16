using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int YearFormed { get; set; }

        public string Website { get; set; }
    }
}