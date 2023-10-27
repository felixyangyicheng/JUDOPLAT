﻿
namespace JUDOPLAT.API_JUDOPLAT.Data
{
    [Table("command")]

    public class Command : BaseTextItem
    {
        [Column("title")]

        public string Title { get; set; }
        [Column("type")]

        public string Type { get; set; }
        [Column("closed")]

        public bool Closed { get; set; }
    }
}
