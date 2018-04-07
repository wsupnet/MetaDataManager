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
using PagedList;

namespace MetaDataManager.Controllers
{
    public class ArtistsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Artists
        public ActionResult Index(string Spot_Id, int? page, string search, string searchBy, string sortBy, ArtistNameModel artistNameModel)
        {
            if (ModelState.IsValid)
            {
                //Create the auth object
                var auth = new ClientCredentialsAuth()
                {
                    //Your client Id
                    ClientId = "CLIENT_ID",
                    //Your client secret UNSECURE!!
                    ClientSecret = "CLIENT_SECRET_HERE",
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
                            Id = artist.Id,
                            Name = searchTrack.Name,
                            Spotify_Id = artist.Spotify_Id,
                            Popularity = searchTrack.Popularity,
                            Image = searchTrack.Images[2].Url,
                            Description = artist.Description,
                            Website = artist.Website
                        };
                        model.Add(tempModel); //Adding our results to the model we created earlier
                    }                    
                }

                ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
                ViewBag.SortPopularityParameter = sortBy == "Popularity" ? "Pop desc" : "Popularity";

                var artists = model.AsQueryable();

                //Searches Artist by name 
                if(searchBy == "Name")
                {
                    artists = artists.Where(x => x.Name.ToLower().StartsWith(search) || search == null);
                }

                //Switch statement that controls how the List of Artists is sorted
                switch (sortBy)
                {
                    case "Name desc":
                        artists = artists.OrderByDescending(x => x.Name);
                        break;
                    case "Pop desc":
                        artists = artists.OrderByDescending(x => x.Popularity);
                        break;
                    case "Popularity":
                        artists = artists.OrderBy(x => x.Popularity);
                        break;
                    default:
                        artists = artists.OrderBy(x => x.Name);
                        break;

                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(artists.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create([Bind(Include = "Id,Name,Website,Description")] Artist artist)
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
        public ActionResult Edit([Bind(Include = "Id,Name,Website,Description,Spotify_Id")] Artist artist)
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

        [HttpGet]
        public ActionResult Add(string Spot_Id, string image, string aName)
        {
            if (ModelState.IsValid)
            {
                //Creating a new instance of the artist class.
                //Basically populating the properties/fields
                Artist artist = new Artist
                {
                    Spotify_Id = Spot_Id,
                    Name = aName
                };

                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
