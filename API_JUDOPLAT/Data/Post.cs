
namespace JUDOPLAT.API_JUDOPLAT.Data
{
    [Table("post")]

    public class Post:BaseTextItem
    {
        [Column("title")]

        public string Title { get; set; }

        public virtual IList<Comment> Comments { get; set; }

    }
}
