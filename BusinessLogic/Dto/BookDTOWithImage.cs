using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class BookDTOWithImage
    {
        public BookDTO Book { get; set; }
        public IFormFile? BookImg { get; set; }

        public BookDTOWithImage()
        {
        }
    }
}

