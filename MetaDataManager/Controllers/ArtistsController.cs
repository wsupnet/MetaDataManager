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
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Enums;
using Newtonsoft.Json;

namespace MetaDataManager.Controllers
{
    public class ArtistsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Artists
        public ActionResult Index(string Spot_Id, ArtistNameModel artistNameModel)
        {
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

                //Searches for artist
                var searchArtist = spotify.SearchItems(artistNameModel.ArtistName, SpotifyAPI.Web.Enums.SearchType.Artist);

                if (!string.IsNullOrEmpty(artistNameModel.ArtistName))
                {
                    ViewData["ArtistsJson"] = JsonConvert.SerializeObject(searchArtist.Artists);
                    ViewData["Artists"] = searchArtist.Artists.Items.ToList();
                }



                List<Artist> model = new List<Artist>(); //Create a list so we can add items 
                foreach (var artist in db.Artists) // For each album you find in database of albums...
                {
                    if (artist.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        // It uses the spotify api and searches using the GetTrack method against the database 
                        var searchTrack = spotify.GetArtist(artist.Spotify_Id);

                        //Create a temporary model so we can add it to the model list above
                        var tempModel = new Artist
                        {
                            Name = searchTrack.Name,
                            Spotify_Id = artist.Spotify_Id,
                        };
                        model.Add(tempModel); //Adding our results to the model we created earlier
                    }

                    //returns the List we made referencing the Spotify_Id of the album
                    
                }

                return View(model);
            }

            return View(db.Artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,YearFormed,Website,Description")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,YearFormed,Website,Description")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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

        public ActionResult AddDiscography(int Id)
        {
            return RedirectToAction("Index", "Albums", new { artistId = Id });
        }

        public ActionResult RedirectToExternalSite(int Id)
        {
            //Artist model = new Artist();
            Artist model = db.Artists.Find(Id);

            return Redirect(model.Website);
            //return String.Format("<a href='http://{0}' target="_blank">{1}</a>", url, text);

        }

        [HttpGet]
        public ActionResult Add(string Spot_Id)
        {
            if (ModelState.IsValid)
            {
                //Creating a new instance of the artist class.
                //Basically populating the properties/fields
                Artist artist = new Artist
                {
                    Spotify_Id = Spot_Id
                };

                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
