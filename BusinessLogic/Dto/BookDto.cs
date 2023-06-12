using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ChipId { get; set; }
        public int VolumeNum { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? Edition { get; set; }
        public int? PublishYear { get; set; }
        public int ShulId { get; set; }
        public int? Copies { get; set; }
        public string? Description { get; set; }
        public string? BookImgName { get; set; }
        public BookDto(int id,string name, int chipId, int volumeNum, string? author, string? category, string? edition, int? publishYear, int shulId,int? copies, string? description, string? bookImgName)
        {
            Id = id;
            Name = name;
            ChipId = chipId;
            VolumeNum = volumeNum;
            Author = author;
            Category = category;
            Edition = edition;
            PublishYear = publishYear;
            ShulId = shulId;
            Copies = copies;
            Description = description;
            BookImgName = bookImgName;
        }

        public BookDto()
        {
        }
    }
}
