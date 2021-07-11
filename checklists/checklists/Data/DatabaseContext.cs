using checklists.Models.CheckListItems;
using checklists.Models.CheckLists;
using Microsoft.EntityFrameworkCore;

namespace checklists.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<CheckListsEntity> CheckList { get; set; }
        public DbSet<CheckListItemsEntity> CheckListItem { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
    }
}