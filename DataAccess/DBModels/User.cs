using System;
using System.Collections.Generic;

namespace DataAccess.DBModels
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public int PermissionId { get; set; }
        public int ShulId { get; set; }

        public virtual Permission Permission { get; set; } = null!;
    }
}
