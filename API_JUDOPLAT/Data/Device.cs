﻿
namespace JUDOPLAT.API_JUDOPLAT.Data
{
    [Table("device")]

    public class Device
	{
        [Column("id")]
        public Guid Id { get; set; }
        [ForeignKey("ApiUser")]


        public string ApiUserId { get; set; }
        public virtual ApiUser ApiUser { get; set; }
    }
}

