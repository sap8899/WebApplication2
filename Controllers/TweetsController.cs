using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Models;
using TweetSharp;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Site Admin")]
    public class TweetsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Tweets twts)
        {
            string key = "YBMWwwLqGSL0vIFspKFpsA2wt";
            string secret = "4RNXjB0LoT0NXSXQok3OvN79iKxn8mxbTz9s8WREatPZNaU0cH";
            string token = "1444302272784211968-LD40izCgSO0CXGps1RGopf86qE8HCd";
            string tokenSecret = "wUvdsHs0lwNjO4eKP0rJQKuujlKsTAnOqrhA5NSndBAdv";

            string message = twts.tweets;

            //Enter the Image Path if you want to upload image .

            var service = new TweetSharp.TwitterService(key, secret);
            service.AuthenticateWith(token, tokenSecret);

            //this Condition  will check weather you want to upload a image & text or only text 

            var result = service.SendTweet(new SendTweetOptions
            {
                Status = message
            });

            twts.tweets = "";
            return View();
        }
    }
}