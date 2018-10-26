using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TranquilAlmatyWebsite.Data;
using TranquilAlmatyWebsite.Data.Models;
using TranquilAlmatyWebsite.Models.Post;
using TranquilAlmatyWebsite.Models.Reply;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;

namespace TranquilAlmatyWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;
        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forumService,
            UserManager<ApplicationUser> userManager, 
            IApplicationUser userService)
        {
            _postService = postService;
            _forumService = forumService;
            _userService = userService;
            _userManager = userManager;
          
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies, 
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User)

            };
            return View(model);
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                 .Result.Contains("Admin");
        }

        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost] 
        
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            _postService.Add(post).Wait();
            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);

            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        } 
    }
}