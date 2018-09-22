namespace PhotoShare.App.Core.Models
{
    using Common;

    public class TagModel
    {
        public int Id { get; set; }

        [Tag]
        public string Name { get; set; }
    }
}