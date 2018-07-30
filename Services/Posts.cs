using NewsMaker.Models;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using Microsoft.SyndicationFeed.Atom;
using System.Xml;
using System.Threading.Tasks;


namespace NewsMaker.Services
{
    class Posts
    {
        private async Task<bool> retrievePosts(Feed feed)
        {
            using (var xmlReader = XmlReader.Create("https://azurecomcdn.azureedge.net/en-us/blog/feed/", new XmlReaderSettings() { Async = true }))
            {
                // Instantiate an Rss20FeedReader using the XmlReader.
                // This will assign as default an Rss20FeedParser as the parser.
                var feedReader = new RssFeedReader(xmlReader);

                //
                // Read the feed
                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await feedReader.ReadImage();
                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            break;

                        // Read link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();
                            break;
                    }
                }
            }
            return true;
        }
    }
}