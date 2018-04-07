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

namespace MetaDataManager.Controllers
{
    public class HomeController : Controller
    {
        private MetaDataManagerContext db = new MetaDataManagerContext();

        //private static SpotifyWebAPI _spotify;
        static ClientCredentialsAuth auth;

        public ActionResult Index()
        {
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
                    ClientId = "CLIENT_ID",
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
    }
}