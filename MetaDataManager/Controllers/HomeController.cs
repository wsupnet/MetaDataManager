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

namespace MetaDataManager.Controllers
{
    public class HomeController : Controller
    {
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
                    ClientId = "CLIENT_ID_HERE",
                    //Your client secret UNSECURE!!
                    ClientSecret = "CLIENT_SECRET_HERE",
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

                //SearchItem track = spotify.SearchItems("roadhouse+blues", SearchType.Album | SearchType.Playlist);
                //var track = spotify.SearchItems("roadhouse+blues", SearchType.Album | SearchType.Playlist);
                var searchArtist = spotify.SearchItems(artistNameModel.ArtistName, SpotifyAPI.Web.Enums.SearchType.Artist | SpotifyAPI.Web.Enums.SearchType.Playlist);
                var songName = spotify.SearchItems(artistNameModel.SongName, SpotifyAPI.Web.Enums.SearchType.Track | SpotifyAPI.Web.Enums.SearchType.Playlist);

                if (!string.IsNullOrEmpty(artistNameModel.SongName))
                {
                    ViewData["SongJson"] = JsonConvert.SerializeObject(songName.Tracks);
                    ViewData["Songs"] = songName.Tracks.Items.ToList();
                }
                else if (!string.IsNullOrEmpty(artistNameModel.ArtistName))
                {
                    ViewData["ArtistsJson"] = JsonConvert.SerializeObject(searchArtist.Artists);
                    ViewData["Artists"] = searchArtist.Artists.Items.ToList();
                }


            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}