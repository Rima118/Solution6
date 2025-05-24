using System.Diagnostics;
using common.DTO;
using DAL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace _18._05.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IImageRepository _repository;
        private readonly IWebHostEnvironment _env;

        public HomeController(IImageRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult<ImageDetailsDTO>> Upload([FromForm] ImageUploadDTO dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required.");

            var ext = Path.GetExtension(dto.File.FileName).ToLowerInvariant();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                return BadRequest("Only JPG and PNG files are allowed.");

            if (dto.File.Length > 5 * 1024 * 1024)
                return BadRequest("File must be <= 5MB.");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var folderPath = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(folderPath);
            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await dto.File.CopyToAsync(stream);

            int? width = null, height = null;
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(dto.File.OpenReadStream()))
            {
                width = image.Width;
                height = image.Height;
            }

            var id = await _repository.UploadImageAsync(dto, fileName, $"/images/{fileName}", width, height);
            var result = await _repository.GetImageByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ImageMetadataDTO>>> GetAll()
            => Ok(await _repository.GetAllImagesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImageDetailsDTO>> GetById(int id)
        {
            var image = await _repository.GetImageByIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _repository.GetImageByIdAsync(id);

            if (image == null)
            {
                return NotFound(); 
            }

            var filePath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(image.FilePath));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath); 
            }

            var deleted = await _repository.DeleteImageAsync(id);

            if (deleted)
            {
                return NoContent(); 
            }
            else
            {
                return StatusCode(500, "Delete failed"); 
            }
        }


        [HttpPut("{id:int}/Caption")]
        public async Task<ActionResult> UpdateCaption(int id, [FromBody] UpdateCaptionDTO dto)
        {
            var updated = await _repository.UpdateCaptionAsync(id, dto);

            if (updated)
            {
                return NoContent(); 
            }
            else
            {
                return NotFound(); 
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }

    
}

