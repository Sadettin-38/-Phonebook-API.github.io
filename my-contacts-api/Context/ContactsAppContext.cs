using Microsoft.EntityFrameworkCore;
using my_contacts_api.Models;

namespace my_contacts_api.Context
{
    public class ContactsAppContext : DbContext
    {
        public ContactsAppContext(DbContextOptions<ContactsAppContext> options): base(options) { }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
