using Microsoft.EntityFrameworkCore;
using WebChatExam.Models.Chats;

namespace WebChatExam.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
