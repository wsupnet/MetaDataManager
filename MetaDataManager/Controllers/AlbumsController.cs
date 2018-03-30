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
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using Newtonsoft.Json;
using PagedList;

namespace MetaDataManager.Controllers
{
    public class AlbumsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Albums
        public ActionResult Index(string songId, string albumId, int? page, string searchBy, string search, string sortBy, ArtistNameModel artistNameModel)
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

                //Searches for songName
                var songName = spotify.SearchItems(artistNameModel.SongName, SearchType.Album);

                if (!string.IsNullOrEmpty(artistNameModel.SongName))
                {
                    var result = songName.Tracks;
                    ViewData["SongJson"] = JsonConvert.SerializeObject(result);
                    ViewData["Songs"] = songName.Albums.Items.ToList();
                }

                List<Album> model = new  List<Album>(); //Create a list so we can add items 
                foreach (var album in db.Albums) // For each album you find in database of albums...
                {
                    if (album.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        // It uses the spotify api and searches using the GetTrack method against the database 
                        var searchAlbum = spotify.GetAlbum(album.Spotify_Id, "");

                        ////Get the Artist Name
                        //if (!string.IsNullOrEmpty(artistNameModel.SongName))
                        //{
                        //    var artistInfo = songName.Tracks.Items.Where(x => x.Id == album.Spotify_Id).FirstOrDefault();
                        //}
                        //Create a temporary model so we can add it to the model list above
                        var tempModel = new Album
                        {
                            Id = album.Id,
                            Name = searchAlbum.Name,
                            Tracks = searchAlbum.Tracks.Total,
                            Label = searchAlbum.Label,
                            Release_Date = searchAlbum.ReleaseDate,
                            Image = searchAlbum.Images[1].Url,
                            ArtistId = searchAlbum.Artists[0].Id,
                            Artist_Name = searchAlbum.Artists[0].Name,                  
                            Spotify_Id = album.Spotify_Id,
                            AlbumId = album.Id
                        };

                        model.Add(tempModel); //Adding our results to the model we created earlier
                    }
                    
                }

                //Responsible for sorting
                ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
                ViewBag.SortArtistParameter = sortBy == "Artist" ? "Artist desc" : "Artist";
                ViewBag.SortDateParameter = sortBy == "Date" ? "Date desc" : "Date";
                ViewBag.SortRecordParameter = sortBy == "Record" ? "Record desc" : "Record";

                var albums = model.AsQueryable();

                //searches Album by Name
                if (searchBy == "Name")
                {
                    albums = albums.Where(x => x.Name.ToLower().StartsWith(search) || search == null);
                }
                else
                {
                }

                //Switch statement that controls how the List of Albums is sorted
                switch (sortBy)
                {
                    case "Name desc":
                        albums = albums.OrderByDescending(x => x.Name);
                        break;
                    case "Artist desc":
                        albums = albums.OrderByDescending(x => x.Artist_Name);
                        break;
                    case "Artist":
                        albums = albums.OrderBy(x => x.Artist_Name);
                        break;
                    case "Date desc":
                        albums = albums.OrderByDescending(x => x.Release_Date);
                        break;
                    case "Date":
                        albums = albums.OrderBy(x => x.Release_Date);
                        break;
                    case "Record desc":
                        albums = albums.OrderByDescending(x => x.Label);
                        break;
                    case "Record":
                        albums = albums.OrderBy(x => x.Label);
                        break;
                    default:
                        albums = albums.OrderBy(x => x.Name);
                        break;

                }

                //int pageSize = 5;
                //int pageNumber = (page ?? 1);
                //returns the List we made referencing the Spotify_Id of the album
                //return View(model.ToPagedList(pageNumber, pageSize));
                return View(albums.ToList());
                //return View(model);
            }

            return View(db.Albums.ToList());
            //return View(db.Albums.Where(x => x.ArtistId == artistId).ToList());
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
        public ActionResult Create(int artistId)
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
        public ActionResult Create([Bind(Include = "Id,Name,ArtistId,Year,Tracks,Genre")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index", new { artistId = album.ArtistId });
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

        public ActionResult SeeTracks(int albumId)
        {
            return RedirectToAction("Index", "Songs", new { albumId = albumId });
        }

        [HttpGet]
        public ActionResult Add(string albumSpotId, string albumName)
        {
            if (ModelState.IsValid)
            {
                //Creating a new instance of the Album class.
                //Basically populating the properties/fields
                Album album = new Album
                {
                    Name = albumName,
                    Spotify_Id = albumSpotId,
                };
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        public ActionResult FullData()
        {
            /*
             var entryPoint = (from ep in dbContext.tbl_EntryPoint
                                 join e in dbContext.tbl_Entry on ep.EID equals e.EID
                                 join t in dbContext.tbl_Title on e.TID equals t.TID
                                 where e.OwnerID == user.UID
                                 select new {
                                     UID = e.OwnerID,
                                     TID = e.TID,
                                     Title = t.Title,
                                     EID = e.EID
                                 }).Take(10);
             */

            var model = (from artist in db.Artists
                         join album in db.Albums on artist.Name equals album.Artist_Name
                         join song in db.Songs on album.Artist_Name equals song.Artist_Name
                         select new
                         {
                             Name = song.Name,
                             Albums = song.Albums,
                             Artist = artist.Name,
                             Duration = song.Duration,
                             Preview_Url = song.Preview_Url
                         });

            return View(model);
        }
    }
}
