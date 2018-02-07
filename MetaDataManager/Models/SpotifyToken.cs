using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaDataManager.Models
{
    public class SpotifyToken
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
    }
}