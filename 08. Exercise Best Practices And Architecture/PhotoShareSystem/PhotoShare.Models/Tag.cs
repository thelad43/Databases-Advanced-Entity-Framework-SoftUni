namespace PhotoShare.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AlbumTag> AlbumTags { get; set; } = new List<AlbumTag>();
    }
}