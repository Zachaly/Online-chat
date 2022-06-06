using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;
        private string _profilePicturePath;

        public FileManager(IConfiguration configuration)
        { 
            _imagePath = configuration["Path:Images"];
            _profilePicturePath = configuration["Path:ProfilePictures"];
        }

        public FileStream ImageStream(string image)
            => new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

        public FileStream ProfilePictureStream(string image)
            => new FileStream(Path.Combine(_profilePicturePath, image), FileMode.Open, FileAccess.Read);

        public bool RemoveImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);
                if (File.Exists(file))
                {
                    File.Delete(file);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> SaveImage(IFormFile image, bool profilePicture = false)
        {
            try
            {
                string savePath = "";

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
