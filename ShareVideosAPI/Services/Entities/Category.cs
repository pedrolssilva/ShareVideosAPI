﻿namespace ShareVideosAPI.Services.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Color { get; set; }

        public ICollection<Video> Videos { get; set; }

        public Category()
        {
            Videos = new List<Video>();
        }
    }
}
