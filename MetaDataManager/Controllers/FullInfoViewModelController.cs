using MetaDataManager.Data;
using MetaDataManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetaDataManager.Controllers
{
    public class FullInfoViewModelController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();
        // GET: FullInfoViewModel
        public ActionResult Index()
        {
            //var Results =
            //    (from Albums in db.Albums
            //     join Songs in db.Songs
            //     on Albums.Id equals Songs.AlbumId
            //     select new SongViewModel
            //     {
            //         Title = Songs.Title,
            //         Album = Albums.Name
            //     }).ToList();

            var model = (from artist in db.Artists
                         join album in db.Albums on artist.Name equals album.Artist_Name
                         join song in db.Songs on album.Id equals song.AlbumId
                         select new FullInfoViewModel
                         {
                             Track = song.Name,
                             Album = album.Name,
                             Artist = artist.Name,
                             Duration = song.Duration,
                             Preview_Url = song.Preview_Url
                         }).ToList();

            return View(model);
        }
    }
}