using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.DTO
{
    public class UpdateCaptionDTO
    {
        [Required]
        [MaxLength(150)]
        public string Caption { get; set; }
    }
}
