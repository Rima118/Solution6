using common.DTO;
using DAL.Entity;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Object;

namespace DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> UploadImageAsync(ImageUploadDTO dto, string fileName, string filePath, int? width, int? height)
        {
            var image = new ImageEntry
            {
                FileName = fileName,
                FilePath = filePath,
                Caption = dto.Caption,
                UploadedAt = DateTime.UtcNow,
                Width = width,
                Height = height
            };

            _context.ImageEntries.Add(image);
            await _context.SaveChangesAsync();

            return image.Id;
        }

        public async Task<List<ImageMetadataDTO>> GetAllImagesAsync()
        {
            return await _context.ImageEntries
                .OrderByDescending(i => i.UploadedAt)
                .Select(i => new ImageMetadataDTO
                {
                    Id = i.Id,
                    FileName = i.FileName,
                    Caption = i.Caption,
                    UploadedAt = i.UploadedAt,
                    ImageUrl = i.FilePath
                }).ToListAsync();
        }

        public async Task<ImageDetailsDTO?> GetImageByIdAsync(int id)
        {
            var image = await _context.ImageEntries.FindAsync(id);
            if (image == null)
                return null;

            return new ImageDetailsDTO
            {
                Id = image.Id,
                FileName = image.FileName,
                Caption = image.Caption,
                UploadedAt = image.UploadedAt,
                FilePath = image.FilePath,
                Width = image.Width,
                Height = image.Height,
                ImageUrl = image.FilePath
            };
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await _context.ImageEntries.FindAsync(id);
            if (image == null) return false;

            _context.ImageEntries.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCaptionAsync(int id, UpdateCaptionDTO dto)
        {
            var image = await _context.ImageEntries.FindAsync(id);
            if (image == null) return false;

            image.Caption = dto.Caption;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
