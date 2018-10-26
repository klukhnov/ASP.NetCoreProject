using System.Collections.Generic;
using TranquilAlmatyWebsite.Models.Post;

namespace TranquilAlmatyWebsite.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
    }
}
