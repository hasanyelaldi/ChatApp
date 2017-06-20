using ChatApp.Model.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business
{
    public class ChatAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ChatAppContext>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
