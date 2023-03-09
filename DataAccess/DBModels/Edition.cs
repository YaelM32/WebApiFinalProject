using System;
using System.Collections.Generic;

namespace DataAccess.DBModels
{
    public partial class Edition
    {
        public Edition()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
