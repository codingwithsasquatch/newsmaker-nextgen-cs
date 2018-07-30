using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Collections.Generic;
using System;

namespace NewsMaker.Models
{
    public class Post: TableEntity, ISyndicationItem
    {

        public string Id
        {
            get { return RowKey; }
            set { RowKey = value; }
        }
        public string Title {get; set; }
        public string Description { get; set; }
        public DateTimeOffset Published { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public IEnumerable<ISyndicationCategory> Categories { get; set; }
        public IEnumerable<ISyndicationPerson> Contributors { get; set; }
        public IEnumerable<ISyndicationLink> Links { get; set; }
    }
}