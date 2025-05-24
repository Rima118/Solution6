using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class ImageEntry
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public DateTime UploadedAt { get; set; }
        public string FilePath { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
