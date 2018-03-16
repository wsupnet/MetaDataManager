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

namespace MetaDataManager.Controllers
{
    public class AlbumsController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        // GET: Albums
        public ActionResult Index(int? artistId, string songId, string albumId, ArtistNameModel artistNameModel)
        {

            if (artistId == null)
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
                var songName = spotify.SearchItems(artistNameModel.SongName, SpotifyAPI.Web.Enums.SearchType.Track);


                if (!string.IsNullOrEmpty(artistNameModel.SongName))
                {
                    var result = songName.Tracks;
                    ViewData["SongJson"] = JsonConvert.SerializeObject(result);
                    ViewData["Songs"] = songName.Tracks.Items.ToList();

                }

                List<Album> model = new  List<Album>(); //Create a list so we can add items 
                foreach (var album in db.Albums) // For each album you find in database of albums...
                {
                    if (album.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        // It uses the spotify api and searches using the GetTrack method against the database 
                        var searchTrack = spotify.GetTrack(album.Spotify_Id, ""); 

                        //Create a temporary model so we can add it to the model list above
                        var tempModel = new Album
                        {
                            Name = searchTrack.Name,
                            Tracks = album.Tracks,
                            ArtistId = album.ArtistId,
                            Spotify_Id = album.Spotify_Id,
                            Playlist_Id = album.Playlist_Id
                        };
                        model.Add(tempModel); //Adding our results to the model we created earlier
                    }
                    
                }

                //List<Song> model2 = new List<Song>();
                //foreach ( var song in db.Songs)
                //{
                //    if (song.Spotify_Id == null)
                //    {
                //        var searchTracks = spotify.GetTrack(song.Spotify_Id, "");

                //        var tempModel2 = new Song
                //        {
                //            Title = searchTracks,
                //        }
                //    }
                //}

                //returns the List we made referencing the Spotify_Id of the album
                return View(model);
            }

            ViewBag.ArtistId = artistId;
            return View();
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

        public ActionResult AddSong(int Id)
        {
            return RedirectToAction("Index", "Songs", new { albumId = Id });
        }

        [HttpGet]
        public ActionResult Add(string songId, string albumId)
        {
            if (ModelState.IsValid)
            {

                //Creating a new instance of the Album class.
                //Basically populating the properties/fields
                Album album = new Album
                {
                    Spotify_Id = songId,
                    Playlist_Id = albumId
                };
                //foreach(var song in db.Songs)
                //{
                //    Song track = new Song
                //    {
                //        Spotify_Id = 
                //    }
                //}

                db.Albums.Add(album);
                db.SaveChanges();

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

                List<Album> albumList = new List<Album>();

                //Created a list so we can go through each row
                var albums = db.Albums.ToList();
                foreach(var albumRow in albums)
                {
                    if (albumRow.Spotify_Id != null) // If the Spotify_Id is not null
                    {
                        //var name = GetSpotifyArtist(albumRow.Spotify_Id); //Getting the name of the album
                        var searchAlbum = spotify.GetAlbum(albumRow.Spotify_Id, "");
                        Album newAlbum = new Album
                        {
                            Id = albumRow.Id,
                            Name = searchAlbum.Name,
                            Tracks = searchAlbum.Tracks.Total,
                            Label = searchAlbum.Label,
                            Release_Date = searchAlbum.ReleaseDate,
                            Image = searchAlbum.Images[2].Url,
                            ArtistId = searchAlbum.Artists[0].Id,
                            Artist_Name = searchAlbum.Artists[0].Name,
                            Spotify_Id = albumRow.Spotify_Id,
                            Playlist_Id = albumRow.Playlist_Id
                        };
                        albumList.Add(newAlbum);
                    }
                }
                return PartialView("_PartialAlbumTable", albumList);

                //return RedirectToAction("Index");
            }

            return View("Index");
        }

        private string GetSpotifyArtist(string albumId)
        {
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
            var songName = spotify.SearchItems(albumId, SpotifyAPI.Web.Enums.SearchType.Track);


            return songName.Tracks.Items[0].Album.Name; //Find out how to get just the name of the album
        }
    }
}
