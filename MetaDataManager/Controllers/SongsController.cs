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
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MetaDataManager.Controllers
{
    public class SongsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Songs
        public ActionResult Index(int? albumId)
        {
            if (albumId == null)
            {
                //Create the auth object
                var auth = new ClientCredentialsAuth()
                {
                    //Your client Id
                    ClientId = "d465cd5175d04b038cca6f1679643396",
                    //Your client secret UNSECURE!!
                    ClientSecret = "b136e21e115b49b0bb6afd6f3560192e",
                    //How many permissions we need?
                    Scope = Scope.UserReadPrivate,
                };

                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                List<Song> model = new List<Song>(); //Create a list so we can add items 
                foreach (var song in db.Songs) // For each album you find in database of albums...
                {
                    if (song.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        // It uses the spotify api and searches using the GetTrack method against the database 
                        var searchTrack = spotify.GetTrack(song.Spotify_Id, "");

                        //Create a temporary model so we can add it to the model list above
                        var tempModel = new Song
                        {
                            Title = searchTrack.Name,
                            AlbumId = song.AlbumId,
                            Spotify_Id = song.Spotify_Id,
                            Playlist_Id = song.Playlist_Id
                        };
                        model.Add(tempModel); //Adding our results to the model we created earlier
                    }

                }
                //returns the List we made referencing the Spotify_Id of the album
                return View(model);
            }



            //if (albumId == null)
            //{
            //    return View(db.Songs.ToList());
            //}

            ViewBag.AlbumId = albumId;

            return View(db.Songs.Where(x => x.AlbumId == albumId).ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create(int albumId)
        {
            Song song = new Song();

            ViewBag.AlbumId = albumId;

            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,AlbumId,Artist,Year")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index", new { songId = song.AlbumId });
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Album,Artist,Year")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
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
