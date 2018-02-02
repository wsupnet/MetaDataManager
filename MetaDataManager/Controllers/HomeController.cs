using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MetaDataManager.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {

            //ViewBag.AuthUrl = GetAuthUrl();
            //var music = await MusicLookup();

            return View();
        }

        //private string GetAuthUrl()
        //{
        //    string clientId = "d465cd5175d04b038cca6f1679643396";
        //    string redirectUrl = "";
        //    Scope scope = Scope_Playlist_Modify_Playlist

        //    return "https://accounts.spotify.com/en/authorize?client_id-" + clientId +
        //        "&response_type-token&redirect_url" + redirectUrl +
        //        "&state-&scope" + scope.GetStringAttribute(" ") +
        //        "&show_dialog=true";
        //}

        //private async Task<String> MusicLookup()
        //{
            

            ////Get API Information
           
            //SpotifyWebAPI.Authentication.ClientId = "d465cd5175d04b038cca6f1679643396";
            //SpotifyWebAPI.Authentication.ClientSecret = "b136e21e115b49b0bb6afd6f3560192e";

            ////Search
            //var search = await SpotifyWebAPI.Artist.Search("Linkin Park");

        //    return "Done";
        //}

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