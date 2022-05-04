using System;

namespace WebChatExam.Models.ARCHIVE
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }  
        public int SenderId { get; set; }
    }
}
