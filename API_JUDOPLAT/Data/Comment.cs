﻿
namespace JUDOPLAT.API_JUDOPLAT.Data
{
    [Table("comment")]

    public class Comment:BaseTextItem
    {
        [ForeignKey("Post")]


        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
