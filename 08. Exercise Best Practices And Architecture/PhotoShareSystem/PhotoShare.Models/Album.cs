namespace PhotoShare.Models
{
    using Models.Enums;
    using System.Collections.Generic;

    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Color? BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public List<AlbumRole> AlbumRoles { get; set; } = new List<AlbumRole>();

        public List<Picture> Pictures { get; set; } = new List<Picture>();

        public List<AlbumTag> AlbumTags { get; set; } = new List<AlbumTag>();
    }
}