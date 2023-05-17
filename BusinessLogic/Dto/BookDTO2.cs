using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class BookDTO2
    {
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

        public BookDTO2(string name, int chipId, int volumeNum, int? authorId, int? categoryId, int? editionId, int? publishYear, int shulId, int? copies, string? description)
        {
            Name = name;
            ChipId = chipId;
            VolumeNum = volumeNum;
            AuthorId = authorId;
            CategoryId = categoryId;
            EditionId = editionId;
            PublishYear = publishYear;
            ShulId = shulId;
            Copies = copies;
            Description = description;
        }

        public BookDTO2()
        {
        }
    }
}

