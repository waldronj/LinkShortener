using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkShortener.Models;

namespace LinkShortener.Controllers
{
    public class URLController : Controller
    {
        //
        // GET: /URL/

        public ActionResult Index()
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            var data = lsm.GetUrlList();
            return View(data);
        }

        [HttpPost]
        public ActionResult Create(string siteName, string url, string description)
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            lsm.CreateShortURL(siteName, url, description);
            ViewBag.sURL = url;
            return View("Success");
        }

    }
}
