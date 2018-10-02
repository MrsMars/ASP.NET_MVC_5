using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GameStore.UsersDomain.Entities;


namespace GameStore.UsersDomain.Concrete
{
    public class EFDbUserContext : DbContext
    {
        public EFDbUserContext() : base("UsersDbConnection") { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
