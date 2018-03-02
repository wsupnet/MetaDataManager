using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MetaDataManager.Models;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.UI.WebControls.Expressions;

using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using Newtonsoft.Json;
using MetaDataManager.Data;
using PagedList;
using System.Web.UI;

namespace MetaDataManager.Controllers
{
    public class HomeController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        //private static SpotifyWebAPI _spotify;
        static ClientCredentialsAuth auth;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ArtistNameModel artistNameModel)
        {
            if (ModelState.IsValid)
            {

                //Create the auth object
                auth = new ClientCredentialsAuth()
                {
                    //Your client Id
                    ClientId = "d465cd5175d04b038cca6f1679643396",
                    //Your client secret UNSECURE!!
                    ClientSecret = "b136e21e115b49b0bb6afd6f3560192e",
                    //How many permissions we need?
                    Scope = Scope.UserReadPrivate,
                };

                //With this token object, we now can make calls
                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                var searchArtist = spotify.SearchItems(artistNameModel.ArtistName, SpotifyAPI.Web.Enums.SearchType.Artist);

                //var albumInfo = spotify.GetAlbum("2szeSQtOcJgRhDXmTS3SIf");
                //var trackInfo = spotify.GetTrack("2szeSQtOcJgRhDXmTS3SIf");
                //var artistInfo = spotify.GetArtist("2szeSQtOcJgRhDXmTS3SIf");

                //Searches for songName
                var songName = spotify.SearchItems(artistNameModel.SongName, SpotifyAPI.Web.Enums.SearchType.Track);




                if (!string.IsNullOrEmpty(artistNameModel.SongName))
                {
                    //int pageSize = 5;
                    //int pageNumber = (page ?? 1);
                    var result = songName.Tracks;
                    ViewData["SongJson"] = JsonConvert.SerializeObject(result);
                    ViewData["Songs"] = songName.Tracks.Items.ToList();

                    //Save to database table
                    //foreach (var song in result.Items)
                    //{
                    //    var name = song.Name;
                    //    var spot_Id = song.Id;
                    //}
                }
                else if (!string.IsNullOrEmpty(artistNameModel.ArtistName))
                {
                    //int pageSize = 5;
                    //int pageNumber = (page ?? 1);
                    var result = songName.Tracks;
                    ViewData["ArtistsJson"] = JsonConvert.SerializeObject(searchArtist.Artists);
                    ViewData["Artists"] = searchArtist.Artists.Items.ToList();

                    //var model = searchArtist.Artists.Items.ToPagedList(pageNumber, pageSize);
                    //return View(model);

                }

                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> About(Playlist playlist)
        {
            if (ModelState.IsValid)
            {

                //Create the auth object
                auth = new ClientCredentialsAuth()
                {
                    //Your client Id
                    ClientId = "d465cd5175d04b038cca6f1679643396",
                    //Your client secret UNSECURE!!
                    ClientSecret = "b136e21e115b49b0bb6afd6f3560192e",
                    //How many permissions we need?
                    Scope = Scope.UserReadPrivate,
                };

                //With this token object, we now can make calls
                Token token = auth.DoAuth();
                var spotify = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken,
                    UseAuth = true
                };

                string ClientId = auth.ClientId;

                var newPlaylist = spotify.CreatePlaylist(ClientId, playlist.Description, playlist.Public);

            }

            return Redirect("https://api.spotify.com/v1/users/{ClientId}/playlists");
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }
        }
}