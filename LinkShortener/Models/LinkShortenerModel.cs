using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkEntities;

namespace LinkShortener.Models
{
    public class LinkShortenerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ShortURL { get; set; }
        public string Description { get; set; }
    
        public List<LinkShortenerModel> GetUrlList()
        {
            TheJwalLSEntities db = new TheJwalLSEntities();
            List<LinkShortenerModel> lsm = new List<LinkShortenerModel>();
            var data = db.Links.ToList();
            foreach (var item in data)
            {
                LinkShortenerModel sl = new LinkShortenerModel();
                sl.Id = item.Id;
                sl.Name = item.Name;
                sl.ShortURL = item.ShortURL;
                sl.Description = item.Description;
                sl.URL = item.URL;
                lsm.Add(sl);    
            }
            return lsm;
        }

        public void CreateShortURL(string siteName, string url, string description, string sUrl)
        {
            if (!CheckDupe(url))
            {
                TheJwalLSEntities db = new TheJwalLSEntities();
                Link lnk = new Link();
                lnk.Name = siteName;
                lnk.URL = url;
                lnk.ShortURL = sUrl;
                lnk.Description = description;
                db.Links.Add(lnk);
                db.SaveChanges();    
            }
            
        }

        public string GetSUrl(int id)
        {
            TheJwalLSEntities db = new TheJwalLSEntities();
            var data = (from x in db.Links where x.Id == id select x).FirstOrDefault();
            string ShortenedUrl = data.URL;
            return ShortenedUrl;
        }

        public bool CheckDupe(string url)
        {
            TheJwalLSEntities db = new TheJwalLSEntities();
            var result = (from x in db.Links
                          where x.URL == url
                          select x).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }


}