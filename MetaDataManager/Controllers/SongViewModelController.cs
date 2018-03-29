using MetaDataManager.Data;
using MetaDataManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetaDataManager.Controllers
{
    public class SongViewModelController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();
        // GET: SongViewModel
        public ActionResult Index()
        {
            //var Results =
            //    (from Albums in db.Albums
            //     join Songs in db.Songs
            //     on Albums.Id equals Songs.AlbumId
            //     select new SongViewModel
            //     {
            //        Title = Songs.Title,
            //        Album = Albums.Name
            //     }).ToList();

            return View();
        }
    }
}