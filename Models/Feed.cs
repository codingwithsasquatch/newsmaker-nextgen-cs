using Microsoft.WindowsAzure.Storage.Table;

namespace NewsMaker.Models
{
        public class Feed : TableEntity
        {
            public string ShortUrl {
                get { return RowKey; }
                set { RowKey = value; }
            }
            public string LongUrl { get; set; }
        }
}