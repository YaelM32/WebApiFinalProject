using System;
using System.Collections.Generic;

namespace DataAccess.DBModels
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ChipId { get; set; }
        public int VolumeNum { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? EditionId { get; set; }
        public int? PublishYear { get; set; }
        public int ShulId { get; set; }
        public int? Copies { get; set; }
        public string? Description { get; set; }

        public virtual Author? Author { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Edition? Edition { get; set; }
    }
}
