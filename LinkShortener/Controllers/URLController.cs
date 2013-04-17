﻿using System;
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
        //
        // GET: /URL/

        public ActionResult Index()
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            var data = lsm.GetUrlList();
            return View(data);
        }

        public void S(int id)
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            string url = lsm.GetSUrl(id);
            string ValidUrl = "";
            if (!url.StartsWith("http://"))
            {
                ValidUrl = "http://" + url;
            }
            else
            {
                ValidUrl = url;
            }
            Response.Redirect(ValidUrl);
        }

        [HttpPost]
        public ActionResult Create(string siteName, string url, string description)
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            lsm.CreateShortURL(siteName, url, description);
            TheJwalLSEntities db = new TheJwalLSEntities();
            var id = (from x in db.Links
                         where x.URL == url
                         select x.Id).FirstOrDefault();
            ViewBag.sURL = id ;
            return View("Success");
        }

    }
}
