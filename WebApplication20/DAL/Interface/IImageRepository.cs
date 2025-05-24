using common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IImageRepository
    {
        Task<int> UploadImageAsync(ImageUploadDTO dto, string fileName, string filePath, int? width, int? height);
        Task<List<ImageMetadataDTO>> GetAllImagesAsync();
        Task<ImageDetailsDTO?> GetImageByIdAsync(int id);
        Task<bool> DeleteImageAsync(int id);
        Task<bool> UpdateCaptionAsync(int id, UpdateCaptionDTO dto);
    }
}
