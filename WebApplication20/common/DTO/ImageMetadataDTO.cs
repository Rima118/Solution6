using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.DTO
{
    public class ImageMetadataDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public DateTime UploadedAt { get; set; }
        public string ImageUrl { get; set; }
    }
}
