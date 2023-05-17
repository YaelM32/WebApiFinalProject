using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BusinessLogic.Dto
{
    public class ShulDto
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Map { get; set; }
        public string? Logo { get; set; }

    }


}

