﻿using Microsoft.AspNetCore.Http;

namespace TranquilAlmatyWebsite.Models.Forum
{
    public class AddForumModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile ImageUpload { get; set; }
    }
}
