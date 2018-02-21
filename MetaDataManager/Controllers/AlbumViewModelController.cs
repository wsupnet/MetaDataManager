using MetaDataManager.Data;
using MetaDataManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetaDataManager.Controllers
{
    public class AlbumViewModelController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();
        // GET: AlbumViewModel
        public ActionResult Index()
        {
            var Results =
                (from Artist in db.Artists
                 join Album in db.Albums
                 on Artist.Id equals Album.Id
                 select new AlbumViewModel
                 {
                     Name = Artist.Name,
                     Year = Artist.YearFormed,
                     Artist = Artist.Name,
                 }).ToList();



            return View(Results);
        }
    }
}