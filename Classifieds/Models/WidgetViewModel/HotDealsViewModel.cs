﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Models.WidgetViewModel
{
    public class HotDealsViewModel
    {
        int size = 6;
        public HotDealsViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var now = DateTime.Now;
            // get random deals
            var randomDealsQ = from d in db.Deals.Include("Listing").Include("Listing.images")
                         where d.Ends > now && d.Starts < now && d.Listing.images.Count() > 0
                         select d;
            RandomDeals = randomDealsQ.OrderBy(c => Guid.NewGuid()).Take(size).ToList();
            //get latest deals
            var latestDealsQ = from d in db.Deals
                               where d.Ends > now && d.Starts < now && d.Listing.images.Count() > 0
                               select d;
            LatestDeals = latestDealsQ.OrderByDescending(c => c.Starts).Take(size).ToList();

            //get highest scoring deals
            var editorPickQ = from d in db.Deals
                               where d.Ends > now && d.Starts < now && d.Listing.images.Count() > 0
                               select d;
            EditorPicks = editorPickQ.OrderByDescending(c => c.TotalScore).Take(size).ToList();

             
        }
        public List<Deal> RandomDeals { get; set; }
        public List<Deal> LatestDeals { get; set; }
        public List<Deal> EditorPicks { get; set; }
    }
}