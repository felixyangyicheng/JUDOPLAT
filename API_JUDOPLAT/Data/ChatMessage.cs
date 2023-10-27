
namespace JUDOPLAT.API_JUDOPLAT.Data
{
	public class ChatMessage
	{


        public string SenderName { get; set; }

        public string Text { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public byte[]? ImageData { get; set; }

    }
}

