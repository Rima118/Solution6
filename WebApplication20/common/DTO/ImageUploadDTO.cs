using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        [MaxLength(150)]
        public string Caption { get; set; }
    }
}
