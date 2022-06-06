using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _imagePath;
        private readonly string _profilePicturePath;

        public FileManager(IConfiguration configuration)
        { 
            _imagePath = configuration["Path:Images"];
            _profilePicturePath = configuration["Path:ProfilePictures"];
        }

        public FileStream ImageStream(string image)
            => new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

        public FileStream ProfilePictureStream(string image)
            => new FileStream(Path.Combine(_profilePicturePath, image), FileMode.Open, FileAccess.Read);

        public async Task<string> SaveImage(IFormFile image, bool profilePicture = false)
        {
            try
            {
                string savePath = "";

                // save path differs if image is used as profile picture
                if (profilePicture)
                    savePath = Path.Combine(_profilePicturePath);
                else 
                    savePath = Path.Combine(_imagePath);

                if (!Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{Guid.NewGuid()}{mime}";

                using (var stream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error";
            }
        }
    }
}
