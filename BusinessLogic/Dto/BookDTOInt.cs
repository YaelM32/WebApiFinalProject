using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class BookDTOInt
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? VolumeNum { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? EditionId { get; set; }
        public int? PublishYear { get; set; }
        public int ShulId { get; set; }
        public int? Copies { get; set; }
        public string? Description { get; set; }
        public string? BookImgName { get; set; }
        public int? MaxCopies { get; set; }

        public BookDTOInt(int id,string name, int volumeNum, int? authorId, int? categoryId, int? editionId, int? publishYear, int shulId, int? copies, string? description, string? bookImgName, int? maxCopies)
        {
            Id = id;
            Name = name;
            VolumeNum = volumeNum;
            AuthorId = authorId;
            CategoryId = categoryId;
            EditionId = editionId;
            PublishYear = publishYear;
            ShulId = shulId;
            Copies = copies;
            Description = description;
            BookImgName = bookImgName;
            MaxCopies = maxCopies;
        }

        public BookDTOInt()
        {
        }
    }
}

