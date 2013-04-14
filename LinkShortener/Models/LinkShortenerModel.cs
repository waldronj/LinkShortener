using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkEntities;

namespace LinkShortener.Models
{
    public class LinkShortenerModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string ShortURL { get; set; }
        public string Description { get; set; }
    
        public List<LinkShortenerModel> GetUrlList()
        {
            LinkModelContainer db = new LinkModelContainer();
            List<LinkShortenerModel> lsm = new List<LinkShortenerModel>();
            var data = db.Links.ToList();
            foreach (var item in data)
            {
                LinkShortenerModel sl = new LinkShortenerModel();
                sl.Name = item.Name;
                sl.ShortURL = item.ShortURL;
                sl.Description = item.Description;
                sl.URL = item.URL;
                lsm.Add(sl);    
            }
            return lsm;
        }
    }


}