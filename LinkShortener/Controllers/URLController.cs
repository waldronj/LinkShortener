﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkEntities;
using LinkShortener.Models;
using AttributeRouting.Web.Mvc;

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

        [GET("{id}")]
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
        public ActionResult Create(string siteName, string url, string description, string passphrase)
        {
            string checkUrl = url.ToLower();
            if (checkUrl.StartsWith("http://") || checkUrl.StartsWith("https://"))
            {
                if (passphrase == System.Configuration.ConfigurationManager.AppSettings["passphrase"].ToString())
                {
                    LinkShortenerModel lsm = new LinkShortenerModel();
                    lsm.CreateShortURL(siteName, url, description);
                    TheJwalLSEntities db = new TheJwalLSEntities();
                    var id = (from x in db.Links
                              where x.URL == url
                              select x.ShortURL).FirstOrDefault();
                    ViewBag.sURL = id;
                    return View("Success");
                }
                return View("Failure");
            }
            else
            {
                return View("Failure");
            }
        }

        public ActionResult Sort()
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            var data = lsm.GetUrlList().OrderBy(x=> x.Name );
            
            return View("Index", data);
        }

        public ActionResult getData()
        {
            LinkShortenerModel lsm = new LinkShortenerModel();
            var data = lsm.GetUrlList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
