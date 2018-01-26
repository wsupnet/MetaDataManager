using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetaDataManager.Data;
using MetaDataManager.Models;

namespace MetaDataManager.Controllers
{
    public class AlbumsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Albums
        public ActionResult Index(int? artistId)
        {

            //MetaDataManagerContext metaDataManagerContext = new MetaDataManagerContext();
            //List<Album> albums = metaDataManagerContext.Albums.Where(x => x.Id == artistId).ToList();

            //return View(albums);

            //if (artistId > 0)
            //{
            //    ViewBag.ArtistId = artistId;

            //    return View(db.Albums.Where(x => x.Artist).ToList());
            //}
            //else
            //{
            //    return View(db.Albums.ToList());
            //}

            ViewBag.ArtistId = artistId;

            return View(db.Albums.Where(x => x.ArtistId == artistId).ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        [HttpGet]
        public ActionResult Create(int? artistId)
        {
            Album album = new Album();

            ViewBag.ArtistId = artistId;

            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Artists,Year,Tracks,Genre")] Album album)
        {
            if (ModelState.IsValid)
            {
                //album.ArtistId = db.Artists.Where(x => x.Id == album.ArtistId).FirstOrDefault();
                ViewBag.ArtistId = album.ArtistId;
                //album.ArtistId = db.Artists.Where(x => x.Id == album.ArtistId).FirstOrDefault();
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Artist,Year,Tracks,Genre")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
