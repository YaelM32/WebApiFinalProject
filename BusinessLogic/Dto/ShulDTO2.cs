using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto
{
    public class ShulDTO2
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public IFormFile? Logo { get; set; }
    }
}
