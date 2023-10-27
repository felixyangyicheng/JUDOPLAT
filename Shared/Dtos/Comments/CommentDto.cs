

namespace JUDOPLAT.Shared.Dtos.Comments
{
    public class CommentDto
    {

        public int Id { get; set; }

        public string Content { get; set; }
     
        public DateTime CreatedOn { get; set; }
    
        public DateTime UpdatedOn { get; set; }
        public string ApiUserName { get; set; }
        public string ApiUserId { get; set; }
        public int PostId { get; set; }

    }
}
