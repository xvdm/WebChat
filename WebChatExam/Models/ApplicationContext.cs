using Microsoft.EntityFrameworkCore;

namespace WebChatExam.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ChatMessagesModel> MessagesInChat { get; set; }
        public DbSet<ChatUnreadMessagesModel> UnreadMessages { get; set; }
        public DbSet<ChatUsersModel> UsersInChat { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
