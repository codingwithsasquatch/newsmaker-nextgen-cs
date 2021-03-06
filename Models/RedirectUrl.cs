using Microsoft.WindowsAzure.Storage.Table;

namespace NewsMaker.Models
{
        public class RedirectUrl : TableEntity
        {
            public string ShortUrl {
                get { return RowKey; }
                set { RowKey = value; }
            }
            public string LongUrl { get; set; }
        }
}