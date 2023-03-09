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

        public Book(int id, string name, int chipId, int volumeNum, int? authorId, int? categoryId, int? editionId, int? publishYear, int shulId, Author? author, Category? category, Edition? edition)
        {
            Id = id;
            Name = name;
            ChipId = chipId;
            VolumeNum = volumeNum;
            AuthorId = authorId;
            CategoryId = categoryId;
            EditionId = editionId;
            PublishYear = publishYear;
            ShulId = shulId;
            Author = author;
            Category = category;
            Edition = edition;
        }
        public Book()
        {

        }
        public virtual Author? Author { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Edition? Edition { get; set; }
    }
}
