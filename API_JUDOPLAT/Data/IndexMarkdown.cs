

namespace JUDOPLAT.API_JUDOPLAT.Data
{
    [Table("indexMarkdown")]

    public class IndexMarkdown:BaseTextItem
	{
        [Column("title")]
        public string Title { get; set; }
        [Column("type")]

        public string? Type { get; set; }
        [Column("version")]

        public string? Version { get; set; }
    }

}

