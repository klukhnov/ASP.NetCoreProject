using System.Collections.Generic;
using TranquilAlmatyWebsite.Models.Post;

namespace TranquilAlmatyWebsite.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
    }
}
