namespace WebChatExam.Models.Chats
{
    public class UnreadMessagesModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UnreadMessagesAmount { get; set; }
    }
}
