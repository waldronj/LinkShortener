using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkEntities;
using LinkShortener.Models;

namespace LinkShortener.Controllers
{
    public class URLController : Controller
    {
       
        public ActionResult Index()
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            var data = lsm.GetUrlList();
            return View(data);
        }

        public void S(string id)
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            string url = lsm.GetSUrl(id);
            string ValidUrl = "";
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                ValidUrl = url;
            }
            else
            {
                ValidUrl = "http://" + url;
            }
            Response.Redirect(ValidUrl);
        }

        [HttpPost]
        public ActionResult Create(string siteName, string url, string description)
        {
            string checkUrl = url.ToLower();
            if (checkUrl.StartsWith("http://") || checkUrl.StartsWith("https://"))
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var sUrl = new String(stringChars);
                
                LinkShortenerModel lsm = new LinkShortenerModel();
                lsm.CreateShortURL(siteName, url, description, sUrl);
                TheJwalLSEntities db = new TheJwalLSEntities();
                var id = (from x in db.Links
                          where x.URL == url
                          select x.Id).FirstOrDefault();
                ViewBag.sURL = id;
                return View("Success");   
            }
            else
            {
                return View("Failure");
            }
        }

    }
}
