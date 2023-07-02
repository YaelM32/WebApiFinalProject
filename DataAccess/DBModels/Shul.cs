using System;
using System.Collections.Generic;

namespace DataAccess.DBModels
{
    public partial class Shul
    {
        public Shul()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Map { get; set; }
        public string? Logo { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
