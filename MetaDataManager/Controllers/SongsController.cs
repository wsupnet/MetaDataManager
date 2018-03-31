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
            List<Song> model = new List<Song>();

            if (ModelState.IsValid)
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

                foreach (var album in db.Albums)
                {
                    if (album.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        //Gets the tracks based on the Id of the Album
                        var getTrack = spotify.GetAlbumTracks(album.Spotify_Id, 50, 0, "");

                        foreach (var track in getTrack.Items)
                        {
                            var tempModel = new Song
                            {
                                Name = track.Name,
                                AlbumId = album.Id,
                                Artist_Name = track.Artists[0].Name,
                                Album_Name = album.Name,
                                Track_Number = track.TrackNumber,
                                Duration = track.DurationMs,
                                Preview_Url = track.PreviewUrl,
                                Disc_Number = track.DiscNumber,
                                Spotify_Id = track.Id,
                                Album_SpotId = album.Spotify_Id
                            };
                            model.Add(tempModel);
                        };
                        ViewData["Tracks"] = getTrack.Items.ToList();
                    }
                }
                if (albumId == null)
                {
                    return View(model.ToList());
                }
                else
                {
                    return View(model.Where(x => x.AlbumId == albumId).ToList());
                }

            }

            return View();
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
