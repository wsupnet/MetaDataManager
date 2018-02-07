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

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //private static SpotifyWebAPI _spotify;
        static ClientCredentialsAuth auth;
        public async Task<ActionResult> Index()
        {

            //string trackTitle = "Back In Black";
            //string trackArtist = "ACDC";
            //string url = string.Format("https://api.spotify.com/v1/search?q={0} {1}&type=track&market=US&limit=10&offset=0", trackTitle, trackArtist);

            //var jsonData = GetTrackInfo(url);

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

            //SearchItem track = spotify.SearchItems("roadhouse+blues", SearchType.Album | SearchType.Playlist);
            //var track = spotify.SearchItems("roadhouse+blues", SearchType.Album | SearchType.Playlist);
            var track = spotify.SearchItems("bts", SpotifyAPI.Web.Enums.SearchType.Artist | SpotifyAPI.Web.Enums.SearchType.Playlist);


            ViewData["ArtistsJson"] = JsonConvert.SerializeObject(track.Artists);
            ViewData["Artists"] = track.Artists.Items.ToList();


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