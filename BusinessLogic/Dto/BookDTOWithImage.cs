using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class BookDTOWithImage
    {
        public BookDto Book { get; set; }
        public IFormFile? BookImg { get; set; }

        public BookDTOWithImage()
        {
        }
    }
}

